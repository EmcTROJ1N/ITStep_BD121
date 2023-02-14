using System.Data;
using System;
using static LinkedList;

namespace CSharp
{
    class Program
    {
        static void Main(string[] arr)
        {
            LinkedList lst = new LinkedList();
            for (int i = 0; i < 10; i++)
                lst.Add(i);
            lst.Print();
            lst.Remove(0);
            lst.PrintBack();
        }
    }
}