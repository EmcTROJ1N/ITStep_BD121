using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRUDSQL
{
    /// <summary>
    /// Логика взаимодействия для AddFormOwner.xaml
    /// </summary>
    public partial class AddFormOwner : Window
    {
        SqlConnection Connection;
        public AddFormOwner(SqlConnection connection)
        {
            InitializeComponent();
            this.Connection = connection;
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Connection.Open();

            SqlCommand cmd = new SqlCommand("insert into Owners (owner_id, fullname) values (@owner_id, @fullname)", Connection);

            SqlParameter ownerIdParam = new SqlParameter("@owner_id", SqlDbType.Int);
            ownerIdParam.Value = OwnerIDTextBox.Text;
            cmd.Parameters.Add(ownerIdParam);
            
            SqlParameter carIdParam = new SqlParameter("@fullname", SqlDbType.VarChar, 20);
            carIdParam.Value = FullNameTextBox.Text;
            cmd.Parameters.Add(carIdParam);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                cmd.CommandText = "update Owners set fullname = @fullname where owner_id = @owner_id";
                cmd.ExecuteNonQuery();
            }

            Connection.Close();
            this.Close();


            this.Close();
        }
    }
}
