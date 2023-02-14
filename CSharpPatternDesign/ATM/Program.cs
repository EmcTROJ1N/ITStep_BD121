using System;

class Program
{
    static void Main(string[] args)
    {
        ATM bank = new ATM(new WaitForMoney());
        bank.PushMoney();
    }
}