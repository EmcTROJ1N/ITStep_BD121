class BubbleSort : SortStrategy
{
    public override void Sort(int[] arr, Comparator comp)
    {
        var len = arr.Length;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                if (comp.Comp(arr[j], arr[j + 1]))
                {
                    Swap(ref arr[j], ref arr[j + 1]);
                }
            }
        }
    }
}