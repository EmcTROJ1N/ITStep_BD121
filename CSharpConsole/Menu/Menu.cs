using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_MenuDelegates
{
    class Menu
    {
        //список пунктов меню
        public List<MenuItem> items = new List<MenuItem>();

        public void Add(char letter, string title, MenuItemHandler handler)
        {
            // создать новый пункт меню
            MenuItem item = new MenuItem(letter, title, handler);

            //добавить новый пункт меню в конец списка пунктов
            items.Add(item);
        }

        public void Print()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Print();
            }
        }

        public void Run() // цикл показа и обработки меню
        {
            while (true)
            {
                Console.Clear();
                Print();

                var input = Console.ReadKey();
                char letter = input.KeyChar;

                // пробежать по всем пунктам меню
                Console.WriteLine();

                // перебрать все пункты меню
                for (int i = 0; i < items.Count; i++)
                {
                    // если нажатая буква совпадает с буквой текущего пункта
                    if (items[i].letter == letter)
                    {
                        MenuItemHandler handler = items[i].menuItemHandler;

                        // запуск метода-обработчика для пункта меню
                        handler();

                        break;
                    }
                }
            }

        }


    }
}
