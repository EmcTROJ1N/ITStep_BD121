using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        MainWindow Root;
        public RegisterForm(MainWindow root)
        {
            InitializeComponent();
            Root = root;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (new CommunicationState[] { CommunicationState.Closing, CommunicationState.Closed, CommunicationState.Faulted }.Contains(Root.Server.State))
                MessageBox.Show("Connection broken, sorry");
            else if (Root.CurrentAdmin != null)
                MessageBox.Show("You already logged");
            else
            {
                try
                {
                    if (Password1Box.Password != Password2Box.Password)
                        throw new Exception("The passwords don`t match");
                    Root.CurrentAdmin = Root.Server.RegisterAdmin(LoginTextBox.Text, Password1Box.Password);

                    Root.LoggedAsItem.Header = $"Logged as: {LoginTextBox.Text}";
                    Root.ConnectorIDItem.Header = $"Connector ID: {Root.CurrentAdmin.ConnectorID} (Click to copy)";

                    this.Close();
                    MessageBox.Show("Register confirmed");
                }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
