class Turing : Car
{
    public Turing(int weight, int speed, int price, bool hasLeatherSalon)
    {
        Weight = weight;
        Speed = speed;
        Price = price;
        HasLeatherSalon = hasLeatherSalon;
    }

    public override void Print()
    {
        System.Console.WriteLine($"Weight: {Weight}");
        System.Console.WriteLine($"Has Leather salon: {HasLeatherSalon}");
        System.Console.WriteLine($"Speed: {Speed}");
        System.Console.WriteLine($"Price: {Price}");
    }
}