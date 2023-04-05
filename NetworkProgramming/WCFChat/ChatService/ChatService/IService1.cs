using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService" в коде и файле конфигурации.
    public interface IClientCallback
    {
        [OperationContract]
        void UpdateUI(User user);
    }


    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IService
    {
        List<User> ConnectedUsers { get; set; }

        [OperationContract]
        List<User> GetUsers();
        
        [OperationContract]
        bool AddClient(string login);
        [OperationContract]
        bool RemoveClient(string login);
        [OperationContract]
        bool CloseConnection(string loginFrom, string loginTo);
        [OperationContract]
        bool OpenConnection(string loginFrom, string loginTo);
        [OperationContract]
        bool SendTextMessage(string loginFrom, string loginTo, string message);
        
        /*[OperationContract]
        void AddHandler(Update func);*/

        // TODO: Добавьте здесь операции служб
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "ChatService.ContractType".
}
