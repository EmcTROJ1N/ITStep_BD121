class Node : Subject
{
    int x, y;
    public int X
    {
        get => x;
        set
        {
            x = value;
            Notify();
        }
    }
    public int Y 
    {
        get => y;
        set
        {
            y = value;
            Notify();
        }
    }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(int x, int y)
    {
        this.x = x;
        this.y = y;
        Notify();
    }
}