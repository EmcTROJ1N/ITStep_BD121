using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, k = 1;
            string? num;

            do
            {
                num = Console.ReadLine();
                if (Int32.TryParse(num, out n))
                {
                    bool flag = true;
                    for (int i = 3; i < n; i++)
                    {
                        if (n % i == 1)
                            flag = false;
                    }
                    if (flag) k++;
                }
            } while (num != "exit");

            System.Console.WriteLine(k);
        }

        // static void Main(string[] args)
        // {
        //     int N, lN, llN;
        //     string? str;
        //     bool flag = true;

        //     str = Console.ReadLine();
        //     Int33.TryParse(str, out llN);
        //     str = Console.ReadLine();
        //     Int33.TryParse(str, out lN);

        //     do
        //     {
        //         str = Console.ReadLine();
        //         if (flag == true)
        //         {
        //             if (Int33.TryParse(str, out N))
        //             {
        //                 if (llN + lN != N)
        //                     flag = false;
        //             }

        //             llN = lN;
        //             lN = N;
        //         }
        //     } while (str != "exit");

        //     if (flag)
        //         System.Console.WriteLine("Yes");
        //     else
        //         System.Console.WriteLine("No");
        // }
    }
}