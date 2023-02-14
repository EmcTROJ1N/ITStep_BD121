using System.Text;
using System.Linq;
delegate void Events(string _event);
delegate void PMsg(string msg);

class Server
{
    public int Count { get; private set; }
    System.Timers.Timer _Timer;
    protected event Events? _Events;
    protected event PMsg? PrivateMessages;

    StreamWriter writer;
    public string Tmp = "";
    List<string> DumpFiles = new List<string>();

    public Server(int interval)
    {
         _Timer = new System.Timers.Timer(interval); 
         writer = new StreamWriter("data.log", false, Encoding.Default);
    }
    ~Server() { writer.Close(); }

    public void Subscribe(IServerable user)
    {
        _Events += user.Event;
        PrivateMessages += user.Messages;
        Count++;
        user.Subscribe();
    }

    public void UnSubscribe(IServerable user)
    {
        _Events -= user.Event;
        PrivateMessages -= user.Messages;
        Count--;
        user.UnSubscribe();
    }

    public void SendEvent(string str)
    {
        _Timer.Enabled = true;
        Tmp = str;
        _Timer.Elapsed += StartEvent;
    }

    public void SendMsg(string str) { PrivateMessages?.Invoke(str); }

    public void SpyFolder(string path)
    {
        List<FileInfo> lst = new DirectoryInfo(path).GetFiles().ToList();
        foreach (var item in lst)
            DumpFiles.Add(item.FullName);

        Tmp = path;
        _Timer.Elapsed += SpyFolderEvent;
        _Timer.Enabled = true;
    }

    private void StartEvent(object sender, System.Timers.ElapsedEventArgs e)
    {
        _Events?.Invoke($"{Tmp} started");
    }

    private void SpyFolderEvent(object sender, System.Timers.ElapsedEventArgs e)
    {
        List<string> currentFiles = new List<string>();
        foreach (var item in new DirectoryInfo(Tmp).GetFiles())
            currentFiles.Add(item.FullName);

        if (DumpFiles?.Count != currentFiles.Count)
        {
            if (currentFiles?.Count > DumpFiles?.Count)
            {
                foreach (var file in DumpFiles)
                    currentFiles?.Remove(file);

                foreach (var file in currentFiles)
                {
                    if (file.EndsWith(".png"))
                        File.Delete(file);
                    SendMsg($"Added file {file}");
                    writer.WriteLine($"Added file {file}");
                }
            }
            else
            {
                foreach (var file in currentFiles)
                    DumpFiles.Remove(file);

                foreach (var file in DumpFiles)
                {
                    SendMsg($"Added file {file}");
                    writer.WriteLine($"Removed file {file}");
                }
            }

            DumpFiles.Clear();
            FileInfo[] lst = new DirectoryInfo(Tmp).GetFiles();
            foreach (var item in lst)
                DumpFiles.Add(item.FullName);
        }
    }

}