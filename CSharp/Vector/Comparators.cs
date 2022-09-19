public abstract class Comparator
{
    public abstract bool Comp(int el1, int el2);
}

public class Increasing : Comparator
{
    public override bool Comp(int el1, int el2)
    {
        if (el1 < el2) return false;
        else return true;
    }
}

public class Decreasing : Comparator
{
    public override bool Comp(int el1, int el2)
    {
        if (el1 > el2) return false;
        else return true;
    }
}