using System;
using static Vertex;

class Link
{
    Vertex From;
    Vertex To;
    public string Title { get; }
    public int Id { get; }
    int Weight;

    public Link(int id, string title, int weight, Vertex from, Vertex to)
    {
        this.From = from;
        this.To = to;
        this.Title = title;
        this.Id = id;
        this.Weight = weight;
    }

    public void Print()  { System.Console.WriteLine($"Link: {Title}, from {From.Title} to {To.Title}"); }
}