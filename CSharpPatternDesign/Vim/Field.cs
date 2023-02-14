using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Field
{
    List<string?> Lst;
    int NumStr = 1;
    Compiler _Compiler;

    public Field(ConsoleColor consoleColor, ConsoleColor foregroundColor)
    {
        Console.BackgroundColor = consoleColor;
        Console.ForegroundColor = foregroundColor;
        Console.Clear();

        _Compiler = new ConcreteCompiler();
        Lst = new List<string?>();
        InitField();
    }

    public void InitField()
    {
        while (true)
        {
            System.Console.Write($"{NumStr} ");
            string? str = Console.ReadLine();

            if (str == null) continue;
            if (str == ":q!") break;

            if (Regex.IsMatch(str, @":wq \w{1,}.\w{1,}", RegexOptions.IgnoreCase))
            {
                StreamWriter writer = new StreamWriter(str.Substring(4), false, Encoding.Default);

                foreach (var line in Lst)
                    writer.WriteLine(line);

                writer.Close();
                break;
            }
            
            if (str == ":compile by proxy")
            {
                _Compiler = new ProxyCompiler();
                _Compiler.Compile(Lst);
            }
            if (str == ":compile") _Compiler.Compile(Lst);


            NumStr++;
            Lst.Add(str);
        }
    }
}