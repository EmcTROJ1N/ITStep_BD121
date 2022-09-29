class QuickSort : SortStrategy
{
    void Swap(ref int el1, ref int el2)
    {
        var temp = el1;
        el1 = el2;
        el2 = temp;
    }
    int Partition(int[] arr, int minIndex, int maxIndex, Comparator comp)
    {
        var pivot = minIndex - 1;
        for (int i = minIndex; i < maxIndex; i++)
        {
            if (comp.Comp(arr[maxIndex], arr[i]))
            {
                pivot++;
                Swap(ref arr[pivot], ref arr[i]);
            }
        }

        pivot++;
        Swap(ref arr[pivot], ref arr[maxIndex]);
        return pivot;
    }

    int[] qs(int[] array, int minIndex, int maxIndex, Comparator comp)
    {
        if (minIndex >= maxIndex)
            return array;

        var pivotIndex = Partition(array, minIndex, maxIndex, comp);
        qs(array, minIndex, pivotIndex - 1, comp);
        qs(array, pivotIndex + 1, maxIndex, comp);

        return array;
    }

    public void Sort(int[] arr, Comparator comp)
    {
        arr = qs(arr, 0, arr.Length - 1, comp);
    }
}