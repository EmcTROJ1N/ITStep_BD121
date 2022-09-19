using System;
using System.Collections.Generic;
using static Vertex;
using static Link;

class Graph
{
    public string Title { get; set; }
    int VertId = 0;
    public int CurrentVertexCount { get; set; } = 0;
    public int linkId { get; set; } = 0;
    public int CurrentLinksCount { get; set; } = 0;
    SortedDictionary<string, Vertex> Vertices;

    public Graph(string title)
    {
        Title = title;
        Vertices = new SortedDictionary<string, Vertex>();
    }

    public Vertex? FindVertex(string title)
    {
        if (Vertices.TryGetValue(title, out Vertex? vert) == true)
            return vert;
        else
            return null;
    }

    public int AddVertex(string title, int weight)
    {
        if (FindVertex(title) != null)
            return -1;
        Vertices.Add(title, new Vertex(VertId++, title, weight));
        CurrentVertexCount++;

        return VertId - 1;
    }

    public int AddLink(string from, string to, string title, int weight)
    {
        Vertex? vertFrom = FindVertex(from);
        Vertex? vertTo = FindVertex(to);

        if (vertFrom != null && vertTo != null)
        {
            Link lnk = new Link(linkId++, title, weight, vertFrom, vertTo);
            vertFrom.Links.Add(lnk);
            vertTo.Links.Add(lnk);
            CurrentLinksCount++;
            return linkId - 1;
        }
        return -1;
    }

    public bool RemoveVertex(int vertId)
    {
        foreach (var item in Vertices)
        {
            if (item.Value.Id == vertId)
            {
                CurrentLinksCount -= item.Value.CurrentLinksCount;
                CurrentVertexCount--;
                Vertices.Remove(item.Key);

                return true;
            }
        }
        return false;
    }
    public bool RemoveVertex(string title)
    {
        foreach (var item in Vertices)
        {
            if (item.Value.Title == title)
            {
                CurrentLinksCount -= item.Value.CurrentLinksCount;
                CurrentVertexCount--;
                Vertices.Remove(item.Key);

                return true;
            }
        }
        return false;
    }

    public void Print()
    {
        System.Console.WriteLine($"Graph: {Title}");
        System.Console.WriteLine($"Vertices: {CurrentVertexCount}, Links: {CurrentLinksCount}");

        foreach (var item in Vertices)
            item.Value.Print();
    }

}