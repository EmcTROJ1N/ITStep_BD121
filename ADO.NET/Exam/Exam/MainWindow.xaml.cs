using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using System.Text;
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

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public class PlaneWithManufacture
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public partial class MainWindow : Window
    {
        PlanesManufacturesEntities Context = new PlanesManufacturesEntities();

        public MainWindow()
        {
            InitializeComponent();
            SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder();
            Builder.DataSource = @".\SQLExpress";
            Builder.ConnectTimeout = 30;
            Builder.IntegratedSecurity = true;
            Builder.MultipleActiveResultSets = true;
            Builder.InitialCatalog = "Company";

            Dictionary<string, DataGrid> template = new Dictionary<string, DataGrid>();

            DataGrid dataGrid = new DataGrid();
            dataGrid.AutoGenerateColumns = false;

            DataGridTextColumn id = new DataGridTextColumn();
            id.Header = "ID";
            id.Binding = new Binding("Employee_id")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGrid.Columns.Add(id);

            DataGridTextColumn fName = new DataGridTextColumn();
            fName.Header = "First name";
            fName.Binding = new Binding("FirstName")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGrid.Columns.Add(fName);

            DataGridTextColumn lName = new DataGridTextColumn();
            lName.Header = "Last Name";
            lName.Binding = new Binding("LastName")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGrid.Columns.Add(lName);

            DataGridTextColumn age = new DataGridTextColumn();
            age.Header = "Age";
            age.Binding = new Binding("Age")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGrid.Columns.Add(age);

            DataGridTextColumn address = new DataGridTextColumn();
            address.Header = "Address";
            address.Binding = new Binding("Address")
            { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGrid.Columns.Add(address);

            DataTemplate helper = new DataTemplate();
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetBinding(Image.SourceProperty, new Binding("PhotoPath"));
            imageFactory.SetValue(Image.WidthProperty, 100.0);
            imageFactory.SetValue(Image.HeightProperty, double.NaN);
            helper.VisualTree = imageFactory;
            dataGrid.RowDetailsTemplate = helper;


            template["Employees"] = dataGrid;

            CompanyGrid.Children.Add(new DBViewer(Builder.ConnectionString, template));

            PlanesDataGrid.ItemsSource = Context.Planes.ToList();
            ManufacturesDataGrid.ItemsSource = Context.Manufacturers.ToList();
        }

        private void PlanesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Выделенная строка в таблице с НОВЫМИ данными (режим UpdateSourceTrigger=PropertyChanged)
            Planes selectedRow = PlanesDataGrid.SelectedItem as Planes;

            // Получить из БД ссылку на редактируемый объект в базе
            Planes selectedDB = (from p in Context.Planes
                                 where p.Id == selectedRow.Id
                                 select p).FirstOrDefault();

            if (selectedDB == null)
            {
                Context.Planes.Add(selectedRow);
                Context.SaveChanges();
                PlanesDataGrid.ItemsSource = Context.Planes.ToList();
                return;
            }

            // Перенести данные из таблицы в объект в БД

            Context.SaveChanges();
            PlanesDataGrid.ItemsSource = Context.Planes.ToList();
        }

        private void ManufacturesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Выделенная строка в таблице с НОВЫМИ данными (режим UpdateSourceTrigger=PropertyChanged)
            Manufacturers selectedRow = ManufacturesDataGrid.SelectedItem as Manufacturers;

            // Получить из БД ссылку на редактируемый объект в базе
            Manufacturers selectedDB = (from p in Context.Manufacturers
                                        where p.ManufacturerId == selectedRow.ManufacturerId
                                        select p).FirstOrDefault();

            if (selectedDB == null)
            {
                Context.Manufacturers.Add(selectedRow);
                Context.SaveChanges();
                ManufacturesDataGrid.ItemsSource = Context.Manufacturers.ToList();
                return;
            }

            // Перенести данные из таблицы в объект в БД

            Context.SaveChanges();
            ManufacturesDataGrid.ItemsSource = Context.Manufacturers.ToList();
        }

        private void FindClick(object sender, RoutedEventArgs e)
        {
            PlanesDataGrid.ItemsSource = (from plane in Context.Planes
                                where plane.ManufacturerId.ToString() == FindTxt.Text ||
                                      plane.Id.ToString() == FindTxt.Text ||
                                      plane.Price.ToString() == FindTxt.Text ||
                                      plane.Speed.ToString() == FindTxt.Text
                                select plane).ToList();
            ManufacturesDataGrid.ItemsSource = (from manufacture in Context.Manufacturers
                                      where manufacture.ManufacturerId.ToString() == FindTxt.Text ||
                                            manufacture.Address.ToString() == FindTxt.Text ||
                                            manufacture.BrandTitle.ToString() == FindTxt.Text ||
                                            manufacture.Phone.ToString() == FindTxt.Text
                                      select manufacture).ToList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            PlanesDataGrid.ItemsSource = Context.Planes.ToList();
            ManufacturesDataGrid.ItemsSource = Context.Manufacturers.ToList();
        }

        private void FindAllPlanesByManufacterClick(object sender, RoutedEventArgs e)
        {
            SummaryDataGrid.ItemsSource = Context.Database.SqlQuery<PlaneWithManufacture>("EXEC GetAllPlanesByManufacturer @param",
                new SqlParameter("@param", ManufactureNameTxt.Text)).ToList();
        }
    }
}
