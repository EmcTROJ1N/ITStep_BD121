class Class
{
    public void A(int a, int b)
    {
        System.Console.WriteLine($"meth a, params {a} {b}");
    }

    public void B(bool a, bool b)
    {
        System.Console.WriteLine($"meth b, params {a} {b}");
    }

    public void C(double a, double b)
    {
        System.Console.WriteLine($"meth c, params {a} {b}");
    }

    public void D(int a, double b)
    {
        System.Console.WriteLine($"meth d, params {a} {b}");
    }

    public void E()
    {
        System.Console.WriteLine("meth e");
    }

    public void NO(string a)
    {
        System.Console.WriteLine("No");
    }
}