using AdministratorClientApp.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AdministratorClientApp
{
    /// <summary>
    /// Логика взаимодействия для Explorer.xaml
    /// </summary>
    public partial class Explorer : Window
    {
        ObservableCollection<FileContainer> FilesListView = new ObservableCollection<FileContainer>();
        AdminServiceClient Server;
        Administrator Admin;
        string TargetLogin;

        TreeViewItem SelectedItem;
        DataGridRow SelectedRow;
        string SelectedFileName;
        string PathToObjectForCopy = "";

        public Explorer(AdminServiceClient server, Administrator admin, string targetLogin)
        {
            InitializeComponent();
            FolderDataGrid.ItemsSource = FilesListView;
            Server = server;
            Admin = admin;
            TargetLogin = targetLogin;

            string[] drives;

            try { drives = Server.GetLogicalDrives(Admin.Login, Admin.Password, TargetLogin); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return; }

            foreach (string drive in drives)
            {
                TreeViewItem item = new TreeViewItem { Header= drive };
                item.Expanded += TreeViewItemExpanded;
                item.Collapsed += Item_Collapsed;
                item.Items.Add(new TreeViewItem());
                FolderContainer cont = new FolderContainer { FullName = drive };
                item.Tag = cont;
                FoldersTree.Items.Add(item);
            }
        }

        private void TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem folderNode = (TreeViewItem)sender;
            if (folderNode.Tag is FolderContainer)
            {
                FolderContainer folder = folderNode.Tag as FolderContainer;
                PathTextBlock.Text = folder.FullName;
                FilesListView.Clear();
                foreach (FileContainer file in folder.Files)
                    FilesListView.Add(file);
            }
        }

        private void TreeViewItemExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = ((TreeViewItem)sender);
            FolderContainer folderContainer= item.Tag as FolderContainer;
            PathTextBlock.Text = folderContainer.FullName;
            
            List<FileContainer> files = new List<FileContainer>();
            try { files = Server.GetFiles(Admin.Login, Admin.Password, TargetLogin, PathTextBlock.Text).ToList(); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            
            FilesListView.Clear();
            foreach (FileContainer file in files)
                FilesListView.Add(file);

            List<FolderContainer> folders = new List<FolderContainer>();
            try { folders = Server.GetFolders(Admin.Login, Admin.Password, TargetLogin, PathTextBlock.Text).ToList(); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            
            item.Items.Clear();
            foreach (FolderContainer folder in folders)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = folder.Name;
                newItem.Expanded += TreeViewItemExpanded;
                newItem.Collapsed += Item_Collapsed;
                newItem.Items.Add(new TreeViewItem());
                newItem.Tag = folder;
                item.Items.Add(newItem);
            }
            e.Handled = true;
        }
        private void Item_Collapsed(object sender, RoutedEventArgs e)
        {
            string name = ((sender as TreeViewItem).Tag as FolderContainer).FullName;
            PathTextBlock.Text = System.IO.Path.GetDirectoryName(name);
            e.Handled = true;
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            try
            {

                if (SelectedRow != null)
                {
                    Server.StartProcess(Admin.Login, Admin.Password, TargetLogin, (SelectedRow.Item as FileContainer).FullName, null);
                    SelectedRow = null;
                }
                if (SelectedItem != null)
                {
                    Server.StartProcess(Admin.Login, Admin.Password, TargetLogin, (SelectedItem.Tag as FolderContainer).FullName, null);
                    SelectedItem = null;
                }
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }    
        }

        private void RenameClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                DataGridCell cell = FolderDataGrid.Columns[0].GetCellContent(SelectedRow).Parent as DataGridCell;
                cell.Focus();
                FolderDataGrid.IsReadOnly = false;
                FolderDataGrid.BeginEdit();
                SelectedRow = null;
                return;
            }
            if (SelectedItem != null)
            {
                StringWrapper folderName = new StringWrapper();
                RenameForm form = new RenameForm(folderName);
                form.ShowDialog();

                try
                {
                    FolderContainer folder = SelectedItem.Tag as FolderContainer;
                    Server.RenameObject(Admin.Login, Admin.Password, TargetLogin, folder.FullName, folderName.Str);
                    folder.FullName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(folder.FullName), folderName.Str);
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

                SelectedItem.Header = folderName.Str;
                SelectedItem = null;
            }
        }

        private void CopyClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                PathToObjectForCopy = (SelectedRow.Item as FileContainer).FullName;
                SelectedRow = null;
                return;
            }
            if (SelectedItem != null)
            {
                PathToObjectForCopy = (SelectedItem.Tag as FolderContainer).FullName;
                SelectedItem = null;
                return;
            }
        }

        private void PasteClick(object sender, RoutedEventArgs e)
        {
            if (PathToObjectForCopy == "") return;
            try
            {
                if (Directory.Exists(PathToObjectForCopy))
                {
                    Server.CopyObject(Admin.Login, Admin.Password, TargetLogin, PathToObjectForCopy, Path.Combine(PathTextBlock.Text, Path.GetDirectoryName(PathToObjectForCopy)));
                    TreeViewItem fromFolderItem = GetNode(PathToObjectForCopy);
                    if (fromFolderItem.Parent is TreeViewItem)
                        (fromFolderItem.Parent as TreeViewItem).Items.Remove(fromFolderItem);
                    else if (fromFolderItem.Parent is TreeView)
                        (fromFolderItem.Parent as TreeView).Items.Remove(fromFolderItem);
                    else
                    {
                        MessageBox.Show("Error");
                        return;
                    }
                    TreeViewItem toFolderItem = GetNode(PathTextBlock.Text);
                    toFolderItem.Items.Add(fromFolderItem);
                }
                else if (File.Exists(PathToObjectForCopy))
                {
                    FileInfo fileInfo = new FileInfo(PathToObjectForCopy);
                    Server.CopyObject(Admin.Login, Admin.Password, TargetLogin, PathToObjectForCopy, Path.Combine(PathTextBlock.Text, Path.GetFileName(PathToObjectForCopy)));
                    FilesListView.Add(new FileContainer()
                    {
                        Name = fileInfo.Name,
                        FullName = fileInfo.FullName,
                        IsReadOnly = fileInfo.IsReadOnly,
                        LastAccessTime = fileInfo.LastAccessTime,
                        LastWriteTime = fileInfo.LastWriteTime,
                        Length = fileInfo.Length
                    });
                }
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            SelectedRow = null;
            SelectedItem = null;
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (SelectedRow != null)
            {
                try
                {
                    Server.DeleteObject(Admin.Login, Admin.Password, TargetLogin, (SelectedRow.Item as FileContainer).FullName);
                } catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }

                FilesListView.Remove(SelectedRow.Item as FileContainer);
                SelectedRow = null;
            }
            if (SelectedItem != null)
            {
                try
                {
                    Server.DeleteObject(Admin.Login, Admin.Password, TargetLogin, (SelectedItem.Tag as FolderContainer).FullName);
                } catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return; }

                TreeViewItem item = GetNode((SelectedItem.Tag as FolderContainer).FullName);
                if (item.Parent is TreeView)
                    FoldersTree.Items.Remove(item);
                else if (item.Parent is TreeViewItem)
                    ((item.Parent) as TreeViewItem).Items.Remove(item);
                SelectedItem = null;
            }
        }
        TreeViewItem GetNode(string path, TreeViewItem item = null, List<string> directories = null)
        { 
            List<TreeViewItem> items;
            if (item == null)
            {
                directories = path.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                items = (from TreeViewItem node in FoldersTree.Items
                         where node.Header.ToString().Substring(0, node.Header.ToString().Length - 1) == directories[0]
                         select node).ToList();
            }
            else
            {
                if (directories.Count == 0)
                    return item;
                items = (from TreeViewItem node in item.Items
                         where node.Header.ToString() == directories[0]
                         select node).ToList();
            }
            if (items.Count == 0) return null;

            return GetNode(path, items[0], directories.Skip(1).ToList());
        }
        private new void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid)
            {
                DataGridRow row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;
                if (row != null)
                {
                    SelectedRow = row;
                    SelectedFileName = (SelectedRow.Item as FileContainer).Name;
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
        private void FolderDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (FolderDataGrid.IsReadOnly) return;

            FileContainer file = e.Row.Item as FileContainer;
            string newFilePath = Path.Combine(Path.GetDirectoryName(file.FullName), file.Name);

            try { Server.RenameObject(Admin.Login, Admin.Password, TargetLogin, file.FullName, newFilePath); }
            catch (FaultException ex)
            {
                FolderDataGrid.IsReadOnly = true;
                file.Name = SelectedFileName;
                MessageBox.Show(ex.Reason.ToString());
                return;
            }
            file.FullName = newFilePath;
            FolderDataGrid.IsReadOnly = true;
        }
    }
}
