using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SearchPlugin
{
    
    public class DataGridFile
    {
        public BitmapImage FileIcon { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DirectoryName { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Length { get; set; }

        public DataGridFile(FileInfo file)
        {
            Name = file.Name;
            FullName = file.FullName;
            LastAccessTime = file.LastAccessTime;
            Length = file.Length;
            DirectoryName = file.Directory.Name;
            LastWriteTime = file.LastWriteTime;
        }
    }
    
    public class PlugIn
    {
        private Thread? CalculateFolderSizeThread;
        private Thread? FindFilesThread;
        
        public bool IsPaused = false;
        public bool IsStopped = false;

        private ProgressBar FilesProgress;
        private TextBlock StatusTextBlock;

        private ObservableCollection<DataGridFile> Files;
        long _SelectedFolderSize = 0;
        public long SelectedFolderSize
        {
            get => _SelectedFolderSize;
            set
            {
                _SelectedFolderSize = value;
                FilesProgress.Maximum = value;
                StatusTextBlock.Text = $"Bytes checked: {FilesProgress.Value} / {value}";
            }
        }
        
        public void CalculateFolderSize(object? obj)
        {
            string path = (string)obj;
            bool accesOpened = true;
            lock (new object())
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        SelectedFolderSize +=
                            new DirectoryInfo(path).EnumerateFiles("*").Sum(file => file.Length);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        accesOpened = false;
                    }
                });
            }

            if (accesOpened == false) return;
            foreach (string folder in Directory.GetDirectories(path))
                CalculateFolderSize(folder);
        }
        void FindFiles(object? obj)
        {
            if (IsStopped) return;
            if (IsPaused)
            {
               while (true)
               {
                    if (IsPaused == false)
                        break;
                    Thread.Sleep(100);
               }
            }
            
            if (obj is object[] == false) return;
            object[] vals = (object[])obj;
            string folderPath = (string)vals[0];
            string mask = (string)vals[1];
            string[] allFiles = Array.Empty<string>();
            string[] filteredFiles = Array.Empty<string>();

            try
            {
                allFiles = Directory.GetFiles(folderPath);
                filteredFiles = Directory.GetFiles(folderPath, mask);
            }
            catch (UnauthorizedAccessException e) { return; }

            foreach (string file in allFiles)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    FileInfo fileInfo = new FileInfo(file);
                    FilesProgress.Value += fileInfo.Length;
                    StatusTextBlock.Text = $"Bytes checked: {FilesProgress.Value} / {SelectedFolderSize}";
                    if (filteredFiles.Contains(file)) Files.Add(new DataGridFile(fileInfo));
                });
            }
            
            foreach (string folder in Directory.GetDirectories(folderPath))
                FindFiles(new object[] { folder, mask });
        }
        
        public void BeginFindFiles(ObservableCollection<DataGridFile> files, string path, string mask)
        {
            Files = files;
            Files.Clear();
            SelectedFolderSize = 0;
            FilesProgress.Value = 0;

            ParameterizedThreadStart ptsSize = new ParameterizedThreadStart(CalculateFolderSize);
            CalculateFolderSizeThread = new Thread(ptsSize);
            CalculateFolderSizeThread.IsBackground = true;
            CalculateFolderSizeThread.Start(path);
            
            ParameterizedThreadStart ptsFind = new ParameterizedThreadStart(FindFiles);
            FindFilesThread = new Thread(ptsFind);
            FindFilesThread.IsBackground = true;
            FindFilesThread.Start(new object[] { path, mask });
        }

        public PlugIn(TextBlock statusTextBlock, ProgressBar filesProgress)
        {
            StatusTextBlock = statusTextBlock;
            FilesProgress = filesProgress;
        }

    }
}
