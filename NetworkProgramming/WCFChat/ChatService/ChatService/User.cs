using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public Dictionary<string, ObservableCollection<string>> Chats { get; set; } = new Dictionary<string, ObservableCollection<string>>();
        [DataMember]
        public ObservableCollection<string> ChatsListViewSource { get; set; } = new ObservableCollection<string>();
        [DataMember]
        public IClientCallback Callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();

        public User(string login)
        {
            Login = login;
        }
    }
}
