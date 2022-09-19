using System;
using static Ticket;

abstract class Pattern
{
    public abstract bool IsLucky(Ticket tic);
}

class PattSum : Pattern
{
    public override bool IsLucky(Ticket tic) 
    {
        List<int> num = tic.num;
        if (num[0] + num[1] + num[2] ==
            num[3] + num[4] + num[5]) return true;
        return false;
    }
}

class PattUpper : Pattern
{
    public override bool IsLucky(Ticket tic)
    {
        List<int> num = tic.num;

        for (int i = 1; i < num.Count; i++)
        {
            if (num[i - 1] > num[i])
                return false;
        }

        return true;
    }
};

class PattDowner : Pattern
{
    public override bool IsLucky(Ticket tic) 
    {
        List<int> num = tic.num;

        for (int i = 1; i < num.Count; i++)
        {
            if (num[i - 1] < num[i])
                return false;
        }

        return true;
    }
};

class AllNumbs : Pattern
{
    public override bool IsLucky(Ticket tic) 
    {
        List<int> num = tic.num;
        for (int i = 1; i < num.Count; i++)
        {
            if (num[i] != num[i - 1])
                return false;
        }
        return true;
    }
};

class Repeat : Pattern
{
    public override bool IsLucky(Ticket tic)
    {
        List<int> num = tic.num;
        for (int i = 0, j = 1; i < num.Count - 2; i += 2, j += 2)
        {
            if ((num[j + 2] != num[j]) || (num[i + 2] != num[i]))
                return false;
        }
        return true;
    }
};