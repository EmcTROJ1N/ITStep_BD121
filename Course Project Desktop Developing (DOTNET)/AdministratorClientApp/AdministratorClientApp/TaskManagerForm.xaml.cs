using AdministratorClientApp.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using System.ServiceModel;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для TaskManagerForm.xaml
    /// </summary>
    public partial class TaskManagerForm : Window
    {
        AdminServiceClient Server;
        string Login;
        Administrator Admin;
        DispatcherTimer Timer = new DispatcherTimer();
        ObservableCollection<ProcessContainer> Processes;
        public TaskManagerForm(AdminServiceClient server, Administrator admin, string login)
        {
            InitializeComponent();
            this.Server = server;
            this.Login = login;
            this.Admin = admin;
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;


            try { Processes = new ObservableCollection<ProcessContainer>(Server.GetProcesses(Admin.Login, Admin.Password, Login, "")); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            CollectionViewSource ProcessesView = new CollectionViewSource { Source = Processes };
            ProcessesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            ProcessGrid.ItemsSource = ProcessesView.View;
            
            Timer.Start();
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            List<ProcessContainer> newProcesses = new List<ProcessContainer>();
            string filter = FilterBox.Text;

            try { await Task.Run(() => { newProcesses = Server.GetProcesses(Admin.Login, Admin.Password, Login, filter).ToList(); }); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            List<ProcessContainer> removedItems = Processes.Except(newProcesses).ToList();
            List<ProcessContainer> addedItems = newProcesses.Except(Processes).ToList();

            List<int> removedIdxs = new List<int>();
            List<int> addedIdxs = new List<int>();

            for (int i = 0; i < removedItems.Count; i++)
            {
                for (int j = 0; j < addedItems.Count; j++)
                {
                    if (removedItems[i].Name == addedItems[j].Name && removedItems[i].Id == addedItems[j].Id)
                    {
                        removedItems[i].WindowTitle = addedItems[j].WindowTitle;
                        removedItems[i].BasePriority = addedItems[j].BasePriority;
                        removedItems[i]._PagedMemory = addedItems[j]._PagedMemory;
                        removedItems[i].Responding = addedItems[j].Responding;

                        removedIdxs.Add(i);
                        addedIdxs.Add(j);
                    }
                }
            }

            for (int i = addedIdxs.Count - 1; i >= 0; i--)
            {
                try
                {
                    removedItems.RemoveAt(removedIdxs[i]);
                    addedItems.RemoveAt(addedIdxs[i]);
                }
                catch { }
            }


            foreach (ProcessContainer item in removedItems)
                Processes.Remove(item);
            foreach (ProcessContainer item in addedItems)
                Processes.Add(item);
        }

        private void Terminate(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.TerminateProcesses(Admin.Login, Admin.Password, Login,
                                                 (from ProcessContainer process in ProcessGrid.SelectedItems
                                                  select process.Id).ToArray());
            }

            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Suspend(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.SuspendProcesses(Admin.Login, Admin.Password, Login,
                                                 (from ProcessContainer process in ProcessGrid.SelectedItems
                                                  select process.Id).ToArray());
            }

            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Resume(object sender, RoutedEventArgs e)
        {
            try
            {
                Server.ResumeProcesses(Admin.Login, Admin.Password, Login,
                                                 (from ProcessContainer process in ProcessGrid.SelectedItems
                                                  select process.Id).ToArray());
            }

            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
