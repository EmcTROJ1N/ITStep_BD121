class BubbleSort : SortStrategy
{ 
    void Swap(ref int el1, ref int el2)
    {
        var temp = el1;
        el1 = el2;
        el2 = temp;
    }
    public void Sort(int[] arr, Comparator comp)
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