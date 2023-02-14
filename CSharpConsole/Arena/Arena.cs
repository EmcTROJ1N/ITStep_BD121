using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class Arena
{
    public int[,] Arr;

    public int percent { get; private set; }

    Random _Random = new Random();

    Point C = new Point();

    public Arena(int n, int m, int t)
    {
        if (t < 0 || t > 100) percent = 30;
        percent = t;

        Arr = new int[n, m];

        for (int i = 0; i < Arr.GetLength(0); i++)
        {
            for (int j = 0; j < Arr.GetLength(1); j++)
            {
                Arr[i, j] = _Random.Next(0, 100);
            }
        }

        for (int j = 0; j < Arr.GetLength(1); j++)
        {
            Arr[0, j] = 0;
            Arr[Arr.GetLength(0) - 1, j] = 0;
        }

        for (int i = 0; i < Arr.GetLength(0); i++)
        {
            Arr[i, 0] = 0;
            Arr[i, Arr.GetLength(1) - 1] = 0;
        }

        C.Y = _Random.Next(1, n - 1);
        C.X = _Random.Next(1, m - 1);
    }

    public void Print(ConsoleColor color)
    {
        for (int i = 0; i < Arr.GetLength(0); i++)
        {
            for (int j = 0; j < Arr.GetLength(1); j++)
            {
                if (Arr[i, j] > percent)
                {
                    Console.ResetColor();
                    Console.Write(" ");
                }
                else
                {
                    Console.BackgroundColor = color;
                    Console.Write(" ");
                }
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    public void Run(ConsoleColor color_obstacle, ConsoleColor color_user)
    {
        Print(color_obstacle);

        Console.SetCursorPosition(C.X, C.Y);

        Console.ForegroundColor = color_user;

        Console.WriteLine("!");


        //    bool flag = true;

        while (true)
        {
            var key = Console.ReadKey();

            Console.SetCursorPosition(C.X, C.Y);
            Console.WriteLine(" ");

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                        if (Arr[C.Y, C.X - 1] > percent)
                            --C.X;
                        break;
                case ConsoleKey.RightArrow:
                        if (Arr[C.Y, C.X + 1] > percent)
                            ++C.X;
                        break;
                case ConsoleKey.UpArrow:
                        if (Arr[C.Y - 1, C.X] > percent)
                            --C.Y;
                        break;
                case ConsoleKey.DownArrow:
                        if (Arr[C.Y + 1, C.X] > percent)
                            ++C.Y;
                        break;
                case ConsoleKey.Enter:
                        Console.Clear();
                        return;
                default:
                    break;
            }

            Console.SetCursorPosition(C.X, C.Y);
            Console.WriteLine("!");

        }
    }
}
