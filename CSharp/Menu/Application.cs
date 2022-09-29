using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_MenuDelegates
{
    class Application
    {
        Menu menu = new Menu();

        string? number1;
        string? number2;
       
        int numb1;
        int numb2;
        int result;

        public Application()
        {
            menu.Add('1', "Sum", Sum);
            menu.Add('2', "Defference", Defference);
            menu.Add('3', "Production", Pro);
            menu.Add('4', "Division", Division);

            menu.Add('5', "New item", () => {
                Console.WriteLine("New item");
				Console.ReadKey();
			});

			menu.Add('6', "Say hello", () => {
				Console.WriteLine("Hello world!!!");
				Console.ReadKey();
			});

			menu.Add('0', "Exit", () => Environment.Exit(0));
        }

        public void Run()
        {
            // передача управления меню
            menu.Run();
        }

		public void Sum()
        {
            Console.WriteLine("Enter values:");
            Console.Write($"Number 1: ");
            number1 = Console.ReadLine();
            Console.Write($"Number 2: ");
            number2 = Console.ReadLine();
           
            if (!Int32.TryParse(number1, out numb1) || !Int32.TryParse(number2, out numb2))
                Sum();
            else
            {
                result = numb1 + numb2;
                Console.WriteLine($"Result={result}");
                Console.ReadKey();
            }
        }

        public void Defference()
        {
            Console.WriteLine("Enter values:");
            Console.Write($"Number 1: ");
            number1 = Console.ReadLine();
            Console.Write($"Number 2: ");
            number2 = Console.ReadLine();
           
            if (Int32.TryParse(number1, out numb1) && Int32.TryParse(number2, out numb2))
                result = numb1 - numb2;
            Console.WriteLine($"Result={result}");
            Console.ReadKey();
        }

        public void Pro()
        {

            Console.WriteLine("Enter values:");
            Console.Write($"Number 1: ");
            number1 = Console.ReadLine();
            Console.Write($"Number 2: ");
            number2 = Console.ReadLine();
           
            if (Int32.TryParse(number1, out numb1) && Int32.TryParse(number2, out numb2))
                result = numb1 * numb2;
            Console.WriteLine($"Result={result}");
            Console.ReadKey();
        }

        public void Division()
        {
            Console.WriteLine("Enter values:");
            Console.Write($"Number 1: ");
            number1 = Console.ReadLine();
            Console.Write($"Number 2: ");
            number2 = Console.ReadLine();
          
            if (Int32.TryParse(number1, out numb1) && Int32.TryParse(number2, out numb2))
            {
                if (numb2 != 0)
                {
                    result = numb1 / numb2;
                    Console.WriteLine($"Result={result}");
                }
                else Console.WriteLine("Error");
            }
            Console.ReadKey();
        }
    }
}
