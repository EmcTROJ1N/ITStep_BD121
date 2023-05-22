using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.ServiceModel;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using UserClientApp.ServiceReference;


namespace UserClientApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    [CallbackBehavior(UseSynchronizationContext = false)]
    public class UserCallback : IUserServiceCallback
    {
        MainWindow Root;
        PowerShell Shell;

        public UserCallback(MainWindow root) =>
            Root = root;

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
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport("AlgLibrary", SetLastError = true, EntryPoint = "RenameRegistryKey",
            CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe uint RenameRegistryKey(
            [MarshalAs(UnmanagedType.LPWStr)] string keyPath,
            [MarshalAs(UnmanagedType.LPWStr)] string newName);

        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        [DllImport("Powrprof.dll", SetLastError = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);


        
        public void LockStationCallback()
        {
            try { LockWorkStation(); }
            catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void SuspendStationCallback(bool toHibernate)
        {
            try { SetSuspendState(toHibernate, true, true); }
            catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public ProcessContainer[] GetProcessesCallback(string filter)
        {
            List<Process> collection = Process.GetProcesses().ToList();

            collection = (from process in collection
                          where process.ProcessName.ToLower().Contains(filter.ToLower())
                          select process).ToList();

            return (from process in collection
                    select new ProcessContainer()
                    {
                        Name = process.ProcessName,
                        Id = process.Id,
                        WindowTitle = process.MainWindowTitle,
                        _PagedMemory = process.PagedMemorySize64,
                        Responding = process.Responding
                    }).ToArray();
        }

        public void ResumeProcessesCallback(int[] pids)
        {
            foreach (int pid in pids)
            {
                foreach (ProcessThread thread in Process.GetProcessById(pid).Threads)
                {
                    var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                    if (pOpenThread == IntPtr.Zero) break;
                    ResumeThread(pOpenThread);
                }
            }
        }
        
        public void SuspendProcessesCallback(int[] pids)
        {
            foreach (int pid in pids)
            {
                foreach (ProcessThread thread in Process.GetProcessById(pid).Threads)
                {
                    var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                    if (pOpenThread == IntPtr.Zero) break;
                    SuspendThread(pOpenThread);
                }
            }
        }

        public void TerminateProcessesCallback(int[] pids)
        {
            foreach (int pid in pids)
                Process.GetProcessById(pid).Kill();
        }

        public void SendMessageBoxCallback(string msg) =>
            MessageBox.Show(msg);

        public void BeginFindFiles(string path, string mask) =>
            Task.Run(() => FindFiles(path, mask));
        
        public void FindFiles(string path, string mask)
        {
           ResourcesSearchingStatus status = Root.Server.GetSearchingStatus(Root.CurrentUser.ConnectorID);
            if (status == ResourcesSearchingStatus.Completed) return;
            if (status == ResourcesSearchingStatus.Paused)
            {
                while (true)
                {
                    status = Root.Server.GetSearchingStatus(Root.CurrentUser.ConnectorID);
                    if (status != ResourcesSearchingStatus.Paused)
                        break;
                    Thread.Sleep(300);
                }
            }

            string[] files = Array.Empty<string>();
            try { files = Directory.GetFiles(path, mask); }
            catch (UnauthorizedAccessException e) { return; }

            bool flag = true;

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                try
                {
                    Root.Server.SendFile(new FileContainer()
                    {
                        Name = fileInfo.Name,
                        FullName = fileInfo.FullName,
                        IsReadOnly = fileInfo.IsReadOnly,
                        LastAccessTime = fileInfo.LastAccessTime,
                        LastWriteTime = fileInfo.LastWriteTime,
                        Length = fileInfo.Length
                    }, Root.CurrentUser.ConnectorID);
                }
                catch (CommunicationException ex) { flag = false; }
            }
            if (flag == false)
                return;

            foreach (string folder in Directory.GetDirectories(path))
                FindFiles(folder, mask);
        }

        public void StartProcessCallback(string path, string args = null)
        {
            try { Process.Start(path, args); }
            catch (Exception ex) { throw new FaultException(ex.Message); }
        }
        public FileContainer[] GetFilesCallback(string path)
        {
            try
            {
                return (from fileInfo in
                             (from file in Directory.GetFiles(path)
                              select new FileInfo(file))
                        select new FileContainer
                        {
                            Name = fileInfo.Name,
                            FullName = fileInfo.FullName,
                            IsReadOnly = fileInfo.IsReadOnly,
                            LastAccessTime = fileInfo.LastAccessTime,
                            LastWriteTime = fileInfo.LastWriteTime,
                            Length = fileInfo.Length
                        }).ToArray();
            }
            catch { return new FileContainer[0]; }
        }

        public FolderContainer[] GetFoldersCallback(string path)
        {
            try
            {
                return (from folderInfo in
                            (from file in Directory.GetDirectories(path)
                             select new DirectoryInfo(file))
                        select new FolderContainer
                        {
                            Name = folderInfo.Name,
                            FullName = folderInfo.FullName,
                            CreationTime = folderInfo.CreationTime,
                            Files = GetFilesCallback(path),
                        }).ToArray();
            }
            catch { return new FolderContainer[0]; }
        }

        public void DeleteObjectCallback(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                else if (File.Exists(path))
                    File.Delete(path);
                else
                    throw new FaultException("Invalid path");
            } catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void RenameObjectCallback(string path, string newName)
        {
            string newObjectPath = Path.Combine(Path.GetDirectoryName(path), newName);
            try
            {
                if (Directory.Exists(path))
                    Directory.Move(path, newObjectPath);
                else if (File.Exists(path))
                    File.Move(path, newObjectPath);
                else
                    throw new FaultException("Invalid path");
            }
            catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void CopyObjectCallback(string fromPath, string toPath)
        {
            try
            {
                if (Directory.Exists(fromPath))
                {
                    Directory.CreateDirectory(toPath);
                    foreach (string file in Directory.GetFiles(fromPath))
                    {
                        string fileName = Path.GetFileName(file);
                        string destFile = Path.Combine(toPath, fileName);
                        File.Copy(file, destFile, true);
                    }
                    foreach (string folder in Directory.GetDirectories(fromPath))
                    {
                        string folderName = Path.GetFileName(folder);
                        string destFolder = Path.Combine(toPath, folderName);
                        CopyObjectCallback(folder, destFolder);
                    }
                    return;
                }
                else if (File.Exists(fromPath))
                    File.Copy(fromPath, toPath);
                else throw new Exception("Object not found");
            }
            catch (Exception e)
            {
                throw new FaultException($"The process failed: {e.Message}");
            }
        }

       
        RegistryKey GetKey(string path, bool write = false)
        {
            string RootKey = path.Split(new char[] { '/', '\\' }).First();
            string pathWithoutRoot = String.Join("\\", path.Split(new char[] { '/', '\\' }).Skip(1));
            RegistryKey key;
            switch (RootKey)
            {
                case "HKEY_CLASSES_ROOT": key = Registry.ClassesRoot.OpenSubKey(pathWithoutRoot, write); break;
                case "HKEY_CURRENT_USER": key = Registry.CurrentUser.OpenSubKey(pathWithoutRoot, write); break;
                case "HKEY_LOCAL_MACHINE": key = Registry.LocalMachine.OpenSubKey(pathWithoutRoot, write); break;
                case "HKEY_USERS": key = Registry.Users.OpenSubKey(pathWithoutRoot, write); break;
                case "HKEY_CURRENT_CONFIG": key = Registry.CurrentConfig.OpenSubKey(pathWithoutRoot, write); break;
                default: throw new Exception("Invalid path");
            }
            return key;
        }
        public RegistryKeyContainer[] GetRegistryKeysCallback(string path = null)
        {

            T CheckCanSerialize<T>(T returnValue)
            {
                var lDCS = new System.Runtime.Serialization.DataContractSerializer(typeof(T));

                Byte[] lBytes;
                using (var lMem1 = new MemoryStream())
                {
                    lDCS.WriteObject(lMem1, returnValue);
                    lBytes = lMem1.ToArray();
                }

                T lResult;
                using (var lMem2 = new MemoryStream(lBytes))
                {
                    lResult = (T)lDCS.ReadObject(lMem2);
                }

                return lResult;
            }

            if (path == null)
            {
                RegistryKey[] RootKeys =
                {
                    Registry.ClassesRoot,
                    Registry.CurrentUser,
                    Registry.LocalMachine,
                    Registry.Users,
                    Registry.CurrentConfig
                };

                RegistryKeyContainer[] collec = (from key in RootKeys
                                                 select new RegistryKeyContainer
                                                 {
                                                     Name = key.Name,
                                                     FullName = key.Name,
                                                     SubKeys = key.GetSubKeyNames(),
                                                     Values = new ValueContainer[0]
                                                 }).ToArray();

                RegistryKeyContainer[] finalContainer = CheckCanSerialize<RegistryKeyContainer[]>(collec);
                return finalContainer;
            }
            else
            {
                RegistryKey ParentKey;
                try { ParentKey = GetKey(path); }
                catch (Exception ex) { throw new FaultException(ex.Message); }

                List<RegistryKeyContainer> keys = new List<RegistryKeyContainer>();
                foreach (string key in ParentKey.GetSubKeyNames())
                {
                    RegistryKeyContainer containerKey = new RegistryKeyContainer
                    {
                        Name = key,
                        FullName = Path.Combine(path, key),
                    };
                    try
                    {
                        containerKey.SubKeys = ParentKey.OpenSubKey(key).GetSubKeyNames();
                        ValueContainer[] values = GetRegistryKeyValuesCallback(containerKey.FullName);
                        foreach (ValueContainer value in values)
                            value.Parent = containerKey;
                        containerKey.Values = values;
                    }
                    catch (SecurityException) { continue; }
                    keys.Add(containerKey);
                }
                return CheckCanSerialize<RegistryKeyContainer[]>(keys.ToArray());
            }
        }

        public ValueContainer[] GetRegistryKeyValuesCallback(string path)
        {
            RegistryKey ParentKey;
            try { ParentKey = GetKey(path); }
            catch (Exception ex) { throw new FaultException(ex.Message); }
            List<ValueContainer> values = new List<ValueContainer>();
            foreach (string key in ParentKey.GetValueNames())
            {
                try
                {
                    values.Add(new ValueContainer
                    {
                        Name = key,
                        Type = ParentKey.GetValueKind(key),
                        Value = ParentKey.GetValue(key)
                    });
                }
                catch { continue; }
            }
            return values.ToArray();
        }

        public void RenameRegistryKeyValueCallback(string path, string name, string newName)
        {
            try
            {
                RegistryKey key = GetKey(path, true);
                key.SetValue(newName, key.GetValue(name));
                key.DeleteValue(name);
            }
            catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void RenameRegistryKeyCallback(string path, string newName)
        {
            try
            {
                uint res = RenameRegistryKey(path, newName);
                if (res != 0)
                    throw new Exception("Something went wrong");
            } catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void DeleteRegistryKeyValueCallback(string path, string valueName)
        {
            try
            {
                RegistryKey key = GetKey(path, true);
                key.DeleteValue(valueName);
            } catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public void DeleteRegistryKeyCallback(string path)
        {
            string[] arr = path.Split(new char[] { '/', '\\' });
            string keyForDeleting = arr.Last();
            string pathWithoutKey = String.Join("\\", arr.Take(arr.Length - 1));
            try
            {
                RegistryKey key = GetKey(pathWithoutKey, true);
                key.DeleteSubKeyTree(keyForDeleting);
            } catch (Exception ex) { throw new FaultException(ex.Message); }
        }

        public string[] GetLogicalDrivesCallback()
        {
            string[] drives;
            try { drives =Environment.GetLogicalDrives(); }
            catch (Exception ex) { throw new FaultException(ex.Message); }
            return drives;
        }
    
        public string[] SendCommandCallback(string command)
        {
            if (command == "init")
                Shell = PowerShell.Create();
            if (Shell == null)
                throw new FaultException("Shell is not opened");
            Shell.AddScript(command);
            Collection<PSObject> collec = Shell.Invoke();

            return (from psobj in Shell.Invoke()
                    select psobj.ToString()).ToArray();
        }
    
        
        public byte[] GetScreenshotCallback()
        {
            System.Drawing.Bitmap screenshotBitmap = new System.Drawing.Bitmap(
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(screenshotBitmap))
                graphics.CopyFromScreen(0, 0, 0, 0, screenshotBitmap.Size);
            BitmapData bitmapData = screenshotBitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, screenshotBitmap.Width, screenshotBitmap.Height),
                ImageLockMode.ReadOnly,
                screenshotBitmap.PixelFormat);
            BitmapSource screenshotBitmapSource = BitmapSource.Create(
                bitmapData.Width,
                bitmapData.Height,
                screenshotBitmap.HorizontalResolution,
                screenshotBitmap.VerticalResolution,
                PixelFormats.Bgra32,
                null,
                bitmapData.Scan0,
                bitmapData.Stride * bitmapData.Height,
                bitmapData.Stride);
            screenshotBitmap.UnlockBits(bitmapData);
            
            byte[] bytes;
            using (MemoryStream stream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(screenshotBitmapSource));
                encoder.Save(stream);
                bytes = stream.ToArray();
            }
            return bytes;
        }
    }

    public partial class MainWindow : Window
    {
        internal UserServiceClient Server;
        InstanceContext Context;
        internal User CurrentUser;
        WindowState PrevState;

        internal DispatcherTimer CheckConnectionTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            Context = new InstanceContext(new UserCallback(this));
            Server = new UserServiceClient(Context);
            CheckConnectionTimer.Interval = TimeSpan.FromSeconds(5);
            CheckConnectionTimer.Tick += CheckConnectionTimer_Tick;
            CheckConnectionTimer.Start();

            using (Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("Collective user");
                if (task == null)
                {
                    MessageBoxResult result = MessageBox.Show("You must add the program to the autostart to keep it running smoothly. To do this?",
                                "Choose", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        Microsoft.Win32.TaskScheduler.TaskDefinition autorunDefinition = ts.NewTask();
                        autorunDefinition.RegistrationInfo.Description = "sets the custom client program \"collective\" to the autostart";
                        autorunDefinition.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(Assembly.GetExecutingAssembly().Location, null, null));

                        autorunDefinition.Principal.RunLevel = Microsoft.Win32.TaskScheduler.TaskRunLevel.Highest;
                        autorunDefinition.Settings.AllowDemandStart = true;
                        autorunDefinition.Settings.DisallowStartIfOnBatteries = false;
                        autorunDefinition.Settings.StopIfGoingOnBatteries = false;
                        autorunDefinition.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                        autorunDefinition.Triggers.Add(new Microsoft.Win32.TaskScheduler.LogonTrigger());

                        ts.RootFolder.RegisterTaskDefinition("Collective user", autorunDefinition);
                    }
                }
                else
                {
                    Microsoft.Win32.TaskScheduler.Action action = task.Definition.Actions.FirstOrDefault();
                    if (action == null || action is Microsoft.Win32.TaskScheduler.ExecAction == false ||
                    (action as Microsoft.Win32.TaskScheduler.ExecAction).Path != Assembly.GetExecutingAssembly().Location)
                    {
                        task.Definition.Actions.Clear();
                        task.Definition.Actions.Add(new Microsoft.Win32.TaskScheduler.ExecAction(Assembly.GetExecutingAssembly().Location, null, null));
                        ts.RootFolder.RegisterTaskDefinition(task.Name, task.Definition);
                    }

                    this.Hide();
                    Icon.Visibility = Visibility.Visible;
                }
            }

            if (File.Exists("authData.log"))
            {
                string login = File.ReadAllText("authData.log");
                Task.Run(() => LogIn(login));
            }
        }

        public bool LogIn(string login)
        {
            try
            {
                CurrentUser = Server.LogInUser(login);
                string connectedWith = Server.GetAdministratorById(CurrentUser.ConnectorID).Login;
                Dispatcher.Invoke(() =>
                {
                    LoginTextBox.Text = $"You login: {CurrentUser.Login}";
                    ConnectorIDTextBox.Text = $"Connector ID: {CurrentUser.ConnectorID}";
                    ConnectionWithItem.Header = $"Connected with: {connectedWith}";
                    ConnectionStatusItem.Header = "Connection status: Connected";
                });
            }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); return false; }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); return false; }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
            return true;
        }

        public void LogOut()
        {
            try { Server.LogOutUser(CurrentUser.Login); }
            catch (FaultException ex) { MessageBox.Show(ex.Reason.ToString()); }
            catch (CommunicationException ex) { MessageBox.Show(ex.Message); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            ConnectorIDTextBox.Text = "Connector ID: ";
            LoginTextBox.Text = "Your login: ";
            ConnectionWithItem.Header = $"Connected with: ";
            ConnectionStatusItem.Header = "Connection status: Disconnected";
            CurrentUser = null;
        }

        private async void CheckConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (Server.State == CommunicationState.Faulted)
            {
                Server = new UserServiceClient(Context);
                await Task.Run(() =>
                {
                    try { Server.Open(); }
                    catch (CommunicationException) { return; }
                    catch (TimeoutException) { return; }
                    if (File.Exists("authData.log"))
                    {
                        string login = File.ReadAllText("authData.log");
                        Task.Run(() => LogIn(login));
                    }
                });
            }
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            LogInForm form = new LogInForm(this);
            form.ShowDialog();
        }

        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            if (Server.State == CommunicationState.Closed && Server.State == CommunicationState.Closing)
                MessageBox.Show("Connection already closed");
            else if (CurrentUser == null)
                MessageBox.Show("You are already logout");
            else LogOut();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            RegisterForm form = new RegisterForm(this);
            form.ShowDialog();
        }
        
        private void Icon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            this.Show();
            Icon.Visibility = Visibility.Hidden;
            this.WindowState = PrevState;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PrevState = this.WindowState;
            this.Hide();
            Icon.Visibility = Visibility.Visible;
            e.Cancel = true;
        }

        private void IconClose(object sender, RoutedEventArgs e)
        {
            Icon.Visibility = Visibility.Hidden;
            Task.Run(() => Environment.Exit(0));
        }

        private void StartUpClick(object sender, RoutedEventArgs e)
        {
            using (Microsoft.Win32.TaskScheduler.TaskService ts = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                // Получение задачи по имени
                Microsoft.Win32.TaskScheduler.Task task = ts.GetTask("Collective user");
                if (task == null)
                    MessageBox.Show("Program is not in startup");
                else
                    ts.RootFolder.DeleteTask(task.Name);
            }
        }
    }
}
