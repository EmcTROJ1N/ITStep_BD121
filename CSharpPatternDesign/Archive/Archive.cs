class Archive
{
    public enum Language { Rus, Eng, Pol, Jap }

    List<SubSystem> SubSystems;
    Language Lang;
    public string NameArchive;
    Algorythm DefaultAlg;

    public Archive(Config _params)
    {
        SubSystems = new List<SubSystem>();

        Lang = _params.SetLanguage();
        NameArchive = _params.SetNameArchive();
        DefaultAlg = _params.SetDefaultAlg();
    }

    public void Attach(SubSystem source) =>
        SubSystems.Add(source);
    public void Detach(SubSystem source) =>
        SubSystems.Remove(source);

    public void Accept(Visitor visitor)
    {
        foreach (SubSystem subSystem in SubSystems)
            subSystem.Accept(visitor);
    }
    
    public void Pack() =>
        DefaultAlg.Pack();

    public void UnPack() =>
        DefaultAlg.UnPack();


    public void Pack(Algorythm alg) =>
        DefaultAlg.Pack();

    public void UnPack(Algorythm alg) =>
        alg.Pack();

    public void Display() =>
        System.Console.WriteLine("Show files");

    public void DisplaySettings()
    {
        System.Console.WriteLine("Язык: {0}", Lang);
        System.Console.WriteLine("Arichive name: {0}", NameArchive);
        System.Console.WriteLine("Default algorythm: {0}", DefaultAlg.ToString());
    }
}