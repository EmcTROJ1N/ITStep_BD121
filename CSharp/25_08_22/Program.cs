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
            string str = "clara has 45 cats, and 46 dogs";
            string[] arr = str.Split(new string[] { ",", ".", " ", "!", "?" }, StringSplitOptions.RemoveEmptyEntries);

            void task1()
            {
                long sum = 0;
                foreach (string word in arr)
                {
                    long num;
                    if (Int64.TryParse(word, out num))
                        sum += num;
                }

                System.Console.WriteLine(sum);
            }

            void task2()
            {
                int idx = 0;
                int minLen = arr[0].Length;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (!Int64.TryParse(arr[i], out var N))
                    {
                        if (minLen > arr[i].Length)
                        {
                            minLen = arr[i].Length;
                            idx = i;
                        }
                    }
                }
                arr[idx] = "!!!";

                str = string.Join(" ", arr);
                System.Console.WriteLine(str);
            }

            void task3()
            {
                str = "";
                foreach (var word in arr)
                {
                    if (Int64.TryParse(word, out var N))
                        continue;
                    bool flag = false;
                    foreach (var ch in "AaEeOoIiUuYy")
                    {
                        if (word.Contains(ch))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (flag)
                    {
                        str += word;
                        str += " ";
                    }
                }

                System.Console.WriteLine(str);
            }

            void task4()
            {
                int num;
                string? str = Console.ReadLine();
                // string str = "51564";
                string resNum = "";
                Int32.TryParse(str, out num);

                string[] oneNine =
                {
                    "один", "два", "три",
                    "четыре", "пять", "шесть",
                    "семь", "восемь", "девять"
                };

                
                string[] oneNineS =
                {
                    "одна", "две", "три",
                    "четыре", "пять", "шесть",
                    "семь", "восемь", "девять"
                };

                string[] elevenTwenty =
                {
                    "одиннадцать", "двенадцать", "тринадцать",
                    "четырнадцать", "пятнадцать", "шестнадцать",
                    "семнадцать", "восемнадцать", "девятнадцать",
                    "двадцать"
                };

                string[] dozens =
                {
                    "десять", "двадцать", "тридцать",
                    "сорок", "пятьдесят", "шестьдесят",
                    "семьдесят", "восемьдесят", "девяносто"
                };

                string[] hundreds =
                {
                    "сто", "двести", "триста",
                    "четыреста", "пятьсот", "шестьсот",
                    "семьсот", "восемьсот", "девятьсот"
                };

                string[][] txtNums =
                { oneNine, dozens, hundreds };

                Array.Reverse(txtNums);

                switch (str.Length)
                {
                    case 1:
                        resNum = oneNine[num - 1];
                        break;
                    case 2:
                        resNum = elevenTwenty[num - 11];
                        break;
                    case 3:
                        for (int i = 0, N = 100; i < str.Length; i++, N /= 10)
                        {
                            resNum += txtNums[i][(num / N) - 1];
                            resNum += " ";
                            num %= N;
                        }
                        break;
                    case 4:
                        resNum += oneNineS[(num / 1000) - 1];
                        resNum += " тысяч ";
                        num %= 1000;

                        for (int i = 0, N = 100; i < str.Length - 1; i++, N /= 10)
                        {
                            resNum += txtNums[i][(num / N) - 1];
                            resNum += " ";
                            num %= N;
                        }
                        break;
                    case 5:

                        if ((num / 1000) > 10 && (num / 1000) < 19)
                        {
                            resNum += elevenTwenty[num / 1000 - 11];   
                            resNum += " тысяч ";
                        }
                        else
                        {
                            resNum += dozens[(num / 10000) - 1];
                            resNum += " ";
                            if ((num / 1000) % 10 != 0)
                            {
                                resNum += oneNineS[((num / 1000) % 10) - 1];
                                resNum += " тысячи ";
                            }
                            else
                                resNum += " тысяч ";
                        }
                        num %= 1000;
                        
                        for (int i = 0, N = 100; i < str.Length - 2; i++, N /= 10)
                        {
                            resNum += txtNums[i][(num / N) - 1];
                            resNum += " ";
                            num %= N;
                        }


                        break;
                    case 6:
                        
                        int num2 = num % 1000;
                        num /= 1000;

                        for (int i = 0, N = 100; i < str.Length - 3; i++, N /= 10)
                        {
                            resNum += txtNums[i][(num / N) - 1];
                            resNum += " ";
                            num %= N;
                        }
                        resNum += " тысяч ";
                        num = num2;
                        for (int i = 0, N = 100; i < str.Length - 3; i++, N /= 10)
                        {
                            resNum += txtNums[i][(num / N) - 1];
                            resNum += " ";
                            num %= N;
                        }

                        break;
                    default:
                        break;
                }
                System.Console.WriteLine(resNum);
            }

            task1();
            task2();
            task3();
            task4();
        }
    }
}