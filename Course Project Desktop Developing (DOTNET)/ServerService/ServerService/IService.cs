using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;

namespace ServerService
{
    public interface IUserCallback
    {
        [OperationContract]
        void SendMessageBoxCallback(string msg);

        [OperationContract]
        ProcessContainer[] GetProcessesCallback(string filter);
        [OperationContract]
        void TerminateProcessesCallback(int[] pids);
        [OperationContract]
        void SuspendProcessesCallback(int[] pids);
        [OperationContract]
        void ResumeProcessesCallback(int[] pids);
        [OperationContract]
        void BeginFindFiles(string path, string mask);
        [OperationContract]
        void StartProcessCallback(string path, string args = null);

        [OperationContract]
        string[] GetLogicalDrivesCallback();
        [OperationContract]
        FolderContainer[] GetFoldersCallback(string path);
        [OperationContract]
        FileContainer[] GetFilesCallback(string path);
        [OperationContract]
        void DeleteObjectCallback(string path);
        [OperationContract]
        void RenameObjectCallback(string path, string newName);
        [OperationContract]
        void CopyObjectCallback(string fromPath, string toPath);
        [OperationContract]
        void LockStationCallback();
        [OperationContract]
        void SuspendStationCallback(bool toHibernate);

        [OperationContract]
        RegistryKeyContainer[] GetRegistryKeysCallback(string path = null);
        [OperationContract]
        ValueContainer[] GetRegistryKeyValuesCallback(string path);
        [OperationContract]
        void RenameRegistryKeyValueCallback(string path, string name, string newName);
        [OperationContract]
        void RenameRegistryKeyCallback(string path, string newName);
        [OperationContract]
        void DeleteRegistryKeyValueCallback(string path, string valueName);
        [OperationContract]
        void DeleteRegistryKeyCallback(string path);
        
        [OperationContract]
        string[] SendCommandCallback(string command);
        [OperationContract]
        byte[] GetScreenshotCallback();
    }

    public interface IAdminCallback
    {
        [OperationContract]
        void UpdateData(List<User> users);
        [OperationContract]
        void GetFile(FileContainer file);
    }

    [ServiceContract(CallbackContract = typeof(IUserCallback))]
    public interface IUserService
    {
        [OperationContract]
        User RegistrateUser(string login, int connectorId);
        [OperationContract]
        User LogInUser(string login);
        [OperationContract]
        void LogOutUser(string login);
        [OperationContract]
        void RemoveUser(string login);
        [OperationContract]
        Administrator GetAdministratorById(int connectorId);
        [OperationContract]
        Administrator GetAdministratorByLog(string login);
        [OperationContract]
        User GetUser(string login);
        [OperationContract]
        void SendFile(FileContainer fileContainer, int connectorId);
        [OperationContract]
        Resources.SearchingStatus GetSearchingStatus(int connectorId);

    }

    
    [ServiceContract(CallbackContract = typeof(IAdminCallback))]
    public interface IAdminService
    {
        [OperationContract]
        void RemoveAdmin(string login, string password);
        [OperationContract]
        Administrator LogInAdmin(string login, string password);
        [OperationContract]
        Administrator RegisterAdmin(string login, string password);
        [OperationContract]
        void LogOutAdmin(string login, string password);
        [OperationContract]
        List<User> GetUsers(Administrator admin);

        [OperationContract]
        void SendMessageBox(string login, string password, string targetLogin, string msg);
        [OperationContract]
        ProcessContainer[] GetProcesses(string login, string password, string targetLogin, string filter = null);
        [OperationContract]
        void TerminateProcesses(string login, string password, string targetLogin, int[] pids);
        [OperationContract]
        void SuspendProcesses(string login, string password, string targetLogin, int[] pids);
        [OperationContract]
        void ResumeProcesses(string login, string password, string targetLogin, int[] pids);

        [OperationContract]
        void BeginSearchFiles(string login, string password, string targetLogin, string path, string mask);
        [OperationContract]
        Resources.SearchingStatus GetSearchingStatus(string login, string password);
        [OperationContract]
        void PauseSearch(string login, string password);
        [OperationContract]
        void ResumeSearch(string login, string password);
        [OperationContract]
        void StopSearch(string login, string password);
        [OperationContract]
        void StartProcess(string login, string password, string targetLogin, string path, string args = null);
        [OperationContract]
        void LockStation(string login, string password, string targetLogin);
        [OperationContract]
        void SuspendStation(string login, string password, string targetLogin, bool toHibernate);

        [OperationContract]
        string[] GetLogicalDrives(string login, string password, string targetLogin);
        [OperationContract]
        FolderContainer[] GetFolders(string login, string password, string targetLogin, string path);
        [OperationContract]
        FileContainer[] GetFiles(string login, string password, string targetLogin, string path);
        [OperationContract]
        void DeleteObject(string login, string password, string targetLogin, string path);
        [OperationContract]
        void RenameObject(string login, string password, string targetLogin, string path, string newName);
        [OperationContract]
        void CopyObject(string login, string password, string targetLogin, string fromPath, string toPath);


        [OperationContract]
        RegistryKeyContainer[] GetRegistryKeys(string login, string password, string targetLogin, string path = null);
        [OperationContract]
        ValueContainer[] GetRegistryKeyValues(string login, string password, string targetLogin, string path);
        [OperationContract]
        void RenameRegistryKeyValue(string login, string password, string targetLogin, string path, string name, string newName);
        [OperationContract]
        void RenameRegistryKey(string login, string password, string targetLogin, string path, string newName);
        [OperationContract]
        void DeleteRegistryKeyValue(string login, string password, string targetLogin, string path, string valueName);
        [OperationContract]
        void DeleteRegistryKey(string login, string password, string targetLogin, string path);

        [OperationContract]
        string[] SendCommand(string login, string password, string targetLogin, string command);
        [OperationContract]
        byte[] GetScreenshot(string login, string password, string targetLogin);

        
        [OperationContract]
        void SendMessageBoxBroadcast(string login, string password, string msg);
        [OperationContract]
        void StartProcessBroadcast(string login, string password, string path, string args = null);
        [OperationContract]
        void LockStationBroadcast(string login, string password);
        [OperationContract]
        void SuspendStationBroadcast(string login, string password, bool toHibernate);
    }
}
