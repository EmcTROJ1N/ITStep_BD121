using ChatClient.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class CalbackClient : Server.IServiceCallback
    {
        MainWindow Root;
        public CalbackClient(MainWindow root) =>
            Root = root;
        public void UpdateUI(User currentUser)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                Root.ChatsListView.ItemsSource = currentUser.ChatsListViewSource;
                if (Root.CurrentInterviewerTextBlock.Text != "")
                    Root.ChatView.ItemsSource = currentUser.Chats[Root.CurrentInterviewerTextBlock.Text];
            }));
        }
    }

    public partial class MainWindow
    {
        internal ServiceClient Server;

        internal string CurrentLogin;
        internal Server.User CurrentUser
        {
            get => (from user in Server.GetUsers()
                    where user.Login == CurrentLogin
                    select user).FirstOrDefault();
        }
        
        public MainWindow()
        {
            InitializeComponent();
            InstanceContext context = new InstanceContext(new CalbackClient(this));
            Server = new ServiceClient(context);

            //ChatsListView.ItemsSource = ChatsListViewSource;
        }

        private void ChatsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentInterviewerTextBlock.Text = (string)ChatsListView.SelectedItem;
            if (ChatsListView.SelectedItem != null)
                ChatView.ItemsSource = CurrentUser.Chats[(string)ChatsListView.SelectedItem];
            else
                ChatView.ItemsSource = new List<string>();
        }

        private void CloseSelectedConnections(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }
            foreach (string chat in ChatsListView.SelectedItems)
            {
                try
                {
                    Server.CloseConnection(CurrentUser.Login, chat);
                } catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            }
        }

        private void OpenConnection(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }
            
            try
            {
                Server.OpenConnection(CurrentUser.Login, ConnectionTextBox.Text);
            } catch (FaultException<string> ex) { MessageBox.Show(ex.Reason.ToString()); }
        }

        private void AuthClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.AddClient(LoginTextBox.Text);
            } catch (FaultException ex) { MessageBox.Show(ex.Message.ToString()); return; }
            CurrentLogin = LoginTextBox.Text;
            MessageBox.Show("Status: logged");
            this.Title = $"Chatting client: {LoginTextBox.Text}";
            CurrentUser.Chats.Clear();
            ChatsListView.ItemsSource = CurrentUser.ChatsListViewSource;
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.RemoveClient(CurrentUser.Login);
            } catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            MessageBox.Show("Status: logout");
            this.Title = "Chatting client: logout";
            CurrentLogin = null;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }
            if (CurrentInterviewerTextBlock.Text == "")
            {
                MessageBox.Show("Set interviewer");
                return;
            }
            
            try
            {
                Server.SendTextMessage(CurrentUser.Login, CurrentInterviewerTextBlock.Text, MessageTextBox.Text);
            } catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            
            MessageTextBox.Clear();
        }
    }
}