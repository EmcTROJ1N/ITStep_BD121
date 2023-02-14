using System;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            Archive archive = new Archive(new MyConfig());

            archive.DisplaySettings();
            
            Algorythm1 alg1 = new Algorythm1();
            Algorythm2 alg2 = new Algorythm2();
            NewAlg newAlg = new NewAlg();
            NewAlgAdapter alg3 = new NewAlgAdapter(newAlg);

            archive.Pack(alg1);
            archive.Pack(alg2);
            archive.Pack(alg3);
            archive.UnPack(alg3);

            SFXPack SFXPack = new SFXPack(alg1);
            TurboPacking turboPack = new TurboPacking(alg2);
            
            archive.Pack(SFXPack);
            archive.UnPack(turboPack);
        }
    }
}