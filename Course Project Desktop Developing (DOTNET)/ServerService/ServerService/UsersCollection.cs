using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    internal class UsersCollection : ClientsCollection<User>
    {
        public UsersCollection() { }
        public UsersCollection(string filename) : base(filename) { }

        public bool Contains(string login) =>
            (from client in this
             where client.Login == login
             select client).Count() == 1;

        public IList<User> GetUsers(int connectorId) =>
            (from client in this
             where client.ConnectorID == connectorId
             select client).ToList();

        public User this[string login]
        {
            get
            {
                List<User> clients = (from client in this
                                      where client.Login == login
                                      select client).ToList();
                if (clients.Count == 0) throw new FaultException("Invalid login");
                return clients.First();
            }
        }
    }
}
