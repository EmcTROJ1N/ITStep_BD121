using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

[Serializable]
class Collection : IEnumerable
{
    List<Interval> Intervals;

    public bool HasHoles { get; private set; }
    public double Start { get; private set; }
    public double End { get; private set; }
    public int Count { get; private set; }

    public Collection()
    {
        Start = 0.0;
        End = 0.0;
        Intervals = new List<Interval>();
        HasHoles = false;
        Count = 0;
    }
    public Collection(Interval interval)
    {
        Intervals = new List<Interval>(new Interval[] { interval });
        Start = Intervals[0].Start;
        End = Intervals[0].End;
        Count = 1;
        HasHoles = false;
    }
    public Collection(Collection source) 
    {
        Intervals = new List<Interval>(source.Intervals); 
        Start = source.Start;
        End = source.End;
        Count = source.Count;
        HasHoles = source.HasHoles;
    }

    public void Print()
    {
        foreach (var item in Intervals)
            System.Console.WriteLine($"{item.Start} {item.End}");
    }

    public bool IsHole()
    {
        List<Interval> tmpLst = new List<Interval>(Intervals);
        tmpLst.Sort();
        double currStart = 0;
        double currEnd = 0;

        for (int i = 0; i < tmpLst.Count - 1; i++)
        {
            if (tmpLst[i].End < tmpLst[i + 1].Start)
            {
                bool flag = false;
                for (int j = 0; j < tmpLst.Count - 1; j++)
                {
                    if (tmpLst[j].Start > tmpLst[j].Start && tmpLst[j].End < tmpLst[i].End)
                        flag = true;
                }
                if (flag == false) return true;
            }
        }
        return false;
    }

    public void Add(Interval inter)
    {
        Intervals.Add(inter);

        foreach (var item in Intervals)
        {
            if (item.Start < Start) Start = item.Start;
            if (item.End > End) End = item.End;
        }

        Count++;
        HasHoles = IsHole();
    }


    public void RemoveByLength(double len)
    {
        List<Interval> elems = new List<Interval>();
        for (int i = 0; i < Intervals.Count; i++)
        {
            if (Intervals[i].Length < len)
                elems.Add(Intervals[i]);
        }

        foreach (var item in elems)
            Intervals.Remove(item);

        foreach (var item in Intervals)
        {
            if (item.Start < Start) Start = item.Start;
            if (item.End > End) End = item.End;
        }
        Count--;
        HasHoles = IsHole();
    }

    public Interval GetLongest()
    {
        int resIdx = 0;
        for (int i = 0; i < Intervals.Count; i++)
        {
            if (Intervals[resIdx].Length < Intervals[i].Length)
                resIdx = i;
        }

        return Intervals[resIdx];
    }

    public Interval GetShortest()
    {
        int resIdx = 0;
        for (int i = 0; i < Intervals.Count; i++)
        {
            if (Intervals[resIdx].Length > Intervals[i].Length)
                resIdx = i;
        }

        return Intervals[resIdx];
    }

    public void RemoveByPos(int pos) 
    {
        Intervals.RemoveAt(pos);
        foreach (var item in Intervals)
        {
            if (item.Start < Start) Start = item.Start;
            if (item.End > End) End = item.End;
        }
        Count--;
        HasHoles = IsHole();
    }
    public void RemoveByVal(Interval inter)
    {
        Intervals.Remove(inter);

        foreach (var item in Intervals)
        {
            if (item.Start < Start) Start = item.Start;
            if (item.End > End) End = item.End;
        }
        Count--;
        HasHoles = IsHole();
    }

    public static Collection operator+(Collection collec, Interval inter)
    {
        Collection reCollec = new Collection(collec);
        reCollec.Add(inter);
        reCollec.End = reCollec.Intervals[reCollec.Intervals.Count - 1].End;
        return reCollec;
    }
    
    public static Collection operator-(Collection collec, Interval inter)
    {
        Collection reCollec = new Collection(collec);
        reCollec.RemoveByVal(inter);
        reCollec.Start = reCollec.Intervals[0].Start;
        reCollec.End = reCollec.Intervals[reCollec.Intervals.Count - 1].End;
        return reCollec;
    }

    public static bool operator==(Collection collec1, Collection collec2) { return collec1.Intervals.Equals(collec2.Intervals); }
    public static bool operator!=(Collection collec1, Collection collec2) { return !collec1.Intervals.Equals(collec2.Intervals); }

    public void Save()
    {
        FileStream fs = new FileStream("data.log", FileMode.Create, FileAccess.Write, FileShare.None);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, this);
        fs.Close();
    }

    public void Load()
    {
        FileStream fs = File.OpenRead("data.log");
        BinaryFormatter bf = new BinaryFormatter();
        Collection? collec = (Collection)bf.Deserialize(fs);
        fs.Close();

        Intervals = new List<Interval>(collec.Intervals);
        HasHoles = collec.HasHoles;
        Start = collec.Start;
        End = collec.End;
        Count = collec.Count;
    }

    public IEnumerator GetEnumerator() { return Intervals.GetEnumerator(); }
    public Interval this[int idx] { get => Intervals[idx]; }
}