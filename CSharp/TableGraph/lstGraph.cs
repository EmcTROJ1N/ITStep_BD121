using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

class Graph : ICloneable
{
    public int MaxVertexCount { get; set; }
    public int CurrentVertexCount { get; set; }
    string[][] Links;



    SortedDictionary<string, int> Vertices;
    SortedDictionary<int, string> IndexVertices;

    public SortedDictionary<string, int> GetVertices() { return Vertices; }
    public SortedDictionary<int, string> GetIndexVertices() { return IndexVertices; }


    public Graph(int maxstringCount)
    {
        MaxVertexCount = maxstringCount;
        CurrentVertexCount = 0;
        Vertices = new SortedDictionary<string, int>();
        IndexVertices = new SortedDictionary<int, string>();
        Links = new string[MaxVertexCount][];
        for (int i = 0; i < Links.Length; i++)
        {
            Links[i] = new string[MaxVertexCount];

            // for (int j = 0; j < MaxstringCount; j++)
            //     Links[i][j] = "0";
        }
    }

    public Graph(Graph source)
    {
        MaxVertexCount = source.MaxVertexCount;
        CurrentVertexCount = source.CurrentVertexCount;
        Vertices = new SortedDictionary<string, int>(source.Vertices);
        IndexVertices = new SortedDictionary<int, string>(source.IndexVertices);
        Array.Copy(source.Links, Links!, source.Links.Length);
    }

    public bool Addstring(string str)
    {
        if (Vertices.ContainsKey(str))
        {
            // System.Console.WriteLine("string already exist");
            return false;
        }

        if (CurrentVertexCount == MaxVertexCount)
        {
            System.Console.WriteLine("No space left");
            return false;
        }

        Vertices.Add(str, CurrentVertexCount);
        IndexVertices.Add(CurrentVertexCount++, str);
        return true;
    }

    public void PrintVertices()
    {
        foreach (var item in Vertices)
            System.Console.WriteLine($"{item.Key} -> {item.Value}");
    }

    public bool AddLink(string string1, string string2, string weight)
    {
        if (!Vertices.ContainsKey(string1) || !Vertices.ContainsKey(string2))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        int frstVertIdx = Vertices[string1];
        int secdVertIdx = Vertices[string2];

        Links[frstVertIdx][secdVertIdx] = weight;
        Links[secdVertIdx][frstVertIdx] = weight;

        return true;
    }

    public bool RemoveLink(string string1, string string2)
    {
        if (!Vertices.ContainsKey(string1) || !Vertices.ContainsKey(string2))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        int frstVertIdx = Vertices[string1];
        int secdVertIdx = Vertices[string2];

        Links[frstVertIdx][secdVertIdx] = "0";
        Links[secdVertIdx][frstVertIdx] = "0";

        return true;
    }
    public string GetLink(string string1, string string2)
    {
        if (!Vertices.ContainsKey(string1) || !Vertices.ContainsKey(string2))
        {
            System.Console.WriteLine("Wrong string name");
            return "None";
        }
        else
        {
            int frstVertIdx = Vertices[string1];
            int secdVertIdx = Vertices[string2];

            return Links[frstVertIdx][secdVertIdx];
        }
    }

    public bool PrintLinks(string str)
    {
        if (!Vertices.ContainsKey(str))
        {
            System.Console.WriteLine("Wrong string name");
            return false;
        }

        System.Console.WriteLine($"{str} links: ");
        int vertIdx = Vertices[str];

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
        Graph source = new Graph(this.MaxVertexCount);

        source.CurrentVertexCount = this.CurrentVertexCount;
        source.Vertices = new SortedDictionary<string, int>(this.Vertices);
        source.IndexVertices = new SortedDictionary<int, string>(this.IndexVertices);
        Array.Copy(this.Links, source.Links, this.Links.Length);

        return source;
    }

    public bool Renamestring(string oldVert, string newVert)
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

    public bool Deletestring(string str)
    {

        if (!Vertices.ContainsKey(str))
        {
            System.Console.WriteLine("Wrong string");
            return false;
        }

        int idx = Vertices[str];

        for (int i = 0; i < CurrentVertexCount; i++)
            Links[i][idx] = null;
        for (int i = 0; i < CurrentVertexCount; i++)
            Links[idx][i] = null;

        Vertices.Remove(str);
        IndexVertices.Remove(idx);

        IEnumerator<KeyValuePair<string, int>> vertIt = Vertices.GetEnumerator();
        IEnumerator<KeyValuePair<int, string>> idxIt = IndexVertices.GetEnumerator();

        SortedDictionary<string, int> tmpVertices = new SortedDictionary<string, int>(Vertices);
        SortedDictionary<int, string> tmpIndexVertices = new SortedDictionary<int, string>(IndexVertices);

        Vertices.Clear();
        IndexVertices.Clear();

        IEnumerator<KeyValuePair<string, int>> tmpVertIt = tmpVertices.GetEnumerator();
        IEnumerator<KeyValuePair<int, string>> tmpIdxIt = tmpIndexVertices.GetEnumerator();

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

    public int FindstringIdx(string source)
    {
        if (Vertices.ContainsKey(source) == false)
            throw new Exception("This string is not exist in graph");
        
        return Vertices[source];
    }

    public bool Contains(string vert)
    {
        return Vertices.ContainsKey(vert);
    }

    public string Findstring(string str)
    {
        foreach (string vert in Vertices.Keys)
        {
            if (vert.Equals(str))
                return vert;
        }

        throw new Exception("Invalid string name");
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