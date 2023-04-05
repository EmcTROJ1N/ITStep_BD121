using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using System.Text;

namespace ChatService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "ServerService" в коде и файле конфигурации.


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode =ConcurrencyMode.Multiple)]
    public class ServerService : IService
    {
        public List<User> ConnectedUsers { get; set; } = new List<User>();
        public List<User> GetUsers() => ConnectedUsers;
        public bool AddClient(string login)
        {
            if ((from user in ConnectedUsers select user.Login).Contains(login)) throw new FaultException("This user already logged");
            else ConnectedUsers.Add(new User(login));
            return true;
        }

        public bool CloseConnection(string loginFrom, string loginTo)
        {
            List<User> usersFrom = (from user in ConnectedUsers 
                             where loginFrom == user.Login 
                             select user).ToList();
            
            List<User> usersTo = (from user in ConnectedUsers 
                             where loginTo == user.Login 
                             select user).ToList();
            if (usersFrom.Count == 0 || usersTo.Count == 0) throw new FaultException("Invalid login");

            User userTo = usersTo[0];
            User userFrom = usersFrom[0];

            if (userTo.ChatsListViewSource.Contains(loginFrom) == false ||
                userFrom.ChatsListViewSource.Contains(loginTo) == false) throw new FaultException("Connection does not exist");

            userTo.Chats.Remove(loginFrom);
            userTo.ChatsListViewSource.Remove(loginFrom);
            userFrom.Chats.Remove(loginTo);
            userFrom.ChatsListViewSource.Remove(loginTo);

            userTo.Callback.UpdateUI(userTo);
            userFrom.Callback.UpdateUI(userFrom);
            
            return true;
        }

        public bool OpenConnection(string loginFrom, string loginTo)
        {
            List<User> usersFrom = (from user in ConnectedUsers 
                             where loginFrom == user.Login 
                             select user).ToList();
            
            List<User> usersTo = (from user in ConnectedUsers 
                             where loginTo == user.Login 
                             select user).ToList();
            if (usersFrom.Count == 0 || usersTo.Count == 0) throw new FaultException("Invalid login");

            User userTo = usersTo[0];
            User userFrom = usersFrom[0];

            if (userTo.ChatsListViewSource.Contains(loginFrom) ||
                userFrom.ChatsListViewSource.Contains(loginTo)) throw new FaultException("Connection already opened");

            userTo.Chats.Add(loginFrom, new ObservableCollection<string>());
            userTo.ChatsListViewSource.Add(loginFrom);

            if (loginFrom != loginTo)
            {
                userFrom.Chats.Add(loginTo, new ObservableCollection<string>());
                userFrom.ChatsListViewSource.Add(loginTo);
            }

            //Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
            //Callback.UpdateUI();
            userTo.Callback.UpdateUI(userTo);
            userFrom.Callback.UpdateUI(userFrom);

            return true;

        }

        public bool RemoveClient(string login)
        {
            if ((from user in ConnectedUsers select user.Login).Contains(login) == false) 
                throw new FaultException<string>("Invalid login");
            else ConnectedUsers.Add(new User(login));
            return true;
        }

        public bool SendTextMessage(string loginFrom, string loginTo, string message)
        {
            List<User> usersFrom = (from user in ConnectedUsers 
                             where loginFrom == user.Login 
                             select user).ToList();
            
            List<User> usersTo = (from user in ConnectedUsers 
                             where loginTo == user.Login 
                             select user).ToList();
            if (usersFrom.Count == 0 || usersTo.Count == 0) throw new FaultException("Invalid login");

            User userTo = usersTo[0];
            User userFrom = usersFrom[0];

            if (userTo.ChatsListViewSource.Contains(loginFrom) == false ||
                userFrom.ChatsListViewSource.Contains(loginTo) == false) throw new FaultException("Connection not opened");

            userTo.Chats[loginFrom].Add($"{loginFrom}: {message}");
            userFrom.Chats[loginTo].Add($"{loginFrom}: {message}");

            userTo.Callback.UpdateUI(userTo);
            userFrom.Callback.UpdateUI(userFrom);

            return true;
        }
    }
}
