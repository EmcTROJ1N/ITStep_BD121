abstract class Visitor
{
    public string CryptAlg { get; set; } = "None";
}


abstract class Algorythm : Visitor
{
    public abstract void Pack();
    public abstract void UnPack();

    public override string ToString() => "Algorythm";
}

class Algorythm1 : Algorythm
{
    public override void Pack()
    {
        System.Console.WriteLine("Pack by alg1");
    }

    public override void UnPack()
    {
        System.Console.WriteLine("Unpacked by alg1");
    }
}

class Algorythm2 : Algorythm
{
    public override void Pack()
    {
        System.Console.WriteLine("Pack by alg2");
    }

    public override void UnPack()
    {
        System.Console.WriteLine("Unpacked by alg2");
    }
}

class NewAlgAdapter : Algorythm
{
    NewAlg _NewAlg;

    public NewAlgAdapter(NewAlg newAlg)
    {
        _NewAlg = newAlg;
    }
    public override void Pack()
    {
        _NewAlg.NewPack();
    }

    public override void UnPack()
    {
        _NewAlg.NewUnpack();
    }
}