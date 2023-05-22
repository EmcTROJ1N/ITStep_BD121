using AdministratorClientApp.ServiceReference;
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
    /// Логика взаимодействия для SendMessageboxForm.xaml
    /// </summary>
    public partial class SendMessageboxForm : Window
    {
        AdminServiceClient Server;
        Administrator Admin;
        string TargetLogin;
        bool IsMultiple;

        public SendMessageboxForm(AdminServiceClient server, Administrator admin, bool isMultiple, string login)
        {
            InitializeComponent();
            Server = server;
            TargetLogin = login;
            Admin = admin;
            IsMultiple = isMultiple;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsMultiple)
            {
                try { Server.SendMessageBoxBroadcastAsync(Admin.Login, Admin.Password, MessageTextBox.Text); }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    if (TargetLogin == null)
                        throw new NullReferenceException("Target login is null");
                    Server.SendMessageBoxAsync(Admin.Login, Admin.Password, TargetLogin, MessageTextBox.Text); 
                }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
