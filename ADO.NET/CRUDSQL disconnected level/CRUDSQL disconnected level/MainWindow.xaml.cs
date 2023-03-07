using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CRUDSQL_disconnected_level
{

    public partial class MainWindow : Window
    {
        SqlConnection Connection = new SqlConnection();
        SqlDataAdapter? Adapter;
        DataTable AnimalsTable = new DataTable();
        //DataView AnimalsView;

        public MainWindow()
        {
            InitializeComponent();
            //AnimalsView = new DataView(AnimalsTable);
            

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "AnimalsDB";
            builder.DataSource = @".\SQLExpress";
            builder.ConnectTimeout = 30;
            builder.IntegratedSecurity = true;
            builder.MultipleActiveResultSets = true;

            Connection.ConnectionString = builder.ConnectionString;
        }

        private void SendDataClick(object sender, RoutedEventArgs e)
        {
            if (Adapter == null) return;

            SqlCommand cmd = Connection.CreateCommand();

            cmd.CommandText = "insert into Animals (id, family, species, yearsOld, mass) values (@p1, @p2, @p3, @p4, @p5)";
            cmd.Parameters.Add("@p1", SqlDbType.Int, 100, "id");
            cmd.Parameters.Add("@p2", SqlDbType.VarChar, 40, "family");
            cmd.Parameters.Add("@p3", SqlDbType.VarChar, 20, "species");
            cmd.Parameters.Add("@p4", SqlDbType.Int, 4, "yearsOld");
            cmd.Parameters.Add("@p5", SqlDbType.Int, 4, "mass");
            Adapter.InsertCommand = cmd;

            cmd = new SqlCommand("delete from Animals where id = @p", Connection);
            cmd.Parameters.Add("@p", SqlDbType.Int, 4, "id");
            Adapter.DeleteCommand = cmd;

            cmd = new SqlCommand("update Animals set family = @p2, species = @p3, yearsOld = @p4, mass = @p5 where id = @p1", Connection);
            cmd.Parameters.Add("@p1", SqlDbType.Int, 4, "id");
            cmd.Parameters.Add("@p2", SqlDbType.VarChar, 40, "family");
            cmd.Parameters.Add("@p3", SqlDbType.VarChar, 20, "species");
            cmd.Parameters.Add("@p4", SqlDbType.Int, 4, "yearsOld");
            cmd.Parameters.Add("@p5", SqlDbType.Int, 4, "mass");
            Adapter.UpdateCommand = cmd;

            Adapter.Update(AnimalsTable);

            // очистка локальной таблицы
            AnimalsTable.Clear();

            // заполнение таблицы с сервера
            Adapter.Fill(AnimalsTable);
        }

        private void GetDataClick(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "select * from Animals";
            Adapter = new SqlDataAdapter(cmd);

            AnimalsTable.Clear();
            Adapter.Fill(AnimalsTable);
            
            //AnimalsGridView.DataContext = AnimalsTable.DefaultView;
            AnimalsGridView.ItemsSource = AnimalsTable.DefaultView;
        }

    }
}
