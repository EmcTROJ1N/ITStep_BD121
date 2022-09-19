using System.Text;
using System.Threading.Tasks;
using System;
using static Ticket;
using static Pattern;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Luck(Ticket tic, List<Pattern> patterns)
            {
                foreach (var pattern in patterns)
                {
                    if (pattern.IsLucky(tic))
                        return true;
                }
                return false;
            }

            List<int> luckyTics = new List<int>();
            List<Pattern> patterns = new List<Pattern>();
            patterns.Add(new PattSum());
            patterns.Add(new PattDowner());
            patterns.Add(new AllNumbs());
            patterns.Add(new Repeat());
            patterns.Add(new PattUpper());

            for (int i = 111111; i < 1000000; i++)
            {
                Ticket tic = new Ticket(i);
                if (Luck(tic, patterns))
                    luckyTics.Add(i);
            }

            StreamWriter writer = new StreamWriter("res.txt", false, Encoding.Default);
            foreach (var tic in luckyTics)
                writer.WriteLine(tic);
            writer.Close();
            System.Console.WriteLine($"Lucky tickets: {luckyTics.Count}");
        }
    }
}