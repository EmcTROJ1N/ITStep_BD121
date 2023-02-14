using System.Text.RegularExpressions;
using System;

[Serializable]
class Interval : IComparable
{
    public double Start { get; set; } = 0.0;
    public double End { get; set; } = 0.0;
    public double Length { get; private set; }
    public Interval(double start, double end)
    {
        Start = start;
        End = end;
        Length = Math.Abs(Start - End);
    }
    public Interval(Interval source)
    {
        Start = source.Start;
        End = source.End;
        Length = source.Length;
    }

    public override bool Equals(object? obj)
    {
        Interval inter = (Interval)obj;
        if (inter?.Start == this.Start && inter?.End == this.End)
            return true;
        else
            return false;
    }

    public override string ToString()
    {
        return $"{Start} {End}";
    }

    int IComparable.CompareTo(object? obj)
    {
        return Start.CompareTo((obj as Interval)?.Start);
    }

    // public double this[int idx] { get => Lst[idx]; }
}