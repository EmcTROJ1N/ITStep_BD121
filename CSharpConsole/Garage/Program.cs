using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Garage garage = new Garage();
            garage.Add(new Mers(1000, 300, 4000, 1000));
            garage.Add(new PickUP(1000, 300, 4000, true));
            garage.Add(new SingleDecker(1000, 300, 4000, 1000));
            garage.Add(new Sedan(1000, 300, 4000, false));
            garage.Add(new Renault(1000, 300, 4000, 1000));

            garage.Print();
        }
    }
}