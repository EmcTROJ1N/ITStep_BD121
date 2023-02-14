using System.ComponentModel;
class Army
{
    List<Warrior> army;

    public Army()
    {
        army = new List<Warrior>();
    }

    public void Append(Warrior warrior)
    {
        army.Add(warrior);
    }

    public void Print()
    {
        foreach (IPrintable item in army)
            item.Print();
        System.Console.WriteLine();
    }

    public Warrior this[int idx]
    {
        get { return army[idx]; }
    }
}