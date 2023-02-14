class SpyFolder
{
    DirectoryInfo? State;
    Folder _Folder;

    public SpyFolder(Folder folder) =>
        _Folder = folder;
    public void Update(FileInfo file)
    {
        State = _Folder._DirectoryInfo;
        System.Console.WriteLine("Add new file");
        Antivirus.Instance().ScanFile(file.FullName);
    }
}

// еще один адаптер
class Folder
{
    List<SpyFolder> Spies = new List<SpyFolder>();
    public DirectoryInfo _DirectoryInfo { get; set; }
    public List<string> DumpFiles { get; set; }
    public List<string> CurrentFiles
    {
        get
        {
            List<string> lst = new List<string>();
            foreach (FileInfo file in _DirectoryInfo.GetFiles())
                lst.Add(file.FullName);
            return lst;
        }
    }
    public Folder(DirectoryInfo directoryInfo)
    {
        _DirectoryInfo = directoryInfo;
        DumpFiles = new List<string>(CurrentFiles);
    }
    
    public void Attach(SpyFolder source) => Spies.Add(source);
    public void Detach(SpyFolder source) => Spies.Remove(source);

    public void Notify(FileInfo file)
    {
        foreach (SpyFolder item in Spies)
            item.Update(file);
    }

}