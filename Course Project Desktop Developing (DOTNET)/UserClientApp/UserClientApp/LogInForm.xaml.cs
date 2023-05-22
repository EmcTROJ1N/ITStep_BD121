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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserClientApp
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
            if (Root.CurrentUser != null)
                MessageBox.Show("You already logged");
            else
            {
                if (Root.LogIn(LoginTextBox.Text))
                {
                    File.WriteAllText("authData.log", LoginTextBox.Text);
                    this.Close();
                }
            }
        }
    }
}
