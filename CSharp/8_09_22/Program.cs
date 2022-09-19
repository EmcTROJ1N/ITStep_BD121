using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Runtime;
using System;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Program
    {
        public enum Operation
        {
            Add,
            Remove,
            SeeAll,
            TranslateW,
            TranslateSent,
            Save,
            Load,
            Exit
        };

        // [Serializable]
        // class Dict
        // {
        //     public Dictionary<string, string> dict;

        //     public Dict()
        //     {
        //         dict = new Dictionary<string, string>();
        //     }
        // }

        static void Main(string[] args)
        {
            Operation oper;
            // Dict dict = new Dict();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            while (true)
            {

                System.Console.WriteLine("Menu:");
                System.Console.WriteLine("0 - Add Pair:");
                System.Console.WriteLine("1 - Remove Pair:");
                System.Console.WriteLine("2 - See all Pairs:");
                System.Console.WriteLine("3 - Translate word");
                System.Console.WriteLine("4 - Translate sentence");
                System.Console.WriteLine("5 - save ");
                System.Console.WriteLine("6 - load ");
                System.Console.WriteLine("7 - exit");

                Int32.TryParse(Console.ReadLine(), out var _oper);
                oper = (Operation)_oper;

                switch (oper)
                {
                    case Operation.Add:
                        dict.Add(Console.ReadLine(), Console.ReadLine());
                        break;
                    case Operation.Remove:
                        dict.Remove(Console.ReadLine());
                        break;
                    case Operation.SeeAll:
                        foreach (var item in dict)
                            System.Console.WriteLine(item);
                        break;
                    case Operation.TranslateW:
                        string str = Console.ReadLine();
                        if (dict.TryGetValue(str, out string res) == true)
                        {
                            System.Console.WriteLine(res);
                        }
                        else
                        {
                            foreach (var item in dict)
                            {
                                if (item.Key == str)
                                {
                                    System.Console.WriteLine(item.Value);
                                    break;
                                }
                            }
                        }

                        break;
                    case Operation.TranslateSent:
                        List<string> lst = Console.ReadLine().Split(new string[] { ",", ".", " ", "!", "?", "—", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (var item in lst)
                            System.Console.Write($"{dict[item]} ");
                        System.Console.WriteLine();
                        break;
                    case Operation.Save:
                        FileStream fs = new FileStream("dict.xml", FileMode.Create, FileAccess.Write, FileShare.None);
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, dict);
                        fs.Close();
                        break;

                    case Operation.Load:
                        BinaryFormatter bf2 = new BinaryFormatter();
                        FileStream fs2 = File.OpenRead("dict.xml");
                        dict = (Dictionary<string, string>)bf2.Deserialize(fs2);
                        fs2.Close();

                        break;

                    default:
                        break;
                }

                if (Operation.Exit == oper)
                    break;
            }
        }
    }
}