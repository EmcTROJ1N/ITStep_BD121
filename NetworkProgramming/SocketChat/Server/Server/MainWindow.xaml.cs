using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, TcpClient> _Connections;
        public Dictionary<string, TcpClient> Connections = new Dictionary<string, TcpClient>();
        private ObservableCollection<string> ConnectionsViewSource = new ObservableCollection<string>();

        private TcpListener Listener;
        public MainWindow()
        {
            InitializeComponent();
            ConnectionsView.ItemsSource = ConnectionsViewSource;
            Listener = new TcpListener(IPAddress.Any, 123);
            Listener.Start();
            Task.Run(() => DoListen());
        }

        public void SendResponse(string login, Response response)
        {
            NetworkStream networkStream = Connections[login].GetStream();

            if (networkStream.CanWrite)
                JsonSerializer.Serialize<Response>(networkStream, response,
                    new JsonSerializerOptions() { WriteIndented = true });
        }

        void DoListen()
        {
            while (true)
            {
                TcpClient client = Listener.AcceptTcpClient();
                Task.Run(() => ClientListenMessages(client));
            }
        }

        void ClientListenMessages(TcpClient client)
        {
            if (!client.Connected) return;
            NetworkStream netStream = client.GetStream();
            string currentLogin = "";
            while (true)
            {
                if (netStream.CanRead)
                {
                    try
                    {
                        byte[] buffer = new byte[1024 * 1024];
                        netStream.Read(buffer, 0, 1024 * 1024);
                        Request? request = JsonSerializer.Deserialize<Request>(new string(Encoding.UTF8.GetString(buffer).Where(c => Convert.ToInt32(c) != 0).ToArray()));

                        if (request != null)
                        {
                            switch (request.Type)
                            {
                                case Request.AnswerType.OpenServerConnection:
                                {
                                    Dispatcher.Invoke(() => Connections.Add(request.From, client));
                                    Dispatcher.Invoke(() => ConnectionsViewSource.Add(request.From));
                                    SendResponse(request.From, new Response(Response.ResponseStatus.OK, Request.AnswerType.OpenServerConnection, "server", "Auth completed"));
                                    currentLogin = request.From;
                                    break;
                                }
                                case Request.AnswerType.TextMessage:
                                {
                                    if (request.To == null || request.Body == null)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.TextMessage, "server", "Request does not have receiver or body"));
                                    else if (Connections.ContainsKey(request.To) == false)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.TextMessage, "server", "Invalid login"));
                                    else    
                                        SendResponse(request.To, new Response(Response.ResponseStatus.OK, Request.AnswerType.TextMessage, request.From, request.Body));
                                    
                                    break;
                                }

                                case Request.AnswerType.OpenClientConnection:
                                {
                                    if (request.To == null)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.OpenClientConnection, "server", "Request does not have parameter: To"));
                                    else if (Connections.ContainsKey(request.To) == false)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.OpenClientConnection, "server", "Invalid login"));
                                    else
                                    {
                                        if (request.From == request.To)
                                            SendResponse(request.To, new Response(Response.ResponseStatus.OK, Request.AnswerType.OpenClientConnection, request.From, request.Body));
                                        else
                                        {
                                            SendResponse(request.To, new Response(Response.ResponseStatus.OK, Request.AnswerType.OpenClientConnection, request.From, request.Body));
                                            SendResponse(request.From, new Response(Response.ResponseStatus.OK, Request.AnswerType.OpenClientConnection, request.To, request.Body));
                                        }
                                    }
                                    break;
                                }

                                case Request.AnswerType.CloseClientConnection:
                                {
                                    if (request.To == null)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.CloseClientConnection, "server", "Request does not have parameter: To"));
                                    else if (Connections.ContainsKey(request.To) == false)
                                        SendResponse(request.From, new Response(Response.ResponseStatus.Error, Request.AnswerType.CloseClientConnection, "server", "Invalid login"));
                                    else
                                    {
                                        if (request.From == request.To)
                                            SendResponse(request.To, new Response(Response.ResponseStatus.OK, Request.AnswerType.CloseClientConnection, request.From, request.Body));
                                        else
                                        {
                                            SendResponse(request.To, new Response(Response.ResponseStatus.OK, Request.AnswerType.CloseClientConnection, request.From, request.Body));
                                            SendResponse(request.From, new Response(Response.ResponseStatus.OK, Request.AnswerType.CloseClientConnection, request.To, request.Body));
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Connections.Remove(currentLogin);
                        Dispatcher.Invoke(() => ConnectionsViewSource.Remove(currentLogin));
                        netStream.Close();
                        break;
                    }

                }
                else
                {
                    MessageBox.Show("Can`t read!!!");
                    netStream.Close();
                    break;
                }
            }
            netStream.Close();
        }
    }
}