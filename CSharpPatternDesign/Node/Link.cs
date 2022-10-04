class Link : Observer
{
    Node From;
    Node To;

    Node FromState = new Node(0, 0);
    Node ToState = new Node(0, 0);
    
    public Link(Node from, Node to)
    {
        From = from;
        To = to;
        From.Attach(Update);
        To.Attach(Update);
    }

    public override void Update()
    {
        FromState.Move(From.X, From.Y);
        ToState.Move(To.X, To.Y);
        System.Console.WriteLine($"Updated link: from {FromState.X} {FromState.Y} to {ToState.X} {ToState.Y}");
    }
}