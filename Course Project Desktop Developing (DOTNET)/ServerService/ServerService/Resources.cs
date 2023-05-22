using System.Linq;

namespace ServerService
{
    public static class Resources
    {
        public enum NetworkStatus { Online, Offline }
        public enum SearchingStatus { WaitingForStart, Searching, Completed, Paused }
        internal static UsersCollection Users { get; set; }
        internal static AdminsCollection Admins { get; set; }
        static Resources()
        {
            Users = new UsersCollection("usersBackup");
            Admins = new AdminsCollection("adminsBackup");
        }
        
        public static void LogOutUser(string login)
        {
            Resources.Users[login].ClientStatus = Resources.NetworkStatus.Offline;

            Administrator admin = Resources.Admins[Resources.Users[login].ConnectorID];
            if (admin.ClientStatus == Resources.NetworkStatus.Online)
                admin.Callback.UpdateData(Resources.Users.GetUsers(admin.ConnectorID).ToList());
        }
        public static void LogOutAdmin(string login, string password) =>
            Resources.Admins[login, password].ClientStatus = Resources.NetworkStatus.Offline;
    }
}
