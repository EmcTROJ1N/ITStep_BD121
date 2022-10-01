
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace ExamSharp
{
    internal class Program
    {
        public static string? location = System.Reflection.Assembly.GetExecutingAssembly().Location;
        // public static string? locationPath = Path.GetDirectoryName(location) + "\\intervals.txt";


        static void Main(string[] args)
        {
            IntervalCollection intervals =  new IntervalCollection();
           
            // intervals.Load(locationPath);
           

            intervals += new Interval(9, 10);
            intervals += new Interval(14, 15);
            intervals += new Interval(19, 20);
            intervals += new Interval(24, 25);
            intervals += new Interval(29, 35);


            intervals.Save("save.txt"); //Сохранение интервалов в файл intervals.txt
        }
    }
}