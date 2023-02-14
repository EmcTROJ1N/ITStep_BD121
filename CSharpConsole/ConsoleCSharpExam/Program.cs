using System.Runtime.CompilerServices;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Collection collec = new Collection();
        collec += new Interval(1, 3);
        collec += new Interval(5, 6);
        collec += new Interval(1, 3);
        collec += new Interval(4, 5);

        System.Console.WriteLine(collec.HasHoles);

        // string path = "/home/omon";
        // Server server = new Server(1000);

        // Client1 client1 = new Client1();
        // Client2 client2 = new Client2(path);

        // server.Subscribe(client1);
        // server.Subscribe(client2);

        // server.SpyFolder(path);

        // Console.ReadLine();
    }
}