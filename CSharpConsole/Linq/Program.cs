using System;

namespace CSharp
{
    class Program
    {
        static int NumberOfOccurrences(int[] arr, int elem)
        {
            return arr.Count(el => el == elem);
        }
        static int GetUnique(int[] arr)
        {
            return arr.Where(el => arr.Count(x => x == el) == 1).First();
        }
        static string RemoveDuplicattesWords(string str)
        {
            string[] arr = str.Split(' ');
            return String.Join(' ', arr.Select(n => n).Distinct());
        }

        // static string Solve(string str)
        // {
        //     return String.Join(' ', str.Split(' ').Select(n => n.Length > str.Split().OrderByDescending(s => s.Length).First().Count()));
        // }

        static string ReverseWords(string str)
        {
            return  str.Split(' ').Aggregate("",(workingSentence, next) => workingSentence + new string(next.Reverse().ToArray())+ " "); 
            // return String.Join(' ', str.Split(' ').Select(n => n.Reverse()));
        }

        static string MakeString(string str)
        {
            return String.Join(' ', str.Split(' ').Select(n => n.First()));
        }
        
        // static string Capitals(string str)
        // {
        //     char[] arr = str.ToCharArray();
        //     return String.Join(' ', arr.Select(n => Array.IndexOf(arr, n.First());
        // }
        static void Main(string[] args)
        {
            System.Console.WriteLine(NumberOfOccurrences(new int[] { 1, 2, 3, 4, 5, 6, 1} , 1));
            System.Console.WriteLine(GetUnique(new int[] { 0, 2, 100, 2, 0 }));
            System.Console.WriteLine(RemoveDuplicattesWords("Hello big world big Hello"));
            // System.Console.WriteLine(Solve("12hello987big89world"));
            System.Console.WriteLine(ReverseWords("Hello world"));
            System.Console.WriteLine(MakeString("Miry Mir"));
            // System.Console.WriteLine(Capitals("Hello world"));
        }
    }
}