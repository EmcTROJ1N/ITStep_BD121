abstract class SubSystem
{
    public abstract void Accept(Visitor visitor);
}

class Encrypt : SubSystem
{
    public override void Accept(Visitor visitor) =>
        visitor.CryptAlg = "Hash";
}

class Decrypt : SubSystem
{
    public override void Accept(Visitor visitor) =>
        visitor.CryptAlg = "None";
}