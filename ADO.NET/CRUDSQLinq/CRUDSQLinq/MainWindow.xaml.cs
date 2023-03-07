using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Windows;

namespace CRUDSQLinq
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection Connection = new SqlConnection();
        AnimalsDB Database = new AnimalsDB();

        public MainWindow()
        {
            InitializeComponent();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "AnimalsDB";
            builder.DataSource = @".\SQLExpress";
            builder.ConnectTimeout = 30;
            builder.IntegratedSecurity = true;
            builder.MultipleActiveResultSets = true;

            Connection.ConnectionString = builder.ConnectionString;

            Update();
        }

        void Update() => AnimalsGridView.ItemsSource = Database.Animals.Select(elem => elem);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Database.Animals.DeleteOnSubmit(Database.Animals.Select(elem => elem).Where(elem => elem.id == Int32.Parse(TextBoxDelete.Text)).First());
            Database.SubmitChanges();
            Update();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Animals animal = new Animals();
            animal.id = Int32.Parse(TextBoxID.Text);
            animal.family = TextBoxFamily.Text;
            animal.species = TextBoxSpecies.Text;
            animal.yearsOld = Int32.Parse(TextBoxYearsOld.Text);
            animal.mass = Int32.Parse(TextBoxMass.Text);

            Database.Animals.InsertOnSubmit(animal);
            Database.SubmitChanges();
            Update();
        }
    }

}
