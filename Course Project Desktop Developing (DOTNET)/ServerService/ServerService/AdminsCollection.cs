using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerService
{
    public class AdminsCollection : ClientsCollection<Administrator>
    {
        public AdminsCollection() { }
        public AdminsCollection(string filename) : base(filename) { }
        public void Remove(string login, string password) =>
            Remove(new Administrator(login, password));

        public bool Contains(string login, string password) =>
                    (from admin in this
                    where admin.Login == login &&
                    admin.Password == password
                    select admin).Count() == 1;

        public bool Contains(string login) =>
                    (from admin in this
                    where admin.Login == login
                    select admin).Count() == 1;

        public Administrator this[string login]
        {
            get
            {
                List<Administrator> clients = (from client in this
                                      where client.Login == login
                                      select client).ToList();
                if (clients.Count == 0) throw new FaultException("Invalid login");
                return clients.First();
            }
        }

        public new Administrator this[int connectorId]
        {
            get
            {
                List<Administrator> clients = (from client in this
                                      where client.ConnectorID == connectorId
                                      select client).ToList();
                if (clients.Count == 0) throw new FaultException("Invalid connector id");
                return clients.First();
            }
        }

        public Administrator this[string login, string password]
        {
            get
            {
                List<Administrator> clients = (from client in this
                                      where client.Login == login &&
                                      client.Password == password
                                      select client).ToList();
                if (clients.Count == 0) throw new FaultException("Invalid login or password");
                return clients.First();
            }
        }

    }
}
