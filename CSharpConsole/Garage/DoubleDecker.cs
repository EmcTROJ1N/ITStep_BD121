class DoubleDecker : Bus
{
    public DoubleDecker(int weight, int speed, int price, int countSits)
    {
        Weight = weight;
        Speed = speed;
        Price = price;
        CountSits = countSits;
    }

    public override void Print()
    {
        System.Console.WriteLine($"Weight: {Weight}");
        System.Console.WriteLine($"Count sits: {CountSits}");
        System.Console.WriteLine($"Speed: {Speed}");
        System.Console.WriteLine($"Price: {Price}");
    }
}