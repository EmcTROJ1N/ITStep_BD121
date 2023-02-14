using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Army NinjaTurtles = new Army();
            NinjaTurtles.Append(new Turtle());
            NinjaTurtles.Append(new Rino());
            NinjaTurtles.Append(new Rat());
            NinjaTurtles.Append(new Fly());

            NinjaTurtles.Print();

            NinjaTurtles[0].Attack(1, 1);
            NinjaTurtles[1].Defend(0, 5);
            NinjaTurtles[2].Move();
            NinjaTurtles[3].GetCords();
        }
    }
}