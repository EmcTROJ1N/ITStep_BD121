abstract class Subject
{
    List<Observer> Observers = new List<Observer>();
    public void Attach(Observer obs) { Observers.Add(obs); }
    public void Detach(Observer obs) { Observers.Remove(obs); }
    public void Notify()
    {
        foreach (Observer obs in Observers)
            obs.Update();
    }

}