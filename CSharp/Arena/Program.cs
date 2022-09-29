using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena(20, 50, 30);
            arena.Run(ConsoleColor.White, ConsoleColor.Blue);
        }
    }
}