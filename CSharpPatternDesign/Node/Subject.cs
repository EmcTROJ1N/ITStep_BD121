using System.Net.Mail;
abstract class Subject
{
    public delegate void Delegate();

    Delegate? delegates;
    public void Attach(Delegate func) { delegates += func; }
    public void Detach(Delegate func) { delegates -= func; } 
    public void Notify()
    {
        delegates?.Invoke();
    }

}