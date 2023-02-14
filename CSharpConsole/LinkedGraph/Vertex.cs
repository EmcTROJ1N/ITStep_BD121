using System;
using System.Collections.Generic;
using static Link;

class Vertex 
{
    public string Title { get; set; }
    public int Weight { get; set; }
    public long Id { get; set; }
    internal List<Link> Links;
    public int CurrentLinksCount { get { return Links.Count; } }

    public Vertex(int id, string title, int weight)
    {
        Links = new List<Link>();
        Id = id;
        Title = title;
        Weight = weight;
    }

    public void RemoveLink(int linkId)
    {
        foreach (var item in Links)
        {
            if (item.Id == linkId)
            {
                Links.Remove(item);
                break;
            }
        }
    }

    public void Print()
    {
        System.Console.WriteLine($"Vertex: {Title}");
        foreach (var item in Links)
            item.Print();
        System.Console.WriteLine();
    }
}