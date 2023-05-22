using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.IO;

namespace UserClientApp
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
            else if (Root.CurrentUser != null)
                MessageBox.Show("You already logged");
            else
            {
                try
                {
                    Int32.TryParse(ConnectorTextBox.Text, out int connector);
                    if (connector == 0)
                    {
                        MessageBox.Show("Invalid connector");
                        return;
                    }
                    Root.CurrentUser = Root.Server.RegistrateUser(LoginTextBox.Text, connector);
                    Root.LoginTextBox.Text = $"You login: {LoginTextBox.Text}";
                    Root.ConnectorIDTextBox.Text = $"Connector ID: {connector}";
                    Root.ConnectionWithItem.Header = $"Connected with: {Root.Server.GetAdministratorById(Root.CurrentUser.ConnectorID).Login}";
                    Root.ConnectionStatusItem.Header = "Connection status: Connected";
                    this.Close();
                }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                File.WriteAllText("authData.log", LoginTextBox.Text);
            }
        }
    }
}
