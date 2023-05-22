using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerService
{
    public class ClientsCollection<T> : List<T>
    {
        string SerializationRegistryValueName;
        public ClientsCollection(string valueName)
        {
            SerializationRegistryValueName = valueName;
            string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            RegistryKey key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{appName}");
            if (key == null || key.GetValue(valueName) == null)
                return;

            try
            {
                string str = key. GetValue(valueName) as string;
                ClientsCollection<T> collec = JsonSerializer.Deserialize<ClientsCollection<T>>(str);
                foreach (T client in collec)
                    base.Add(client);
            } catch { }
        }

        public ClientsCollection() { }

        public new void Add(T client)
        {
            if (this.Contains(client))
                throw new FaultException("You cannot add clients with same logins");
            base.Add(client);
            Serialize();
        }

        public new void Remove(T client)
        {
            if (this.Contains(client) == false)
                throw new FaultException("This account does not exist");
            base.Remove(client);
            Serialize();
        }

        void Serialize()
        {
            string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            RegistryKey key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{appName}", true);
            if (key == null)
            {
                Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey(appName);
                key = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\{appName}", true);
            }
            key.SetValue(SerializationRegistryValueName, JsonSerializer.Serialize<ClientsCollection<T>>(this));
        }
    }
}
