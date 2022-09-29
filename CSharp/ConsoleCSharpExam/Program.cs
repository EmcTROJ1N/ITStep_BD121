using System.Runtime.CompilerServices;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Collection collec = new Collection();
        collec += new Interval(1.1, 2.0);
        collec += new Interval(2.0, 3.5);
        collec += new Interval(3.4, 4.2);

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