using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
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
    /// Логика взаимодействия для LogInForm.xaml
    /// </summary>
    public partial class LogInForm : Window
    {
        MainWindow Root;
        public LogInForm(MainWindow root)
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
                if (Root.LogIn(LoginTextBox.Text, PassBox.Password))
                {
                    Dictionary<string, string> authData = new Dictionary<string, string>
                    {
                        { "login", LoginTextBox.Text },
                        { "password", PassBox.Password }
                    };

                    using (StreamWriter wrter = new StreamWriter("authData.json"))
                        JsonSerializer.Serialize<Dictionary<string, string>>(wrter.BaseStream, authData);
                    MessageBox.Show("Login confirmed");
                    this.Close();
                }
            }
        }
    }
}
