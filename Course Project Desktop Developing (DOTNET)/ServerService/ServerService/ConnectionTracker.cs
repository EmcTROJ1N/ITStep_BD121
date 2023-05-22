using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    internal class ConnectionTracker : IChannelInitializer
    {
        public void Initialize(IClientChannel channel)
        {
            Console.WriteLine("Client {0} initialized", channel.SessionId);
            channel.Closed += ClientDisconnected;
            channel.Faulted += ClientDisconnected;
        }
        static void ClientDisconnected(object sender, EventArgs e)
        {
            string sessionId = ((IClientChannel)sender).SessionId;
            User user = (from us in Resources.Users
                         where us.SessionId == sessionId
                         select us).FirstOrDefault();
            if (user != null)
            {
                Resources.LogOutUser(user.Login);
                return;
            }
            Administrator admin = (from us in Resources.Admins
                                   where us.SessionId == sessionId
                                   select us).FirstOrDefault();
            if (admin != null)
                Resources.LogOutAdmin(admin.Login, admin.Password);
        }
    }

    class ClientTrackerEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) =>
            endpointDispatcher.ChannelDispatcher.ChannelInitializers.Add(new ConnectionTracker());
        public void Validate(ServiceEndpoint endpoint) { }
    }

    public class CustomBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior() => new ClientTrackerEndpointBehavior();
        public override Type BehaviorType { get => typeof(ClientTrackerEndpointBehavior); }
    }
}
