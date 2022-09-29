using System.Text;
using System.Linq;

class Server
{
    System.Timers.Timer _Timer;
    public List<IServerable> Users { get; private set; } = new List<IServerable>();
    List<string> DumpFiles = new List<string>();
    string Tmp = "";

    public Server(int interval){ _Timer = new System.Timers.Timer(interval); }
    public void Subscribe(IServerable user) { Users.Add(user); }
    public void UnSubscribe(IServerable user) { Users.Remove(user); }

    public void SendMsg(string str)
    {
        foreach (var user in Users)
            user.Messages(str);
    }

    public void SpyFolder(string path)
    {
        List<FileInfo> lst = new DirectoryInfo(path).GetFiles().ToList();
        foreach (var item in lst)
            DumpFiles.Add(item.FullName);

        Tmp = path;
        _Timer.Elapsed += SpyFolderEvent;
        _Timer.Enabled = true;
    }

    private void SpyFolderEvent(object sender, System.Timers.ElapsedEventArgs e)
    {
        List<string> currentFiles = new List<string>();
        foreach (var item in new DirectoryInfo(Tmp).GetFiles())
            currentFiles.Add(item.FullName);

        if (DumpFiles?.Count != currentFiles.Count)
        {
            foreach (var file in DumpFiles)
                currentFiles?.Remove(file);

            foreach (var file in currentFiles)
            {
                System.Console.WriteLine("file found");
                SendMsg(file);
            }
            
            DumpFiles.Clear();
            FileInfo[] lst = new DirectoryInfo(Tmp).GetFiles();
            foreach (var item in lst)
                DumpFiles.Add(item.FullName);
        }
    }


}