using System.Net.Http.Headers;
using System.Net;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Console_RexEx
{
    class Program
    {
        public enum Operation { Sum, Min, Pro, Dev };
        static int Calc(string str)
        {
            Operation comparator = Operation.Sum;
            if (str.Contains('+')) comparator = Operation.Sum;
            if (str.Contains('-')) comparator = Operation.Min;
            if (str.Contains('/')) comparator = Operation.Dev;
            if (str.Contains('*')) comparator = Operation.Pro;

            string[] nums = str.Split(new char[] {'+', '-', '*', '/'}, StringSplitOptions.RemoveEmptyEntries);

            Int32.TryParse(nums[0], out int A);
            Int32.TryParse(nums[1], out int B);
            int res = 0;

            switch (comparator)
            {
                case Operation.Sum: res = A + B; break;
                case Operation.Min: res = A - B; break;
                case Operation.Pro: res = A * B; break;
                case Operation.Dev: res = A / B; break;
            }

            return res;
        }
        static void Main(string[] args)
        {
            void task1()
            {
                // string? str = Console.ReadLine();
                string? str = "192.168.0.1";
                if (str == null)
                    return;
                Match phone = Regex.Match(str, @"^((8|\+7|38|\+38)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", RegexOptions.IgnoreCase);
                Match date = Regex.Match(str, @"(0?[1-9]|[12][0-9]|3[01])[\/\-\.](0?8)[ \/\.\-]([0-9]?[0-9]?[0-9]?[0-9])", RegexOptions.IgnoreCase);
                Match doubleNum = Regex.Match(str, @"[\+\-]?\d{1,}\.\d{1,}", RegexOptions.IgnoreCase);
                Match ip = Regex.Match(str, @"(([0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\.){3}([0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]?)$", RegexOptions.IgnoreCase);

                if (str == phone.Value)
                    System.Console.WriteLine("it`s phone");
                if (str == date.Value)
                    System.Console.WriteLine("it`s date");
                if (str == ip.Value)
                    System.Console.WriteLine("it`s ip");
                if (str == doubleNum.Value)
                    System.Console.WriteLine("it`s double");
            }

            void task2()
            {
                StreamReader reader = new StreamReader("text.txt", Encoding.Default);
                StreamWriter writer = new StreamWriter("res.txt", false, Encoding.Default);

                for (string? line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    MatchCollection collec = Regex.Matches(line, @"-?\d+(\.\d+)?\s?[\+|\-|\*\/]\s?-?\d+(\.\d+)?", RegexOptions.IgnoreCase);
                    
                    if (collec.Count == 0)
                        continue;

                    foreach (Match item in collec)
                    {
                        line = line.Replace(item.Value, Calc(item.Value).ToString());
                    }
                    // writer.WriteLine(line);
                    System.Console.WriteLine(line);
                }

                reader.Close();
            }

            task2();
        }
    }
}