using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

class Graph<Vertex, Weight> : ICloneable
{
    public int MaxVertexCount { get; set; }
    public int CurrentVertexCount { get; set; }
    Weight[][] Links;



    public SortedDictionary<Vertex, int> Vertices { get; private set; }
    public SortedDictionary<int, Vertex> IndexVertices { get; private set; }

    public Graph(int maxVertexCount)
    {
        MaxVertexCount = maxVertexCount;
        CurrentVertexCount = 0;
        Vertices = new SortedDictionary<Vertex, int>();
        IndexVertices = new SortedDictionary<int, Vertex>();
        Links = new Weight[MaxVertexCount][];
        for (int i = 0; i < Links.Length; i++)
        {
            Links[i] = new Weight[MaxVertexCount];

            for (int j = 0; j < MaxVertexCount; j++)
                Links[i][j] = default(Weight);
        }
    }

    public Graph(Graph<Vertex, Weight> source)
    {
        MaxVertexCount = source.MaxVertexCount;
        CurrentVertexCount = source.CurrentVertexCount;
        Vertices = new SortedDictionary<Vertex, int>(source.Vertices);
        IndexVertices = new SortedDictionary<int, Vertex>(source.IndexVertices);
        Array.Copy(source.Links, Links!, source.Links.Length);
    }

    public bool AddVertex(Vertex vert)
    {
        if (Vertices.ContainsKey(vert))
        {
            System.Console.WriteLine("string already exist");
            return false;
        }

        if (CurrentVertexCount == MaxVertexCount)
        {
            System.Console.WriteLine("No space left");
            return false;
        }

        Vertices.Add(vert, CurrentVertexCount);
        IndexVertices.Add(CurrentVertexCount++, vert);
        return true;
    }

    public void PrintVertices()
    {
        foreach (var item in Vertices)
            System.Console.WriteLine($"{item.Key} -> {item.Value}");
    }

    public bool AddLink(Vertex vert1, Vertex vert2, Weight weight)
    {
        if (!Vertices.ContainsKey(vert1) || !Vertices.ContainsKey(vert2))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        int frstVertIdx = Vertices[vert1];
        int secdVertIdx = Vertices[vert2];

        Links[frstVertIdx][secdVertIdx] = weight;
        Links[secdVertIdx][frstVertIdx] = weight;

        return true;
    }

    public bool RemoveLink(Vertex vert1, Vertex vert2)
    {
        if (!Vertices.ContainsKey(vert1) || !Vertices.ContainsKey(vert2))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        int frstVertIdx = Vertices[vert1];
        int secdVertIdx = Vertices[vert2];

        Links[frstVertIdx][secdVertIdx] = default(Weight);
        Links[secdVertIdx][frstVertIdx] = default(Weight);

        return true;
    }
    public Weight GetLink(Vertex vert1, Vertex vert2)
    {
        if (!Vertices.ContainsKey(vert1) || !Vertices.ContainsKey(vert2))
        {
            System.Console.WriteLine("Wrong string name");
            return default(Weight);
        }
        else
        {
            int frstVertIdx = Vertices[vert1];
            int secdVertIdx = Vertices[vert2];

            return Links[frstVertIdx][secdVertIdx];
        }
    }

    public bool PrintLinks(Vertex vert)
    {
        if (!Vertices.ContainsKey(vert))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        System.Console.WriteLine($"{vert} links: ");
        int vertIdx = Vertices[vert];

        bool isFirststring = false;
        for (int i = 0; i < CurrentVertexCount; i++)
        {
            if (Links[vertIdx][i] != null)
            {
                if (!isFirststring)
                {
                    System.Console.WriteLine($"{IndexVertices[i]} ({Links[vertIdx][i]})");
                    isFirststring = true;
                }
                else
                    System.Console.WriteLine($"{IndexVertices[i]} ({Links[vertIdx][i]})");
            }
        }
        System.Console.WriteLine();

        return true;
    }

    public object Clone()
    {
        Graph<Vertex, Weight> source = new Graph<Vertex, Weight>(this.MaxVertexCount);

        source.CurrentVertexCount = this.CurrentVertexCount;
        source.Vertices = new SortedDictionary<Vertex, int>(this.Vertices);
        source.IndexVertices = new SortedDictionary<int, Vertex>(this.IndexVertices);
        Array.Copy(this.Links, source.Links, this.Links.Length);

        return source;
    }

    public bool RenameVertex(Vertex oldVert, Vertex newVert)
    {
        if (!Vertices.ContainsKey(oldVert))
        {
            System.Console.WriteLine("Wrong old string name");
            return false;
        }

        int vertIdx = Vertices[oldVert];
        Vertices.Remove(oldVert);
        IndexVertices.Remove(vertIdx);

        Vertices.Add(newVert, vertIdx);
        IndexVertices.Add(vertIdx, newVert);

        return true;
    }

    public bool DeleteVertex(Vertex vert)
    {

        if (!Vertices.ContainsKey(vert))
        {
            System.Console.WriteLine("Wrong vertex");
            return false;
        }

        int idx = Vertices[vert];

        for (int i = 0; i < CurrentVertexCount; i++)
            Links[i][idx] = default(Weight);
        for (int i = 0; i < CurrentVertexCount; i++)
            Links[idx][i] = default(Weight);

        Vertices.Remove(vert);
        IndexVertices.Remove(idx);

        IEnumerator<KeyValuePair<Vertex, int>> vertIt = Vertices.GetEnumerator();
        IEnumerator<KeyValuePair<int, Vertex>> idxIt = IndexVertices.GetEnumerator();

        SortedDictionary<Vertex, int> tmpVertices = new SortedDictionary<Vertex, int>(Vertices);
        SortedDictionary<int, Vertex> tmpIndexVertices = new SortedDictionary<int, Vertex>(IndexVertices);

        Vertices.Clear();
        IndexVertices.Clear();

        IEnumerator<KeyValuePair<Vertex, int>> tmpVertIt = tmpVertices.GetEnumerator();
        IEnumerator<KeyValuePair<int, Vertex>> tmpIdxIt = tmpIndexVertices.GetEnumerator();

        for (int i = 0; tmpVertIt.MoveNext() && tmpIdxIt.MoveNext(); i++)
        {
            Vertices.Add(tmpVertIt.Current.Key, tmpVertIt.Current.Value);
            IndexVertices.Add(tmpIdxIt.Current.Key, tmpIdxIt.Current.Value);
        }
        CurrentVertexCount--;

        return true;
    }

    public void Print()
    {
        System.Console.Write($"X \t");
        foreach (var key in Vertices.Keys)
            System.Console.Write($"{key}\t");
        System.Console.WriteLine();

        for (int i = 0; i < CurrentVertexCount; i++)
        {
            System.Console.Write($"{IndexVertices[i]}\t");
            for (int j = 0; j < CurrentVertexCount; j++)
                System.Console.Write($"{Links[i][j]}\t");
            System.Console.WriteLine();
        }
    }

    public int FindVertexIdx(Vertex source)
    {
        if (Vertices.ContainsKey(source) == false)
            throw new Exception("This string is not exist in graph");
        
        return Vertices[source];
    }

    public bool Contains(Vertex vert)
    {
        return Vertices.ContainsKey(vert);
    }

    public override string ToString()
    {
        string str = "";

        foreach (var vert in Vertices)
        {
            str += vert;
            str += '\n';
        }
        return str;
    }
}