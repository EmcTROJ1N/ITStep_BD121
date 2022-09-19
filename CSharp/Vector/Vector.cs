using System.Threading.Tasks.Dataflow;
using System;
using System.IO;
using System.Text;
using System.Collections;

class Vector
{
    SortStrategy Strategy;
    Comparator Comp;

    int[] Arr;
    public int Size { get; set; }

    public Vector(int size, SortStrategy? strategy = null, Comparator? comp = null)
    {
        Size = size;
        Arr = new int[Size];
        if (strategy == null)
            Strategy = new BubbleSort();
        else
            Strategy = strategy;
        if (comp == null)
            Comp = new Increasing();
        else
            Comp = comp;
    }

    public int this[int i]
    {
        get { return Arr[i]; }
        set { Arr[i] = value; }
    }

    public void PutRands()
    {
        Random rand = new Random();
        for (int i = 0; i < Arr.Length; i++)
            Arr[i] = rand.Next(0, 100);
    }

    public void Print()
    {
        foreach (var item in Arr)
            System.Console.Write($"{item} ");
    }

    public override string ToString()
    {
        string str = "";
        foreach (var item in Arr)
            str += (item + " ");
        return str;
    }

    public int Sum()
    {
        int sum = 0;
        foreach (var item in Arr)
            sum += item;
        return sum;
    }

    public static Vector operator ++(Vector vec)
    {
        for (int i = 0; i < vec.Size; i++)
            vec[i]++;
        return vec;
    }
    public static Vector operator --(Vector vec)
    {
        for (int i = 0; i < vec.Size; i++)
            vec[i]--;
        return vec;
    }

    public static Vector operator +(Vector source, int n)
    {
        Vector vec = new Vector(source.Size);
        for (int i = 0; i < vec.Size; i++)
            vec[i] += n;
        return vec;
    }

    public static Vector operator -(Vector source, int n)
    {
        Vector vec = new Vector(source.Size);
        for (int i = 0; i < vec.Size; i++)
            vec[i] -= n;
        return vec;
    }

    public static Vector operator +(Vector vec1, Vector vec2)
    {
        int s = 0;
        if (vec1.Size < vec2.Size)
            s = vec1.Size;
        else
            s = vec2.Size;
        Vector res = new Vector(s);

        for (int i = 0; i < vec2.Size; i++)
            res[i] = vec1[i];
        for (int i = 0; i < vec1.Size; i++)
            res[i] += vec2[i];
        return res;
    }

    public static Vector operator -(Vector vec1, Vector vec2)
    {
        int s = 0;
        if (vec1.Size < vec2.Size)
            s = vec1.Size;
        else
            s = vec2.Size;
        Vector res = new Vector(s);

        for (int i = 0; i < vec2.Size; i++)
            res[i] = vec1[i];
        for (int i = 0; i < vec1.Size; i++)
            res[i] -= vec2[i];
        return res;
    }

    public static explicit operator int(Vector vec) { return vec.Size; }

    public static bool operator ==(Vector vec, Vector vec2)
    {
        if (vec.Size != vec2.Size)
            return false;

        for (int i = 0; i < vec.Size; i++)
        {
            if (vec[i] != vec2[i])
                return false;
        }

        return true;
    }

    public static bool operator !=(Vector vec, Vector vec2)
    {
        if (vec.Size != vec2.Size)
            return true;

        for (int i = 0; i < vec.Size; i++)
        {
            if (vec[i] != vec2[i])
                return true;
        }

        return false;
    }

    public bool Save()
    {
        StreamWriter writer = new StreamWriter("data.log", false, Encoding.Default);

        writer.WriteLine(Size);
        foreach (var item in Arr)
            writer.WriteLine(item);
        writer.Close();
        return true;
    }

    public bool Load()
    {
        StreamReader reader = new StreamReader("data.log", Encoding.Default);

        string? line = reader.ReadLine();
        Int32.TryParse(line, out int size);
        Size = size;

        Arr = new int[Size];
        int i = 0;
        for (line = reader.ReadLine(); line != null; line = reader.ReadLine())
        {
            Int32.TryParse(line, out Arr[i]);
            i++;
        }

        reader.Close();
        return true;
    }

    public bool Sort()
    {
        Strategy.Sort(Arr, Comp);
        return true;
    }
}