using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CRUDSQL
{
    /// <summary>
    /// Логика взаимодействия для AddFormCar.xaml
    /// </summary>
    public partial class AddFormCar : Window
    {
        SqlConnection Connection { get; set; }
        public AddFormCar(SqlConnection sqlConnection, ObservableCollection<Owner> collection)
        {
            InitializeComponent();
            Connection = sqlConnection;
            CarNameListBox.ItemsSource = collection;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();

            string command = "insert into Auto (owner_id, car_id, Model, MaxSpeed)" +
                             "values (@owner_id, @car_id, @model, @maxspeed)";

            SqlCommand cmd = new SqlCommand(command, Connection);

            SqlParameter ownerIdParam = new SqlParameter("@owner_id", SqlDbType.Int);
            ownerIdParam.Value = ((Owner)CarNameListBox.SelectedItem).OwnerID;
            cmd.Parameters.Add(ownerIdParam);
            
            SqlParameter carIdParam = new SqlParameter("@car_id", SqlDbType.Int);
            carIdParam.Value = CarIDTextBox.Text;
            cmd.Parameters.Add(carIdParam);
            
            SqlParameter modelParam = new SqlParameter("@model", SqlDbType.VarChar, 20);
            modelParam.Value = CarModelTextBox.Text;
            cmd.Parameters.Add(modelParam);

            SqlParameter maxSpeedParam = new SqlParameter("@maxspeed", SqlDbType.Int);
            maxSpeedParam.Value = CarMaxSpeedTextBox.Text;
            cmd.Parameters.Add(maxSpeedParam);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                cmd.CommandText = "update Auto set MaxSpeed = @maxspeed, Model = @model, owner_id = @owner_id where car_id = @car_id";
                cmd.ExecuteNonQuery();
            }

            Connection.Close();
            this.Close();
        }
    }
}
