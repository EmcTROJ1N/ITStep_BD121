using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Antivirus antivirus = Antivirus.Instance(new ESETAlg());
        antivirus.AlgorythmStrategy = new WindowsDefenderAdapter();

        antivirus.SpyFolder("/home/omon");

        Console.ReadLine();
    }
}