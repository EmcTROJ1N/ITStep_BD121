using AdministratorClientApp.ServiceReference;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для RegeditForm.xaml
    /// </summary>
    public partial class RegeditForm : Window
    {
        ObservableCollection<ValueContainer> ValuesListView = new ObservableCollection<ValueContainer>();
        AdminServiceClient Server;
        Administrator Admin;
        string TargetLogin;

        TreeViewItem SelectedItem;
        DataGridRow SelectedRow;

        public RegeditForm(AdminServiceClient server, Administrator admin, string targetLogin)
        {
            InitializeComponent();
            ValuesDataGrid.ItemsSource = ValuesListView;
            Server = server;
            Admin = admin;
            TargetLogin = targetLogin;
            
            RegistryKeyContainer[] keys;
            try { keys = Server.GetRegistryKeys(Admin.Login, Admin.Password, TargetLogin, null); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; }
            
            foreach (RegistryKeyContainer key in keys)
            {
                TreeViewItem item = new TreeViewItem { Header = key.Name };
                item.Expanded += TreeViewItemExpanded;
                item.Collapsed += Item_Collapsed;
                item.Items.Add(new TreeViewItem());
                item.Tag = key;
                FoldersTree.Items.Add(item);
            }
        }

        private void TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem keyNode = (TreeViewItem)sender;
            if (keyNode.Tag is RegistryKeyContainer)
            {
                RegistryKeyContainer key = keyNode.Tag as RegistryKeyContainer;
                ValuesListView.Clear();
                foreach (ValueContainer value in key.Values)
                    ValuesListView.Add(value);
            }
            e.Handled = true;
        }

        private void RenameClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                DataGridCell cell = ValuesDataGrid.Columns[0].GetCellContent(SelectedRow).Parent as DataGridCell;
                cell.Focus();
                ValuesDataGrid.IsReadOnly = false;
                ValuesDataGrid.BeginEdit();
                return;
            }
            if (SelectedItem != null)
            {
                StringWrapper newValueName = new StringWrapper();
                RenameForm form = new RenameForm(newValueName);
                form.ShowDialog();

                try
                {
                    RegistryKeyContainer key = SelectedItem.Tag as RegistryKeyContainer;
                    Server.RenameRegistryKey(Admin.Login, Admin.Password, TargetLogin, key.FullName, newValueName.Str);
                }
                catch (FaultException ex)
                {
                    MessageBox.Show(ex.Reason.ToString());
                    return;
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                SelectedItem.Header = newValueName.Str;
                SelectedItem = null;
            }
        }

        private void ValuesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            ValueContainer oldValue = e.Row.Item as ValueContainer;
            string newValue = (e.EditingElement as TextBox).Text;

            try { Server.RenameRegistryKeyValue(Admin.Login, Admin.Password, TargetLogin, oldValue.Parent.FullName, oldValue.Name, newValue); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
            oldValue.Name = newValue;
            ValuesDataGrid.CellEditEnding -= ValuesDataGrid_CellEditEnding;
            ValuesDataGrid.IsReadOnly = true;
            ValuesDataGrid.CellEditEnding += ValuesDataGrid_CellEditEnding;
        }


        private void TreeViewItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = ((TreeViewItem)sender);
            RegistryKeyContainer currentKey = item.Tag as RegistryKeyContainer;
            if (currentKey == null)
            {
                MessageBox.Show("Access denied");
                return;
            }
            PathTextBlock.Text = currentKey.FullName;

            
            ValuesListView.Clear();
            ValueContainer[] values = currentKey.Values;
            foreach (ValueContainer valueName in values)
                ValuesListView.Add(valueName);


            RegistryKeyContainer[] keys = new RegistryKeyContainer[0];
            try { keys = Server.GetRegistryKeys(Admin.Login, Admin.Password, TargetLogin, currentKey.FullName); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }
            item.Items.Clear();
            foreach (RegistryKeyContainer key in keys)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = key.Name;
                newItem.Expanded += TreeViewItemExpanded;
                newItem.Collapsed += Item_Collapsed;
                newItem.Selected += TreeViewItemSelected;
                newItem.Items.Add(new TreeViewItem());
                newItem.Tag = key;
                item.Items.Add(newItem);
            }
            e.Handled = true;
        }
        private void Item_Collapsed(object sender, RoutedEventArgs e)
        {
            string name = ((sender as TreeViewItem).Tag as RegistryKeyContainer).Name;
            PathTextBlock.Text = Path.GetDirectoryName(name);
            e.Handled = true;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                ValueContainer currentValue = SelectedRow.Item as ValueContainer;
                try { Server.DeleteRegistryKeyValue(Admin.Login, Admin.Password, TargetLogin, currentValue.Parent.FullName, currentValue.Name); }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }
                
                ValuesListView.Remove(currentValue);
                SelectedRow = null;
            }
            if (SelectedItem != null)
            {
                try { Server.DeleteRegistryKey(Admin.Login, Admin.Password, TargetLogin, (SelectedItem.Tag as RegistryKeyContainer).FullName); }
                catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
                catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }

                if (SelectedItem.Parent is TreeView)
                    FoldersTree.Items.Remove(SelectedItem);
                else if (SelectedItem.Parent is TreeViewItem)
                    ((SelectedItem.Parent) as TreeViewItem).Items.Remove(SelectedItem);
                SelectedItem = null;
            }
        }
        private new void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid)
            {
                DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                if (row != null)
                {
                    SelectedRow = row;
                    SelectedItem = null;
                    return;
                }
            }
            if (sender is TreeView)
            {
                DependencyObject source = e.OriginalSource as DependencyObject;
                while (source != null && !(source is TreeViewItem))
                    source = VisualTreeHelper.GetParent(source);
                TreeViewItem item = source as TreeViewItem;

                if (item != null)
                {
                    SelectedItem = item;
                    SelectedRow = null;
                    return;
                }
            }
        }
    }
}
