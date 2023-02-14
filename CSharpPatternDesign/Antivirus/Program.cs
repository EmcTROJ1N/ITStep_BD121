using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Antivirus _Antivirus = Antivirus.Instance();
            _Antivirus.ScanPC();
            _Antivirus.SendFileToQuarantine("ttt.dn.ua");
            _Antivirus.OffProtect();
            _Antivirus.OnProtect();
        }
    }
}