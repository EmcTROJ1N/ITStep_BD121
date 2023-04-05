using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server? ServerClient;
        Dictionary<string, ObservableCollection<string>> Chats = new Dictionary<string, ObservableCollection<string>>();
        private ObservableCollection<string> ChatsListViewSource = new ObservableCollection<string>();

        private string? CurrentUser;

        public MainWindow()
        {
            InitializeComponent();
            ChatsListView.ItemsSource = ChatsListViewSource;
        }

        private void ServerClientOnGetResponse(Response? response)
        {
            if (response == null) return;

            if (response.Status == Response.ResponseStatus.Error)
                MessageBox.Show(((JsonElement)response.Body!).GetString());
            
            if (response.Status == Response.ResponseStatus.OK)
            {
                switch (response.Type)
                {
                    case Request.AnswerType.TextMessage:
                    {
                        Dispatcher.Invoke(() => Chats[response.From].Add($"{response.From}: {((JsonElement)response.Body!).GetString()!}"));
                        break;
                    }
                    case Request.AnswerType.OpenClientConnection:
                    {
                        Chats.Add(response.From!, new ObservableCollection<string>());
                        Dispatcher.Invoke(() => ChatsListViewSource.Add(response.From!));
                        break;
                    }
                    case Request.AnswerType.CloseClientConnection:
                    {
                        Chats[response.From!].Clear();
                        Chats.Remove(response.From!);
                        Dispatcher.Invoke(() => ChatsListViewSource.Remove(response.From!));
                        break;
                    }
                    case Request.AnswerType.OpenServerConnection:
                    {
                        MessageBox.Show(((JsonElement)response.Body!).GetString());
                        break;
                    }
                }
            }
        }

        private void ServerClientOnConnectionRefused(Exception e) =>
            MessageBox.Show(e.Message);

        private void AuthClick(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            auth.ShowDialog();
            ServerClient = auth.ServerClient;
            CurrentUser = auth.CurrentLogin;
            this.Title = $"Chatting client: {auth.CurrentLogin}";
            
            if (ServerClient != null)
            {
                ServerClient.ConnectionRefused += ServerClientOnConnectionRefused;
                ServerClient.GetResponse += ServerClientOnGetResponse;
            }
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (ServerClient == null || CurrentUser == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }
            if (CurrentInterviewerTextBlock.Text == "")
            {
                MessageBox.Show("Set interviewer");
                return;
            }
            
            ServerClient.SendRequest(new Request(Request.AnswerType.TextMessage, CurrentUser, CurrentInterviewerTextBlock.Text, MessageTextBox.Text));
            Chats[CurrentInterviewerTextBlock.Text].Add($"{CurrentUser}: {MessageTextBox.Text}");
        }

        private void OpenConnection(object sender, RoutedEventArgs e)
        {
            if (CurrentUser == null || ServerClient == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }

            ServerClient.SendRequest(new Request(Request.AnswerType.OpenClientConnection, CurrentUser,  ConnectionTextBox.Text));
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            ServerClient?.Client.Close();
            ServerClient = null;
            MessageBox.Show("Status: logout");
            this.Title = "Chatting client: logout";
            Chats.Clear();
            ChatsListViewSource.Clear();
        }

        private void CloseSelectedConnections(object sender, RoutedEventArgs e)
        {
            if (ServerClient == null || CurrentUser == null)
            {
                MessageBox.Show("Status: logout");
                return;
            }
            foreach (string chat in ChatsListView.SelectedItems)
                ServerClient.SendRequest(new Request(Request.AnswerType.CloseClientConnection, CurrentUser, chat));
        }

        private void ChatsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentInterviewerTextBlock.Text = (string)ChatsListView.SelectedItem;
            ChatView.ItemsSource = Chats[(string)ChatsListView.SelectedItem];
        }
    }
}