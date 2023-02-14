abstract class Warrior : IAttack, IWalk
{
    public void Attack(int x, int y) { System.Console.WriteLine($"Warrior attack {x} {y}"); }
    public void Move() { System.Console.WriteLine("Warrior moved"); }
    public void Defend(int x, int y) { System.Console.WriteLine($"Warrior defend {x} {y}"); }
    public void Retreat() { System.Console.WriteLine("Warriors retreat"); }

    public void Stand() { System.Console.WriteLine("Warrior standed"); }
    public void Walk() { System.Console.WriteLine("Warrior walk"); }
    public void Stop() { System.Console.WriteLine("Warrior stopped"); }
    public void Run() { System.Console.WriteLine("Warrior run"); }
    public void GetCords() { System.Console.WriteLine("Warrior getcords"); }

}

class Turtle : Warrior, IDrivable, IPrintable
{
    public void Drive()
    {
        System.Console.WriteLine("Turtle Drived");
    }

    public void Print()
    {
        System.Console.WriteLine("Leonardo print");
    }
}

class Rino : Warrior, IPrintable
{
    public void Print()
    {
       System.Console.WriteLine("Rino print");
    }
}

class Rat : Warrior, IPrintable
{
    public void Print()
    {
        System.Console.WriteLine("Splinter print");
    }
}

class Fly : Warrior, IFly, IPrintable
{
    public void Print()
    {
        System.Console.WriteLine("Buckster Stockman print");
    }
    public void _Fly()
    {
        System.Console.WriteLine("Stockman fly");
    }

    public void Sit()
    {
        System.Console.WriteLine("Stockman sits");
    }
}