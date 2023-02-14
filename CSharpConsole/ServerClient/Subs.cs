interface IServerable
{
    public bool Subscribe();
    public bool UnSubscribe();
    public void Messages(string msg);
    public void Event(string _event);
}


// первый класс-подписчик
class Client1 : IServerable                                  
{
    public bool Subscribe()
    {
        System.Console.WriteLine("Client1 subsribed");
        return true;
    }
    public bool UnSubscribe()
    {
        System.Console.WriteLine("Client1 unsubsribed");
        return true;
    }
    public void Messages(string msg) { System.Console.WriteLine($"One new private message to Client1: {msg}"); }
    public void Event(string _event) { System.Console.WriteLine($"Client1! Stared event {_event}"); }
}

class Client2 : IServerable                  
{
    public bool Subscribe()
    {
        System.Console.WriteLine("Client2 subsribed");
        return true;
    }
    public bool UnSubscribe()
    {
        System.Console.WriteLine("Client2 unsubsribed");
        return true;
    }
    public void Messages(string msg) { System.Console.WriteLine($"One new private message to Client2: {msg}"); }
    public void Event(string _event) { System.Console.WriteLine($"Client2! Stared event {_event}"); }
}