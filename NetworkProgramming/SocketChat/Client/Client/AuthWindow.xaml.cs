using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;

namespace Client;

public partial class AuthWindow : Window
{
    public Server? ServerClient { get; set; }
    public string CurrentLogin { get; set; }
    public AuthWindow() =>
        InitializeComponent();

    private void AuthClick(object sender, RoutedEventArgs e)
    {
       if (new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}$").IsMatch(ConnectionTextBox.Text) ||
           new Regex(@"^localhost:\d{1,5}$").IsMatch(ConnectionTextBox.Text))
       {
           ServerClient = new Server(new TcpClient(ConnectionTextBox.Text.Split(":")[0], 
               Convert.ToInt32(ConnectionTextBox.Text.Split(":")[1])));
           
           ServerClient.SendRequest(new Request(
               Request.AnswerType.OpenServerConnection,
               LoginTextBox.Text));
           CurrentLogin = LoginTextBox.Text;
           this.Close();
       }
       else MessageBox.Show("Invalid input. Give me valid pattern");
    }
}