using System;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Client;

public delegate void RecieveResponse(Response? response);
public delegate void ConnectionError(Exception e);

public class Server
{
    public event RecieveResponse GetResponse;
    public event ConnectionError ConnectionRefused;
    public TcpClient Client { get; set; }

    public Server(TcpClient client)
    {
        Client = client;
        Task.Run(() => ListenResponse());
    }

    public void SendRequest(Request request)
    {
        NetworkStream networkStream = Client.GetStream();
            
        if (networkStream.CanWrite)
            JsonSerializer.Serialize<Request>(networkStream, request, new JsonSerializerOptions() { WriteIndented = true} );
        else MessageBox.Show("Network stream is not available for writing");
    }

    void ListenResponse()
    {
        NetworkStream netStream = Client.GetStream();
        while (true)
        {
            if (netStream.CanRead)
            {
                Response? response = null;
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    netStream.Read(buffer, 0, 1024 * 1024);
                    response = JsonSerializer.Deserialize<Response>(new string(Encoding.UTF8.GetString(buffer).Where(c => Convert.ToInt32(c) != 0).ToArray()));
                }
                catch (Exception e) { ConnectionRefused?.Invoke(e); }
                
                if (response != null) GetResponse?.Invoke(response);
            }
            else MessageBox.Show("Can`t read!!!");
        }
        netStream.Close();
    }
}
