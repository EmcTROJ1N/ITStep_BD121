public class SelectionSort : SortStrategy
{
    void Swap(ref int el1, ref int el2)
    {
        var temp = el1;
        el1 = el2;
        el2 = temp;
    }
    public void Sort(int[] arr, Comparator comp)
    {
        int idx;
        for (int i = 0; i < arr.Length; i++)
        {
            idx = i;
            for (int j = i; j < arr.Length; j++) 
            {
                if (comp.Comp(arr[idx], arr[j]))
                    idx = j;
            }
            if (arr[idx] == arr[i])
                continue;

            Swap(ref arr[i], ref arr[idx]);
        }
    }
}