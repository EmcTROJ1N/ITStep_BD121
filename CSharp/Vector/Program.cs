using System;
using static Vector;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CSharp;

class Program
{

    public static void Main(string[] args)
    {
        Vector vec = new Vector(100, new QuickSort(), new Increasing());
        vec.PutRands();
        vec.Sort();
        System.Console.WriteLine(vec);
    }


}