// алгоритмы сканирования файлов
abstract class Algorythm
{
    public abstract Antivirus.States Scan(FileInfo file);
}

class AvastAlg : Algorythm
{
    public override Antivirus.States Scan(FileInfo file)
    {
        // тут тип логика должна быть
        return Antivirus.States.Alright;
    }
}

class ESETAlg : Algorythm
{
    public override Antivirus.States Scan(FileInfo file)
    {
        // тут тип логика должна быть
        return Antivirus.States.Alright;
    }
}

class KasperskiyAlg : Algorythm
{
    public override Antivirus.States Scan(FileInfo file)
    {
        // тут тип логика должна быть
        return Antivirus.States.Alright;
    }
}

sealed class WindowsDefender
{
    public Antivirus.States Scan(FileInfo file)
    {
        // тут тип дложна быть воообще другая логика
        return Antivirus.States.Alright;
    }
}

// класс адаптер для алгоритма (никогда этот антивирус доверия не внушал)
class WindowsDefenderAdapter : Algorythm
{
    WindowsDefender Alg;

    public WindowsDefenderAdapter() =>
        Alg = new WindowsDefender();

    public override Antivirus.States Scan(FileInfo file) =>
        Alg.Scan(file);
}