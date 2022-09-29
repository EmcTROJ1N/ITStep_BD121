interface IAttack
{
    public void Attack(int x, int y);
    public void Move();
    public void Retreat();
    public void Defend(int x, int y);
}

interface IFly
{
    public void _Fly();
    public void Sit();
}

interface IWalk
{
    public void Stand();
    public void Walk();
    public void Stop();
    public void Run();
    public void GetCords();
}

interface IDrivable
{
    public void Drive();
}

interface IPrintable
{
    public void Print();
}