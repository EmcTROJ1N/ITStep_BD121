class Antivirus
{

    public enum States { Infected, Alright, Quarantine }
    public static Antivirus? _Antivirus;
    List<FileInfo> Quarantine = new List<FileInfo>();
    // List<string> DumpFiles;
    System.Timers.Timer _Timer = new System.Timers.Timer(1000);

    // Это мой обьект, за которым следят
    Folder? _Folder;

    // Эт у нас паттерн стратегия. Посыл такой
    // Задавай любой угодный твоей душе алгоритм - и он будет работать
    public Algorythm AlgorythmStrategy { get; set; }
    Antivirus(Algorythm alg) => AlgorythmStrategy = alg;

    // Эт у нас синглтон, ибо негоже иметь несколько одновременно открытых антивирусов
    public static Antivirus Instance(Algorythm alg)
    {
        if (_Antivirus == null)
            _Antivirus = new Antivirus(alg);
        return _Antivirus;
    }

    public static Antivirus Instance()
    {
        if (_Antivirus == null)
            _Antivirus = new Antivirus(new ESETAlg());
        return _Antivirus;
    }

    public void FindInfected(FileInfo file)
    {
        System.Console.WriteLine("Find infected file, what do you want to do?");
        System.Console.WriteLine("1 - Delete file");
        System.Console.WriteLine("2 - Send to quarantine");
        Int32.TryParse(Console.ReadLine(), out int N);
        switch (N)
        {
            case 1: File.Delete(file.FullName); break;
            case 2:
                file.MoveTo("Quarantine");
                Quarantine.Add(file);
                break;
        }
    }

    public void ScanFile(string path)
    {
        FileInfo file = new FileInfo(path);
        if (Quarantine.Contains(file))
        {
            System.Console.WriteLine("File in quarantine");
            return;
        }
        if (AlgorythmStrategy.Scan(file) == Antivirus.States.Infected)
            FindInfected(file);
        else System.Console.WriteLine("File OK");
    }

    public void ScanFolder(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        foreach (FileInfo file in directoryInfo.EnumerateFiles())
            ScanFile(path);
    }


    public void SpyFolder(string path)
    {
        _Folder = new Folder(new DirectoryInfo(path));
        _Folder.Attach(new SpyFolder(_Folder));

        _Timer.Elapsed += SpyFolderEvent;
        _Timer.Enabled = true;
    }

    void SpyFolderEvent(object? sender, System.Timers.ElapsedEventArgs e)
    {
        if (_Folder?.CurrentFiles.Count != _Folder?.DumpFiles.Count)
        {
            if (_Folder != null)
            {
                List<string> currentFiles = new List<string>(_Folder.CurrentFiles);

                foreach (var file in _Folder.DumpFiles)
                    currentFiles?.Remove(file);

                foreach (var file in currentFiles)
                    _Folder?.Notify(new FileInfo(file));

                _Folder.DumpFiles = new List<string>(_Folder.CurrentFiles);
            }

        }

    }
}