using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using Path = System.IO.Path;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Drawing;
using SearchPlugin;

namespace Exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = (0x0001),
            SUSPEND_RESUME = (0x0002),
            GET_CONTEXT = (0x0008),
            SET_CONTEXT = (0x0010),
            SET_INFORMATION = (0x0020),
            QUERY_INFORMATION = (0x0040),
            SET_THREAD_TOKEN = (0x0080),
            IMPERSONATE = (0x0100),
            DIRECT_IMPERSONATION = (0x0200)
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        [DllImport("kernel32", CharSet = CharSet.Auto,SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        
    
        private ObservableCollection<DataGridFile> Files = new ObservableCollection<DataGridFile>();
        private ObservableCollection<Process> Processes = new ObservableCollection<Process>();
        private PlugIn FindFilesPlugin;
        private DispatcherTimer ProcessesTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            ProcessesTimer.Interval = TimeSpan.FromSeconds(1);
            ProcessesTimer.Tick += ProcessesTimerOnTick;
            ProcessesTimer.Start();
            ProcesesDataGrid.ItemsSource = Processes;
            
            FindFilesPlugin = new PlugIn(StatusTextBlock, FilesProgress);
            
            CollectionViewSource FileView = new CollectionViewSource { Source = Files };
            FileView.GroupDescriptions.Add(new PropertyGroupDescription("DirectoryName"));
            FilesDataGrid.ItemsSource = FileView.View;
            FolderPath.Text = Environment.CurrentDirectory;
        }

        private void ProcessesTimerOnTick(object? sender, EventArgs e)
        {
            if (RootTab.SelectedIndex != 1) return;

            int selected = ProcesesDataGrid.SelectedIndex;
            Processes.Clear();
            foreach (Process process in Process.GetProcesses())
                Processes.Add(process);
            ProcesesDataGrid.SelectedIndex = selected;
        }


        private void PauseFind(object sender, RoutedEventArgs e) => FindFilesPlugin.IsPaused = true;
        private void ResumeFind(object sender, RoutedEventArgs e) => FindFilesPlugin.IsPaused = false;
        private void StopFind(object sender, RoutedEventArgs e) => FindFilesPlugin.IsStopped = true;
        
        private void StartSearchClick(object sender, RoutedEventArgs e) =>
            FindFilesPlugin.BeginFindFiles(Files, FolderPath.Text, MaskTextBox.Text);

        private void BrowsePath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                DereferenceLinks = true,
                Filter = "Folders|*.none",
                FileName = "Select folder"
            };
            if (dialog.ShowDialog() == true)
                FolderPath.Text = Path.GetDirectoryName(dialog.FileName);
        }

        private void OpenFile(object sender, MouseButtonEventArgs e)
        {
            if (FilesDataGrid.SelectedItem == null) return;
            Process.Start(((DataGridFile)FilesDataGrid.SelectedItem).FullName);
        }

        private void TerminateProcess(object sender, RoutedEventArgs e) =>
            (ProcesesDataGrid.SelectedItem as Process).Kill();

        private void SuspendProcess(object sender, RoutedEventArgs e)
        {
            foreach (ProcessThread thread in (ProcesesDataGrid.SelectedItem as Process).Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero) break;
                SuspendThread(pOpenThread);
            }
        }

        private void ResumeProcess(object sender, RoutedEventArgs e)
        {
            foreach (ProcessThread thread in (ProcesesDataGrid.SelectedItem as Process).Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero) break;
                ResumeThread(pOpenThread);
            }
        }
    }
}