using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_MenuDelegates
{
    public delegate void MenuItemHandler();

    class MenuItem
    {
        //название пункта меню
        string title;

        //буква пункта меню
        public char letter;

        // переменная типа делегат MyFunc
        public MenuItemHandler menuItemHandler;

        public MenuItem(char letter, string title, MenuItemHandler del)
        {
            this.letter = letter;
            this.title = title;
            this.menuItemHandler = del;
        }

        public void Print()
        {
            Console.WriteLine($"{letter}. {title}");
        }
    }
}
