using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBUniversalCRUD
{
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder();
        Dictionary<string, DataGrid> Templates = new Dictionary<string, DataGrid>();
        DBReader View;
        public MainWindow()
        {
            InitializeComponent();
            Builder.DataSource = @".\SQLExpress";
            Builder.ConnectTimeout = 30;
            Builder.IntegratedSecurity = true;
            Builder.MultipleActiveResultSets = true;

            DataGrid planesGrid = new DataGrid();
            planesGrid.AutoGenerateColumns = false;

            DataGridTextColumn id = new DataGridTextColumn();
            id.Header = "ID";
            id.Binding = new Binding("id")
            {  UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            planesGrid.Columns.Add(id);

            DataGridTextColumn brand = new DataGridTextColumn();
            brand.Header = "Brand";
            brand.Binding = new Binding("brand")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            planesGrid.Columns.Add(brand);

            DataGridTextColumn model = new DataGridTextColumn();
            model.Header = "Model";
            model.Binding = new Binding("model")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            planesGrid.Columns.Add(model);

            DataGridTextColumn seats = new DataGridTextColumn();
            seats.Header = "Seats";
            seats.Binding = new Binding("seats")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            planesGrid.Columns.Add(seats);

            DataGridTextColumn volume = new DataGridTextColumn();
            volume.Header = "Volume";
            volume.Binding = new Binding("volume")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            planesGrid.Columns.Add(volume);

            DataTemplate template = new DataTemplate();

            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));

            imageFactory.SetBinding(Image.SourceProperty, new Binding("photo"));
            imageFactory.SetValue(Image.WidthProperty, 100.0);
            imageFactory.SetValue(Image.HeightProperty, double.NaN);

            template.VisualTree = imageFactory;

            planesGrid.RowDetailsTemplate = template;

            Templates["planes"] = planesGrid;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Field.Children.Remove(View);
            Builder.InitialCatalog = DbName.Text;
            View = new DBReader(Builder.ConnectionString, Templates);
            Grid.SetRow(View, 1);
            Grid.SetColumn(View, 0);
            Field.Children.Add(View);
        }
    }
}
