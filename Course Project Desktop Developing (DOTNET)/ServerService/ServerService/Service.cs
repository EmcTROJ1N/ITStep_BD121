using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace ServerService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "UserService" в коде и файле конфигурации.

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserService : IUserService
    {
        Administrator IUserService.GetAdministratorById(int connectorId) =>
            Resources.Admins[connectorId];

        Administrator IUserService.GetAdministratorByLog(string login) =>
            Resources.Admins[login];

        Resources.SearchingStatus IUserService.GetSearchingStatus(int connectorId) =>
            Resources.Admins[connectorId].FindStatus;

        User IUserService.GetUser(string login) =>
            Resources.Users[login];

        User IUserService.LogInUser(string login)
        {
            if (Resources.Users.Contains(login) == false)
                throw new FaultException("This account does not exist");
            Resources.Users[login].ClientStatus = Resources.NetworkStatus.Online;
            
            Administrator admin = Resources.Admins[Resources.Users[login].ConnectorID];
            if (admin.ClientStatus == Resources.NetworkStatus.Online)
                admin.Callback.UpdateData(Resources.Users.GetUsers(admin.ConnectorID).ToList());
            
            return Resources.Users[login];
        }
        void IUserService.LogOutUser(string login) => Resources.LogOutUser(login);

        User IUserService.RegistrateUser(string login, int connectorId)
        {
            if (Administrator.IDs.Contains(connectorId) == false)
                throw new FaultException("Invalid connector id");
                Resources.Users.Add(new User(login, connectorId));

            Administrator admin = Resources.Admins[Resources.Users[login].ConnectorID];
            if (admin.ClientStatus == Resources.NetworkStatus.Online)
                admin.Callback.UpdateData(Resources.Users.GetUsers(admin.ConnectorID).ToList());

            return Resources.Users[login];
        }

        void IUserService.RemoveUser(string login)
        {
            if (Resources.Users.Contains(login) == false)
                throw new FaultException("This account does not exist");
            Resources.Users.Remove(Resources.Users[login]);

            Administrator admin = Resources.Admins[Resources.Users[login].ConnectorID];
            if (admin.ClientStatus == Resources.NetworkStatus.Online)
                admin.Callback.UpdateData(Resources.Users.GetUsers(admin.ConnectorID).ToList());
        }

        void IUserService.SendFile(FileContainer fileContainer, int connectorId) =>
            Resources.Admins[connectorId].Callback.GetFile(fileContainer);
    }


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AdminService : IAdminService
    {
        List<User> IAdminService.GetUsers(Administrator admin) =>
            (from user in Resources.Users
             where user.ConnectorID == admin.ConnectorID
             select user).ToList();

        Administrator IAdminService.LogInAdmin(string login, string password)
        {
            Resources.Admins[login, password].ClientStatus = Resources.NetworkStatus.Online;
            Resources.Admins[login].Callback.UpdateData(Resources.Users.GetUsers(Resources.Admins[login].ConnectorID).ToList());
            return Resources.Admins[login];
        }
        void IAdminService.LogOutAdmin(string login, string password) => Resources.LogOutAdmin(login, password);

        Administrator IAdminService.RegisterAdmin(string login, string password)
        {
            if (Resources.Admins.Contains(login))
                throw new FaultException("This login already used");

            Administrator admin = new Administrator(login, password);
            Resources.Admins.Add(admin);
            admin.Callback.UpdateData(Resources.Users.GetUsers(Resources.Admins[login].ConnectorID).ToList());
            return admin;
        }

        void IAdminService.RemoveAdmin(string login, string password) =>
            Resources.Admins.Remove(login, password);

        void IAdminService.SendMessageBox(string login, string password, string targetLogin, string msg)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.SendMessageBoxCallback(msg);
        }
        ProcessContainer[] IAdminService.GetProcesses(string login, string password, string targetLogin, string filter = null)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetProcessesCallback(filter);
        }

        void IAdminService.TerminateProcesses(string login, string password, string targetLogin, int[] pids)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.TerminateProcessesCallback(pids);
        }

        void IAdminService.SuspendProcesses(string login, string password, string targetLogin, int[] pids)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.SuspendProcessesCallback(pids);
        }

        void IAdminService.ResumeProcesses(string login, string password, string targetLogin, int[] pids)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.ResumeProcessesCallback(pids);
        }

        void IAdminService.BeginSearchFiles(string login, string password, string targetLogin, string path, string mask)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            if (Resources.Admins[login].FindStatus != Resources.SearchingStatus.WaitingForStart &&
                Resources.Admins[login].FindStatus != Resources.SearchingStatus.Completed)
                throw new FaultException("the search process has already started");
            Resources.Users[targetLogin].Callback.BeginFindFiles(path, mask);
            Resources.Admins[login].FindStatus = Resources.SearchingStatus.Searching;
        }

        Resources.SearchingStatus IAdminService.GetSearchingStatus(string login, string password) =>
            Resources.Admins[login, password].FindStatus;

        void IAdminService.PauseSearch(string login, string password)
        {
            if (Resources.Admins[login].FindStatus != Resources.SearchingStatus.Searching)
                throw new FaultException("search is not paused, there is nothing to stop");
            Resources.Admins[login, password].FindStatus = Resources.SearchingStatus.Paused;
        }

        void IAdminService.ResumeSearch(string login, string password)
        {
            if (Resources.Admins[login].FindStatus != Resources.SearchingStatus.Paused)
                throw new FaultException("no search is performed, there is nothing to pause");
            Resources.Admins[login, password].FindStatus = Resources.SearchingStatus.Searching;
        }

        void IAdminService.StopSearch(string login, string password)
        {
            if (Resources.Admins[login].FindStatus != Resources.SearchingStatus.Searching)
                throw new FaultException("no search is performed, there is nothing to stop");
            Resources.Admins[login, password].FindStatus = Resources.SearchingStatus.Completed;
        }

        void IAdminService.StartProcess(string login, string password, string targetLogin, string path, string args = null)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.StartProcessCallback(path, args);
        }

        FolderContainer[] IAdminService.GetFolders(string login, string password, string targetLogin, string path)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetFoldersCallback(path);
        }

        FileContainer[] IAdminService.GetFiles(string login, string password, string targetLogin, string path)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetFilesCallback(path);
        }

        void IAdminService.DeleteObject(string login, string password, string targetLogin, string path)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.DeleteObjectCallback(path);
        }

        void IAdminService.RenameObject(string login, string password, string targetLogin, string path, string newName)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.RenameObjectCallback(path, newName);
        }

        void IAdminService.CopyObject(string login, string password, string targetLogin, string fromPath, string toPath)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.CopyObjectCallback(fromPath, toPath);
        }

        RegistryKeyContainer[] IAdminService.GetRegistryKeys(string login, string password, string targetLogin, string path = null)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetRegistryKeysCallback(path);
        }

        ValueContainer[] IAdminService.GetRegistryKeyValues(string login, string password, string targetLogin, string path)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetRegistryKeyValuesCallback(path);
        }

        void IAdminService.RenameRegistryKeyValue(string login, string password, string targetLogin, string path, string name, string newName)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.RenameRegistryKeyValueCallback(path, name, newName);
        }

        void IAdminService.RenameRegistryKey(string login, string password, string targetLogin, string path, string newName)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.RenameRegistryKeyCallback(path, newName);
        }

        void IAdminService.DeleteRegistryKeyValue(string login, string password, string targetLogin, string path, string valueName)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.DeleteRegistryKeyValueCallback(path, valueName);
        }

        void IAdminService.DeleteRegistryKey(string login, string password, string targetLogin, string path)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.DeleteRegistryKeyCallback(path);
        }

        string[] IAdminService.SendCommand(string login, string password, string targetLogin, string command)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.SendCommandCallback(command);
        }

        string[] IAdminService.GetLogicalDrives(string login, string password, string targetLogin)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetLogicalDrivesCallback();
        }

        void IAdminService.LockStation(string login, string password, string targetLogin)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.LockStationCallback();
        }

        void IAdminService.SuspendStation(string login, string password, string targetLogin, bool toHibernate)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            Resources.Users[targetLogin].Callback.SuspendStationCallback(toHibernate);
        }

        byte[] IAdminService.GetScreenshot(string login, string password, string targetLogin)
        {
            if (Resources.Users[targetLogin].ConnectorID != Resources.Admins[login, password].ConnectorID)
                throw new FaultException("This user is not in your group");
            return Resources.Users[targetLogin].Callback.GetScreenshotCallback();
        }

        void IAdminService.SendMessageBoxBroadcast(string login, string password, string msg)
        {
            if (Resources.Admins.Contains(login, password) == false)
                throw new FaultException("Invalid login or password");
            foreach (User user in Resources.Users.GetUsers(Resources.Admins[login].ConnectorID))
                user.Callback.SendMessageBoxCallback(msg);
        }

        void IAdminService.StartProcessBroadcast(string login, string password, string path, string args)
        {
            if (Resources.Admins.Contains(login, password) == false)
                throw new FaultException("Invalid login or password");
            foreach (User user in Resources.Users.GetUsers(Resources.Admins[login].ConnectorID))
                user.Callback.StartProcessCallback(path, args);
        }

        void IAdminService.LockStationBroadcast(string login, string password)
        {
            if (Resources.Admins.Contains(login, password) == false)
                throw new FaultException("Invalid login or password");
            foreach (User user in Resources.Users.GetUsers(Resources.Admins[login].ConnectorID))
                user.Callback.LockStationCallback();
        }

        void IAdminService.SuspendStationBroadcast(string login, string password, bool toHibernate)
        {
            if (Resources.Admins.Contains(login, password) == false)
                throw new FaultException("Invalid login or password");
            foreach (User user in Resources.Users.GetUsers(Resources.Admins[login].ConnectorID))
                user.Callback.SuspendStationCallback(toHibernate);
        }
    }
}
