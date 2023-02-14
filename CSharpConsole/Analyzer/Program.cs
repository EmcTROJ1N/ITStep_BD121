using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Analyzer analitic = new Analyzer();
            Class cl = new Class();
            Random rand = new Random();

            object test = cl;

            analitic.Analyze(test, rand);

        }
    }
}