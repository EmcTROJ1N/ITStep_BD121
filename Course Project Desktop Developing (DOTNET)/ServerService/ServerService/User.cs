using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text.Json.Serialization;

namespace ServerService
{
    [DataContract]
    public class User
    {

        [DataMember]
        Resources.NetworkStatus _clientStatus;
        [DataMember]
        [JsonIgnore]
        public Resources.NetworkStatus ClientStatus
        {
            get => _clientStatus;
            set
            {
                _clientStatus = value;
                Callback = value == Resources.NetworkStatus.Online ? OperationContext.Current.GetCallbackChannel<IUserCallback>() : null;
                SessionId = value == Resources.NetworkStatus.Online ? OperationContext.Current.Channel.SessionId : null;
            }
        }
        [DataMember]
        public int ConnectorID { get; set; }
        [DataMember]
        public string Login { get; set; }
        [JsonIgnore]
        IUserCallback _callback;
        [JsonIgnore]
        public string SessionId { get; set; }
        [JsonIgnore]
        public IUserCallback Callback
        {
            get
            {
                if (ClientStatus != Resources.NetworkStatus.Online)
                    throw new FaultException("User now offline");
                else
                    return _callback;
            }
            set => _callback = value;
        }

        public User() { }
        public User(string login, int connector)
        {
            Login = login;
            ClientStatus = Resources.NetworkStatus.Online;
            ConnectorID = connector;
            Callback = OperationContext.Current.GetCallbackChannel<IUserCallback>();
            SessionId = OperationContext.Current.Channel.SessionId;
        }

        public override string ToString() => Login;
    }
}
