using AdministratorClientApp.ServiceReference;
using System.Windows;
using System.Diagnostics;
using System;
using System.Text;
using System.IO;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using System.ServiceModel;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для Telnet.xaml
    /// </summary>
    public partial class Telnet : Window
    {
        AdminServiceClient Server;
        Administrator Admin;
        string TargetLogin;
        
        public Telnet(AdminServiceClient server, Administrator admin, string targetLogin)
        {
            InitializeComponent();
            Server = server;
            Admin = admin;
            TargetLogin = targetLogin;
            NewSessionClick(null, null);
        }

        private void NewSessionClick(object sender, RoutedEventArgs e)
        {
            TelnetRichTextBox.Document.Blocks.Clear();
            CommandTextBox.Text = "";
            try { Server.SendCommand(Admin.Login, Admin.Password, TargetLogin, "init"); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }
        }

        private void DockPanel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string command = CommandTextBox.Text.ToLower();
                if (new string[] { "cls", "clear" }.Contains(command))
                {
                    TelnetRichTextBox.Document.Blocks.Clear();
                    CommandTextBox.Text = "";
                }
                if (command == "exit")
                    this.Close();

                string[] result;

                try { result = Server.SendCommand(Admin.Login, Admin.Password, TargetLogin, command); }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }

                if (result.Length == 0)
                {
                    CommandTextBox.Text = "";
                    return;
                }
                Run outputRun = new Run();
                Paragraph outputParagraph = new Paragraph(outputRun);
                outputRun.Text += String.Join(Environment.NewLine, result);
                TelnetRichTextBox.Document.Blocks.Add(outputParagraph);
                CommandTextBox.Text = "";
                TelnetRichTextBox.ScrollToEnd();
            }
        }

        //string[] SendPSCommand(string command)
        //{
        //    Shell.AddScript(command);
        //    Collection<PSObject> collec = Shell.Invoke();

        //    return (from psobj in Shell.Invoke()
        //            select psobj.ToString()).ToArray();
        //}
    }
}
