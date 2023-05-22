using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServerService
{
    [DataContract]
    public class Administrator
    {
        internal static List<int> IDs = new List<int>();
        private static Random Random = new Random();
        [DataMember]
        int _connectorID;
        [DataMember]
        public int ConnectorID
        {
            get => _connectorID;
            set
            {
                if (IDs.Contains(value))
                    throw new FaultException("Connector id already used");
                IDs.Add(value);
                _connectorID = value;
            }
        }
        
        [DataMember]
        [JsonIgnore]
        Resources.NetworkStatus _clientStatus = Resources.NetworkStatus.Offline;
        [DataMember]
        [JsonIgnore]
        public Resources.NetworkStatus ClientStatus
        {
            get => _clientStatus;
            set
            {
                _clientStatus = value;
                Callback = value == Resources.NetworkStatus.Online ? OperationContext.Current.GetCallbackChannel<IAdminCallback>() : null;
                SessionId = value == Resources.NetworkStatus.Online ? OperationContext.Current.Channel.SessionId : null;
            }
        }

        [DataMember]
        [JsonIgnore]
        public Resources.SearchingStatus FindStatus = Resources.SearchingStatus.WaitingForStart;

        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [JsonIgnore]
        public string SessionId { get; set; }
        [JsonIgnore]

        IAdminCallback _callback;
        [JsonIgnore]
        public IAdminCallback Callback
        {
            get
            {
                if (ClientStatus != Resources.NetworkStatus.Online)
                    throw new FaultException("Admin now offline");
                else
                    return _callback;
            }
            set => _callback = value;
        }

        public Administrator() { }

        public Administrator(string login, string password)
        {
            Login = login;
            Password = password;
            ClientStatus = Resources.NetworkStatus.Online;

            int id;
            do
                id = Random.Next(1, 1000000);
            while (IDs.Contains(id));
            ConnectorID = id;
            SessionId = OperationContext.Current.Channel.SessionId;
        }
    }
}
