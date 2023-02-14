abstract class Decorator : Algorythm
{
    public Algorythm? Alg { get; protected set; } = null;

    public override void Pack() { Alg?.Pack(); }
    public override void UnPack() { Alg?.UnPack(); }
}

class SFXPack : Decorator
{
    public SFXPack(Algorythm alg) { Alg = alg; }

    public override void Pack()
    {
        base.Pack();
        System.Console.WriteLine("SFX packing alg");
    }

    public override void UnPack()
    {
        base.UnPack();
        System.Console.WriteLine("SFX unpacking alg");
    }
}

class TurboPacking : Decorator
{
    public TurboPacking(Algorythm alg) { Alg = alg; }

    public override void Pack()
    {
        base.Pack();
        System.Console.WriteLine("Turbo packing alg");
    }

    public override void UnPack()
    {
        base.UnPack();
        System.Console.WriteLine("Turbo unpacking alg");
    }
}