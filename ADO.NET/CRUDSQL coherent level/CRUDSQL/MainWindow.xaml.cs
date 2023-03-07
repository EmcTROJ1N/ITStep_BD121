using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CRUDSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Car
    {
        public string Model { get; set; }
        public int MaxSpeed { get; set; }
        public int OwnerID { get; set; }
        public int CarID { get; set; }

        public Car(string model, int maxSpeed, int ownerID, int carID)
        {
            Model = model;
            MaxSpeed = maxSpeed;
            OwnerID = ownerID;
            CarID = carID;
        }
    }

    public class Owner
    {
        public int OwnerID { get; set; }
        public string FullName { get; set; }

        public Owner(int ownerId, string fullName)
        {
            OwnerID = ownerId;
            FullName = fullName;
        }

        public override string ToString() => FullName;
    }

    public partial class MainWindow : Window
    {
        SqlConnection Connection = new SqlConnection();
        ObservableCollection<Car> CarsList = new ObservableCollection<Car>();
        ObservableCollection<Owner> OwnersList = new ObservableCollection<Owner>();

        public MainWindow()
        {
            InitializeComponent();
            
            TableAuto.ItemsSource = CarsList;
            TableOwners.ItemsSource = OwnersList;

            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //builder.InitialCatalog = "Cars";
            //builder.DataSource = @".\SQLExpress";
            //builder.ConnectTimeout = 30;
            //builder.IntegratedSecurity = true;
            //builder.MultipleActiveResultSets = true;

            //Connection.ConnectionString = builder.ConnectionString;

            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["CRUDSQL.Properties.Settings.carsConnectionString"].ConnectionString;

            Update();
        }

        void Update()
        {
            if (Connection.State == ConnectionState.Open) return;

            Connection.Open();
            CarsList.Clear();
            OwnersList.Clear();
            SqlCommand cmdGetCars = new SqlCommand();
            cmdGetCars.Connection = Connection;
            cmdGetCars.CommandText = "select * from Auto";

            SqlDataReader dataCars = cmdGetCars.ExecuteReader(CommandBehavior.CloseConnection);
            
            
            SqlCommand cmdGetOwners = new SqlCommand();
            cmdGetOwners.Connection = Connection;
            cmdGetOwners.CommandText = "select * from Owners";
            SqlDataReader dataOwners = cmdGetOwners.ExecuteReader(CommandBehavior.CloseConnection);

            // Прочитать одну строку из результатов запроса
            while (dataCars.Read())
                CarsList.Add(new Car((string)dataCars["Model"], (int)dataCars["MaxSpeed"],
                                (int)dataCars["owner_id"], (int)dataCars["car_id"]));
            while (dataOwners.Read())
                OwnersList.Add(new Owner((int)dataOwners["owner_id"], (string)dataOwners["fullname"]));



            Connection.Close();

        }

        private void UpdateClick(object sender, RoutedEventArgs e) => Update();

        private void AddClick(object? sender, RoutedEventArgs? e)
        {
            switch (Tabs.SelectedIndex)
            {
                case 0:
                    {
                        AddFormCar form = new AddFormCar(Connection, OwnersList);
                        form.ShowDialog();
                        break;
                    }
                case 1:
                    {
                        AddFormOwner form = new AddFormOwner(Connection);
                        form.ShowDialog();
                        break;
                    }
            }
            Update();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Connection.Open();

            switch (Tabs.SelectedIndex)
            {
                case 0:
                    {
                        SqlCommand cmd = new SqlCommand("delete from Auto where car_id = @car_id", Connection);

                        SqlParameter param1 = new SqlParameter("@car_id", SqlDbType.Int);
                        param1.Value = ((Car)TableAuto.SelectedItem).CarID;
                        cmd.Parameters.Add(param1);

                        cmd.ExecuteNonQuery();
                        break;
                    }
                case 1:
                    {
                        SqlCommand cmd = new SqlCommand("delete from Owners where owner_id = @owner_id", Connection);

                        SqlParameter param1 = new SqlParameter("@owner_id", SqlDbType.Int);
                        param1.Value = ((Owner)TableOwners.SelectedItem).OwnerID;
                        cmd.Parameters.Add(param1);

                        cmd.ExecuteNonQuery();

                        break;
                    }
            }

            Connection.Close();
            Update();
        }
    }
}
