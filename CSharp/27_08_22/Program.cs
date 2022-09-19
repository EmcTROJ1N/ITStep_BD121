using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            void task1()
            {
                StreamReader reader = new StreamReader(
                    @"/home/omon/ITStep_PD111/CSharp/27_08/22/test.txt",
                    Encoding.Default);

                string? line = reader.ReadLine();
                int k = 0;
                while (line != null)
                {
                    if (Int32.TryParse(line, out var N))
                        k++;
                    line = reader.ReadLine();
                }
                reader.Close();

                System.Console.WriteLine(k);
            }

            void task2()
            {
                string path1 = "/home/omon/ITStep_PD111/CSharp/27_08_22/in.txt";
                string path2 = "/home/omon/ITStep_PD111/CSharp/27_08_22/out.txt";

                StreamReader reader = new StreamReader(path1, Encoding.Default);
                StreamWriter writer = new StreamWriter(path2, true, Encoding.Default);

                List<string?> lst = new List<string?>();

                string? line = reader.ReadLine();
                lst.Add(line);
                while (line != null)
                {
                    line = reader.ReadLine();
                    lst.Add(line);
                }
                reader.Close();

                lst.Sort();
                lst.Reverse();

                foreach (var item in lst)
                    writer.WriteLine(item);
                writer.Close();
            }

            void task3()
            {
                string path1 = "/home/omon/ITStep_PD111/CSharp/27_08_22/in3.txt";
                string path2 = "/home/omon/ITStep_PD111/CSharp/27_08_22/out3.txt";

                StreamReader reader = new StreamReader(path1, Encoding.Default);
                StreamWriter writer = new StreamWriter(path2, true, Encoding.Default);

                List<string?> lst = new List<string?>();

                string? line = reader.ReadLine();

                while (line != null)
                {
                    lst.Add(line);
                    line = reader.ReadLine();
                }

                int countSpace = 0;

                foreach (var str in lst)
                {
                    string spaces = "";
                    if (str.Contains("{"))
                    {
                        for (int i = 0; i < countSpace; i++)
                            spaces += " ";
                        countSpace += 4;
                        writer.WriteLine(spaces + str);
                        continue;
                    }
                    if (str.Contains("}"))
                    {
                        countSpace -= 4;
                        for (int i = 0; i < countSpace; i++)
                            spaces += " ";
                        writer.WriteLine(spaces + str);
                        continue;
                    }

                    for (int i = 0; i < countSpace; i++)
                        spaces += " ";
                    writer.WriteLine(spaces + str);

                }

                reader.Close();
                writer.Close();
            }

            task3();
        }
    }
}