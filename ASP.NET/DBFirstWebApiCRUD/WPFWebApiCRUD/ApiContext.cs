using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;

namespace WPFWebApiCRUD;

public class ApiContext
{
    private HttpClient Connection;

    private string _baseAdress;
    public string BaseAddress
    {
        get => _baseAdress;
        set
        {
            this._baseAdress = value;
            Connection.BaseAddress = new Uri(value);
        }
    }
    
    public ApiContext(string address, IList<MediaTypeWithQualityHeaderValue> requestHeaders)
    {
        Connection = new HttpClient();
        BaseAddress = address;
        foreach (MediaTypeWithQualityHeaderValue item in requestHeaders)
            Connection.DefaultRequestHeaders.Accept.Add(item);
    }

    public async Task<string> GetStringRequestAsync(string requestUri)
    {
        HttpResponseMessage response = await Connection.GetAsync(requestUri);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<List<TModel>> GetDataRequestAsync<TModel>(string requestUri)
    {
        HttpResponseMessage response = await Connection.GetAsync(requestUri);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            List<TModel> data = JsonSerializer.Deserialize<IEnumerable<TModel>>(json).ToList();
            return data;
        }
        else
        {
            MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            return null;
        }
    }

    public async Task<HttpContent?> DeleteDataRequestAsync<TModel>(string requestUri)
    {
        HttpResponseMessage response = await Connection.DeleteAsync(requestUri);
        if (response.IsSuccessStatusCode)
            return null;
        else
        {
            string json = await response.Content.ReadAsStringAsync();
            HttpContent? error = JsonSerializer.Deserialize<HttpContent>(json);
            return error;
        }
    }
}