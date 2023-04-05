using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.IO;


namespace Threads_19_2_23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread? _Thread = null;
        ObservableCollection<int> PrimeNums = new ObservableCollection<int>();
        bool IsPaused = false;
        bool IsStopped = false;

        public MainWindow()
        {
            InitializeComponent();
            lstBoxResults.ItemsSource = PrimeNums;
        }

        bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        delegate void SetPropertyDel(object obj, string property, object value);
        void SetProperty(object obj, string property, object value)
        {
            Dispatcher.Invoke(() =>
            {
                if (obj == null || string.IsNullOrEmpty(property))
                    return;

                Type objType = obj.GetType();
                PropertyInfo? propInfo = objType.GetProperty(property);
                if (propInfo == null || !propInfo.CanWrite)
                    return;
                propInfo.SetValue(obj, Convert.ChangeType(value, propInfo.PropertyType));
            });
        //    if (!Dispatcher.CheckAccess())
        //    {
        //        SetPropertyDel setProp = new SetPropertyDel(SetProperty);
        //        Dispatcher.Invoke(setProp, new object[] { obj, property, value });
        //    }
        //    else
        //    {
        //        if (obj == null || string.IsNullOrEmpty(property))
        //            return;

        //        Type objType = obj.GetType();
        //        PropertyInfo? propInfo = objType.GetProperty(property);
        //        if (propInfo == null || !propInfo.CanWrite)
        //            return;
        //        propInfo.SetValue(obj, Convert.ChangeType(value, propInfo.PropertyType));
        //    }
        }

        delegate object? GetPropertyDel(object obj, string property);
        object? GetProperty(object obj, string property)
        {
            if (!Dispatcher.CheckAccess())
            {
                GetPropertyDel setProp = new GetPropertyDel(GetProperty);
                return Dispatcher.Invoke(setProp, new object[] { obj, property });
            }
            else
            {
                if (obj == null || string.IsNullOrEmpty(property))
                    return null;

                Type objType = obj.GetType();
                PropertyInfo? propInfo = objType.GetProperty(property);
                if (propInfo == null || !propInfo.CanWrite)
                    return null;
                return propInfo.GetValue(obj);
            }
        }

        delegate object? CallMethodDel(object obj, string methodName, params object[] vals);
        object? CallMethod(object obj, string methodName, params object[] vals)
        {
            if (!Dispatcher.CheckAccess())
            {
                CallMethodDel callMeth = new CallMethodDel(CallMethod);
                return Dispatcher.Invoke(callMeth, new object[] { obj, methodName, vals });
            }
            else
            {
                if (obj == null || string.IsNullOrEmpty(methodName))
                    return null;

                Type objType = obj.GetType();
                MethodInfo? methodInfo = objType.GetMethod(methodName);

                if (methodInfo == null)
                    return null;

                return methodInfo.Invoke(obj, vals);
            }
        }

        void findPrimes(object? obj)
        {
            int[]? vals = obj as int[];
            if (vals == null)
                return;

            for (int i = vals[0]; i < vals[1]; i++)
            {
                if (IsStopped) break;
                if (IsPaused)
                {
                    while (true)
                    {
                        if (IsPaused == false)
                            break;
                        Thread.Sleep(100);
                    }
                }
                if (IsPrime(i))
                {
                    CallMethod(PrimeNums, "Add", i);
                    Thread.Sleep(10);
                }
                SetProperty(PrBarPrimeNums, "Value", i);
            }
        }

        private void FindPrimeNums(object sender, RoutedEventArgs e)
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(findPrimes);
            _Thread = new Thread(pts);
            _Thread.IsBackground = true;
            Int32.TryParse(txtBoxFrom.Text, out int from);
            Int32.TryParse(txtBoxTo.Text, out int to);
            PrBarPrimeNums.Minimum = from;
            PrBarPrimeNums.Maximum = to;
            _Thread.Start(new int[] { from, to });
        }
        private void PauseFind(object sender, RoutedEventArgs e) => IsPaused = true;
        private void ResumeFind(object sender, RoutedEventArgs e) => IsPaused = false;
        private void StopFind(object sender, RoutedEventArgs e) => IsStopped = true;
        
        private void BrowseSourceFolder_Click(object sender, RoutedEventArgs e)
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
                SourceFolderTextBox.Text = Path.GetDirectoryName(dialog.FileName);
        }
        private void BrowseDestinationFolder_Click(object sender, RoutedEventArgs e)
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
                DestinationFolderTextBox.Text = Path.GetDirectoryName(dialog.FileName);
        }
        private void CopyFiles_Click(object sender, RoutedEventArgs e)
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(CopyFiles);
            _Thread = new Thread(pts);
            _Thread.IsBackground = true;
            _Thread.Start();
        }

        public void CopyFiles(object? obj)
        {
            string text = (string)GetProperty(SourceFolderTextBox, "Text")!;
            DirectoryInfo sourceDirectory = new DirectoryInfo(text);
            FileInfo[] sourceFiles = sourceDirectory.GetFiles();

            SetProperty(CopyPrBar, "Maximum", sourceFiles.Length);
            // Copy each file to the destination folder

            int i = 0;
            foreach (FileInfo file in sourceFiles)
            {
                string destinationFilePath = Path.Combine((string)GetProperty(DestinationFolderTextBox, "Text")!, file.Name);
                file.CopyTo(destinationFilePath, true);
                SetProperty(CopyPrBar, "Value", ++i);
            }
        }
    }
}
