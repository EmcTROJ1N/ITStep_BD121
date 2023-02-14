using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
		Server server = new Server(1000);

		Client1 c1 = new Client1();
        Client2 c2 = new Client2();

        server.Subscribe(c1);
        server.Subscribe(c2);

        server.SpyFolder("/home/omon");

        Console.ReadLine();
    }
}
