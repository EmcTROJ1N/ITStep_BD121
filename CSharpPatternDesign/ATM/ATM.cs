abstract class State
{
    public abstract void Handle(ATM source);
}

class WaitForMoney : State
{
    public override void Handle(ATM source)
    {
        do
        {
            System.Console.WriteLine("Enter money: ");
            Int32.TryParse(Console.ReadLine(), out int money);

            if (money <= 0)
            {
                source.CurrentState = new Error();
                return;
            }
            source.CurrentMoney += money;
        } while (source.CurrentMoney < 100);

        source.CurrentState = new MoneyPushing();
    }
}

class MoneyPushing : State
{
    public override void Handle(ATM source)
    {
        System.Console.WriteLine("You money pushing...");
        source.CurrentMoney = 0;
        source.CurrentState = new WaitForMoney();
    }
}

class Error : State
{
    public override void Handle(ATM source)
    {
        System.Console.WriteLine("Error, money no");
        source.CurrentMoney = 0;
        source.CurrentState = new WaitForMoney();
    }
}

class ATM
{
    State currentState;
    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            System.Console.WriteLine($"State: {currentState.GetType().Name}");
        }
    }
    public int CurrentMoney { get; set; }
    public ATM(State state) => currentState = state;

    public void PushMoney()
    {
        while (true)
        {
            Request();
            Thread.Sleep(1000);
        }
    }

    public void Request() => CurrentState.Handle(this);
}