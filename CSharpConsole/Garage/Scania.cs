class Scania : Truck
{
    public Scania(int weight, int speed, int price, int maxWeight)
    {
        Weight = weight;
        Speed = speed;
        Price = price;
        MaxWeight = maxWeight;
    }

    public override void Print()
    {
        System.Console.WriteLine($"Weight: {Weight}");
        System.Console.WriteLine($"Max Weight: {MaxWeight}");
        System.Console.WriteLine($"Speed: {Speed}");
        System.Console.WriteLine($"Price: {Price}");
    }
}