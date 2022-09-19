public class SelectionSort : SortStrategy
{
    public override void Sort(int[] arr, Comparator comp)
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