using System;

class Ticket
{
    public List<int> num { get; private set; }
    public Ticket() { num = new List<int>(); }
    public Ticket(int tic)
    {
        num = new List<int>();
        int k = 0; 
        while (tic != 0)
        {
            num.Add(tic % 10);
            tic /= 10;
            k++;
        }
        num.Reverse();
        if (k < 6)
        {
            for (int i = k; i < 6; i++)
                num.Add(0);
        }
        if (k > 6)
        {
            num.RemoveRange(6, num.Count - 6);
        }
    }

    public override string ToString()
    {
        string str = "";
        foreach (var item in num)
            str += item.ToString();
        return str;
    }
}