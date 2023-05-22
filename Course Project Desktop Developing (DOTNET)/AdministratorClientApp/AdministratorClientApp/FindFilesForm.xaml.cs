using AdministratorClientApp.ServiceReference;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для FindFilesForm.xaml
    /// </summary>
    /// 


    public partial class AdminCallback : IAdminServiceCallback
    {
        void IAdminServiceCallback.GetFile(FileContainer file) =>
            FindFilesForm.CurrentFile = file;
    }

    public partial class FindFilesForm : Window
    {
        internal static FileContainer CurrentFile = null;
        AdminServiceClient Server;
        Administrator Admin;
        string Login;
        ObservableCollection<FileContainer> Files;
        public FindFilesForm(AdminServiceClient server, Administrator admin, string login)
        {
            InitializeComponent();
            Login = login;
            Server = server;
            Admin = admin;
            Files = new ObservableCollection<FileContainer>();
            FilesDataGrid.ItemsSource = Files;
            AppendFiles();
        }

        private void AppendFiles()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (CurrentFile != null)
                    {
                        Dispatcher.Invoke(() => Files.Add(CurrentFile));
                        CurrentFile = null;
                    }
                }
            });
        }

        private void StartSearchClick(object sender, RoutedEventArgs e)
        {
            if (MaskTextBox.Text == "")
            {
                MessageBox.Show("Set mask");
                return;
            }

            if (Regex.IsMatch(FolderPath.Text, @"^(?:[a-zA-Z]:)?(?:\/|\\)(?:(?!\.\w+)[\w\-\.\s]+(?:\/|\\))*$") == false)
            {
                MessageBox.Show("Invalid path. Check that it ends with /");
                return;
            }
            
            try
            {
                Files.Clear();
                string mask = MaskTextBox.Text;
                string path = FolderPath.Text;
                Task.Run(() => Server.BeginSearchFiles(Admin.Login, Admin.Password, Login, path, mask));
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }

        private void PauseFind(object sender, RoutedEventArgs e)
        {
            try { Server.PauseSearch(Admin.Login, Admin.Password); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }

        private void ResumeFind(object sender, RoutedEventArgs e)
        {
            try { Server.ResumeSearch(Admin.Login, Admin.Password); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }
        private void StopFind(object sender, RoutedEventArgs e)
        {
            try { Server.StopSearch(Admin.Login, Admin.Password); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }

        private void SearchStatus(object sender, RoutedEventArgs e)
        {
            try
            {
                ResourcesSearchingStatus Status = Server.GetSearchingStatus(Admin.Login, Admin.Password);
                MessageBox.Show(Status.ToString());
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            if (Server.GetSearchingStatus(Admin.Login, Admin.Password) != ResourcesSearchingStatus.WaitingForStart)
            try { Server.StopSearch(Admin.Login, Admin.Password); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            try 
            {
                foreach (FileContainer file in FilesDataGrid.SelectedItems)
                    Server.StartProcess(Admin.Login, Admin.Password, Login, file.FullName, null); 
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
        }
    }
}
