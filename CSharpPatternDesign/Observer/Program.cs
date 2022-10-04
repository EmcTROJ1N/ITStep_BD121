using System;

class Program
{
    static void Main(string[] args)
    {
        ConcreteSubject subj = new ConcreteSubject();

        subj.Attach(new ConcreteObserver(subj, "X"));
        subj.Attach(new ConcreteObserver(subj, "Y"));
        subj.Attach(new ConcreteObserver(subj, "Z"));

        subj.SubjectState = "XYZ";
        subj.Notify();

    }
}