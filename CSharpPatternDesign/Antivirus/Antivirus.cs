using System.Linq;

class Antivirus
{
    static Antivirus? _Antivirus;
    Quarantine<string> quarantine = new Quarantine<string>();
    OS os = new OS();
    Scaner scaner = new Scaner();

    bool Scan = true;
    private Antivirus() {}

    public static Antivirus Instance()
    {
        if (_Antivirus == null)
            _Antivirus = new Antivirus();
        return _Antivirus;
    }

    public void ScanPC()
    {
        string folder = "/home/omon";
        foreach (var file in os.GetDirectories())
            scaner.ScanFile(file);
        scaner.ScanFolder(folder);
    }

    public void SendFileToQuarantine(string name)
    {
        scaner.ScanFile(name);
        quarantine.Add(name);
    }

    public void OnProtect()
    {
        System.Console.WriteLine("Scann aborted");
        System.Console.WriteLine("Protect off");
    }

    public void OffProtect()
    {
        System.Console.WriteLine("Scann started");
        System.Console.WriteLine("Protect on, scan files");
        ScanPC();
    }

}

class Scaner
{
    public void ScanFile(string file)
    {
        System.Console.WriteLine($"{file} scanned");
    }

    public void ScanFolder(string name)
    {
        System.Console.WriteLine($"Foler {name} scanned");
    }
}

class OS
{
    public void GetFileInDirectory(string name)
    {
        System.Console.WriteLine($"File {name} scanned");
    }
    public List<string> GetDirectories()
    {
        return new string[] { "home", "opt", "media" }.ToList();
    }
}


class Quarantine<T>
{
    List<T> Files = new List<T>();
    public void Add(T file)
    {
        Files.Add(file);
    }
    public void Remove(T file)
    {
        Files.Remove(file);
    }
}