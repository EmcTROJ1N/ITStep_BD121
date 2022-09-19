public abstract class SortStrategy
{
    public abstract void Sort(int[] arr, Comparator comp);
    protected void Swap(ref int el1, ref int el2)
    {
        var temp = el1;
        el1 = el2;
        el2 = temp;
    }
}