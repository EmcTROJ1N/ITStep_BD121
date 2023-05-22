using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using AdministratorClientApp;
using AdministratorClientApp.ServiceReference;

namespace AdministratorClientApp
{

    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class AdminCallback : IAdminServiceCallback
    {
        MainWindow Root;
        public AdminCallback(MainWindow root) =>
            Root = root;

        void IAdminServiceCallback.UpdateData(User[] users)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Root.MembersListView.ItemsSource = users;
            }));
        }
    }


    public partial class MainWindow
    {
        internal AdminServiceClient Server;
        InstanceContext Context;
        internal Administrator CurrentAdmin;

        List<string> ForPCTasks = new List<string>()
        { 
            "Send messagebox", "Shutdown PC", "Reboot PC",
            "Hybernate PC", "Sleep PC", "File manager",
            "Regedit", "Lock PC", "Telnet", "Search files",
            "Task manager", "Get screenshot"
        };

        List<string> ForPCsTasks = new List<string>()
        {
            "Send messageboxes", "Shutdown PCs", "Reboot PCs",
            "Lock PCs", "Hybernate PCs", "Sleep PCs"
        };
        internal DispatcherTimer CheckConnectionTimer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            Context = new InstanceContext(new AdminCallback(this));
            Server = new AdminServiceClient(Context);
            TasksListView.ItemsSource = ForPCsTasks;
            UserTasksListView.ItemsSource = ForPCTasks;
            CheckConnectionTimer.Interval = TimeSpan.FromSeconds(5);
            CheckConnectionTimer.Tick += CheckConnectionTimer_Tick;
            CheckConnectionTimer.Start();

            if (File.Exists("authData.json"))
            {
                Dictionary<string, string> authData = new Dictionary<string, string>();
                using (StreamReader rdr = new StreamReader("authData.json"))
                    authData = JsonSerializer.Deserialize<Dictionary<string, string>>(rdr.BaseStream);
                Task.Run(() => LogIn(authData["login"], authData["password"]));
            }
        }
        private async void CheckConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (Server.State == CommunicationState.Faulted)
            {
                Server = new AdminServiceClient(Context);
                await Task.Run(() =>
                {
                    try { Server.Open(); }
                    catch (CommunicationException) { return; }
                    catch (TimeoutException) { return; }

                    if (File.Exists("authData.json"))
                    {
                        Dictionary<string, string> authData = new Dictionary<string, string>();
                        using (StreamReader rdr = new StreamReader("authData.json"))
                            authData = JsonSerializer.Deserialize<Dictionary<string, string>>(rdr.BaseStream);
                        Task.Run(() => LogIn(authData["login"], authData["password"]));
                    }
                });
            }
        }


        public bool LogIn(string login, string password)
        {
            try { CurrentAdmin = Server.LogInAdmin(login, password); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return false; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return false; }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            Dispatcher.Invoke(() =>
            {
                LoggedAsItem.Header = $"Logged as: {login}";
                ConnectorIDItem.Header = $"Connector ID: {CurrentAdmin.ConnectorID} (Click to copy)";
            });
            return true;
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            if (CurrentAdmin != null)
            {
                MessageBox.Show("You are already in account");
                return;
            }
            LogInForm form = new LogInForm(this);
            form.ShowDialog();
        }

        private void Registrate(object sender, RoutedEventArgs e)
        {
            if (CurrentAdmin != null)
            {
                MessageBox.Show("You are already in account");
                return;
            }
            RegisterForm form = new RegisterForm(this);
            form.ShowDialog();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (Server.State == CommunicationState.Closed && Server.State == CommunicationState.Closing)
                MessageBox.Show("Connection already closed");
            else if (CurrentAdmin == null)
                MessageBox.Show("You are already logout");
            else
            {
                try { Server.LogOutAdmin(CurrentAdmin.Login, CurrentAdmin.Password); }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                CurrentAdmin = null;
                LoggedAsItem.Header = $"Logged as: ";
                ConnectorIDItem.Header = $"Connector ID: (Click to copy)";
                MembersListView.ItemsSource = new List<User>();
                MessageBox.Show("Logout confirmed");
            }
        }

        private void ConnectorIDItem_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAdmin != null)
                Clipboard.SetText(CurrentAdmin.ConnectorID.ToString());
        }
        private void TasksListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (TasksListView.SelectedItem == null) return;
            if (CurrentAdmin == null)
            {
                MessageBox.Show("Log at first");
                return;
            }
            
            switch (TasksListView.SelectedItem.ToString())
            {
                case "Send messageboxes":
                    {
                        SendMessageboxForm form = new SendMessageboxForm(Server, CurrentAdmin, true, null);
                        form.ShowDialog();
                        break;
                    }
                case "Hybernate PCs":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to hybernate all PCs in network?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.SuspendStationBroadcast(CurrentAdmin.Login, CurrentAdmin.Password, true); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Sleep PCs":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to sleep all PCs in network?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.SuspendStationBroadcast(CurrentAdmin.Login, CurrentAdmin.Password, false); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }

                case "Shutdown PCs":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to shutdown all PCs in network?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.StartProcessBroadcast(CurrentAdmin.Login, CurrentAdmin.Password, "shutdown", "/s /t 0"); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Reboot PCs":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to reboot all PCs in network?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.StartProcessBroadcast(CurrentAdmin.Login, CurrentAdmin.Password, "shutdown", "/r /t 0"); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Lock PCs":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to lock all PCs in network?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.LockStationBroadcast(CurrentAdmin.Login, CurrentAdmin.Password); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
            }
        }

        private void UserTasksListView_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (UserTasksListView.SelectedItem == null) return;
            if (CurrentAdmin == null)
            {
                MessageBox.Show("Log at first");
                return;
            }
            if (MembersListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Choose one member");
                return;
            }
            if (UserTasksListView.SelectedItems.Count != 1)
            {
                MessageBox.Show("Choose one task");
                return;
            }

            string currentLogin = ((User)MembersListView.SelectedItem).Login;

            switch (UserTasksListView.SelectedItem.ToString())
            {
                case "Send messagebox":
                    {
                        SendMessageboxForm form = new SendMessageboxForm(Server, CurrentAdmin, false, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "Task manager":
                    {
                        TaskManagerForm form = new TaskManagerForm(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "Search files":
                    {
                        FindFilesForm form = new FindFilesForm(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "File manager":
                    {
                        Explorer form = new Explorer(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "Regedit":
                    {
                        RegeditForm form = new RegeditForm(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "Telnet":
                    {
                        Telnet form = new Telnet(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }
                case "Hybernate PC":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to hybernate {currentLogin} PC?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.SuspendStation(CurrentAdmin.Login, CurrentAdmin.Password, currentLogin, true); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Sleep PC":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to sleep {currentLogin} PC?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.SuspendStation(CurrentAdmin.Login, CurrentAdmin.Password, currentLogin, false); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }

                case "Shutdown PC":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want shutdown {currentLogin} PC?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.StartProcess(CurrentAdmin.Login, CurrentAdmin.Password, currentLogin, "shutdown", "/s /t 0"); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Reboot PC":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want reboot {currentLogin} PC?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.StartProcess(CurrentAdmin.Login, CurrentAdmin.Password, currentLogin, "shutdown", "/r /t 0"); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Lock PC":
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you really want to lock {currentLogin} PC?",
                            "Choose", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            try { Server.LockStation(CurrentAdmin.Login, CurrentAdmin.Password, currentLogin); }
                            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
                            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
                        }
                        break;
                    }
                case "Get screenshot":
                    {
                        Screen form = new Screen(Server, CurrentAdmin, currentLogin);
                        form.ShowDialog();
                        break;
                    }

            }

        }

        private void MembersListView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MembersListView.SelectedItem is User)
               PCNameTextBlock.Text = ((User)MembersListView.SelectedItem).Login;
        }
    }
}