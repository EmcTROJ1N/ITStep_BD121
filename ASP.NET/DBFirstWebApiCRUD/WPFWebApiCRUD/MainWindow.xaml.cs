using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFWebApiCRUD.Models;
using System.Reflection;
using System.Reflection.Emit;

namespace WPFWebApiCRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string BASE_ADDRESS = "http://localhost:5006/";
        private ApiContext Context;

        private ObservableCollection<Car> Cars = new ObservableCollection<Car>();
        private ObservableCollection<Owner> Owners = new ObservableCollection<Owner>();

        public MainWindow()
        {
            InitializeComponent();
            CarsDataGrid.ItemsSource = Cars;
            OwnersDataGrid.ItemsSource = Owners;
            
            Context = new ApiContext(BASE_ADDRESS,
                new List<MediaTypeWithQualityHeaderValue>()
                {
                    new MediaTypeWithQualityHeaderValue("application/json")
                });
        }
        
        public async void DataGridUpdate<TModel>(string requestUri, ObservableCollection<TModel> sourceCollection)
        {
            List<TModel> data = await Context.GetDataRequestAsync<TModel>(requestUri);
            if (data is List<TModel>)
            {
                sourceCollection.Clear();
                foreach (TModel model in data)
                    sourceCollection.Add(model);
            }
        }

        public async void DataGridDelete<TModel>(string requestUri, ObservableCollection<TModel> sourceCollection)
        {
            IdContainer id = new IdContainer();
            GetId form = new GetId(id);
            form.ShowDialog();
            
            HttpContent? data = await Context.DeleteDataRequestAsync<TModel>($"{requestUri}/{id.Id}");

            if (data == null)
                MessageBox.Show("Delete confirmed");
            else
            {
                MessageBox.Show("");
            }
        }

        public async void DataGridCreate<TModel>(string requestUri, ObservableCollection<TModel> sourceCollection) where TModel: new()
        {
            TModel model = new TModel();
                        

            
        }
        
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem? target = (sender as MenuItem);
            if (target == null)
                return;
            
            string? requestUri = target.Tag as string;
            
            if (requestUri == null)
                return;
            
            MethodInfo? m = typeof (MainWindow).GetMethod ($"DataGrid{target.Header}");

            if (m == null)
                return;
            
            switch (requestUri)
            {
                case "api/cars":
                    m.MakeGenericMethod(typeof(Car)).Invoke(this, new object[] { requestUri, Cars });
                    break;
                case "api/owners":
                    m.MakeGenericMethod(typeof(Owner)).Invoke(this, new object[] { requestUri, Owners });
                    break;
            }
        }
    }
}