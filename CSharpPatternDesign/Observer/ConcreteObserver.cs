class ConcreteObserver : Observer
{
    public string Name { get; private set; }
    string? ObserverState;
    ConcreteSubject Subject;

    public ConcreteObserver(ConcreteSubject subj, string name)
    {
        Subject = subj;
        Name = name;
    }

    public override void Update()
    {
        ObserverState = Subject.SubjectState;
        System.Console.WriteLine($"Observer {Name}`s new state is {ObserverState}");
    }
}