abstract class Compiler
{
    public abstract void Compile(IEnumerable<string?> lst);
}


class ConcreteCompiler : Compiler
{
    public override void Compile(IEnumerable<string?> lst)
    {
        System.Console.WriteLine("Code compilation");
    }
}

class ProxyCompiler : Compiler
{
    public override void Compile(IEnumerable<string?> lst)
    {
        System.Console.WriteLine("Code compilation by other compiler");
    }
}