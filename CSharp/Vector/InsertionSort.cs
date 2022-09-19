class InsertionSort : SortStrategy
{
    public override void Sort(int[] arr, Comparator comp)
    {
        for (var i = 0; i < arr.Length; i++)
        {
            var key = arr[i];
            var j = i;
            while ((j > 0) && comp.Comp(arr[j - 1], key))
            {
                Swap(ref arr[j - 1], ref arr[j]);
                j--;
            }

            arr[j] = key;
        }
    }
}