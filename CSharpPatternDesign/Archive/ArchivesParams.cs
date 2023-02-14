abstract class Config
{
    public abstract Archive.Language SetLanguage();
    public abstract string SetNameArchive();
    public abstract Algorythm SetDefaultAlg();
}

class MyConfig : Config
{
    public override Archive.Language SetLanguage() => Archive.Language.Rus;
    public override string SetNameArchive() => "7z елы палы";
    public override Algorythm SetDefaultAlg() => new Algorythm1();
}