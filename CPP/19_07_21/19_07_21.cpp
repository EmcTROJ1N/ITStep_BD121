#include <iostream>

using namespace std;


int* inputArr(int size)
{
    int *arr = new int(size);
    for (int i = 0; i < size; i++)
    {
        cin >> arr[i];
    }
    return arr;
}

void outputArr(int arr[], int size)
{
    for (int i = 0; i < size; i++)
    {
        cout << arr[i];
    }
}

void task_1()
{
    int k;
    cin >> k;
    int *arr = new int[k];
    arr = inputArr(k);

    int count = 0;
    int j = 0;
    for (int i = 0; i < k; i++)
    {
        if (arr[i] % 2 != 0)
            count++;
    }

    int *arr2 = new int[count];
    for (int i = 0; i < k; i++)
    {
        if (arr[i] % 2 != 0)
        {
            arr2[j] = arr[i];
            j++;
        }
    }
}

int task_3(int arr[], int size)
{
    int k = 0;
    for (int i = 0; i < size; i++)
    {
        if (i % 3 == 0 && i % 2 == 0 && i % 5 == 0)
            k++;
    }
    return k;
}

void task_2()
{
    int k;
    cin >> k;
    int *arr = new int[k];
    arr = inputArr(k);

    int countPos = 0;
    int countNeg = 0;
    int jPos = 0;
    int jNeg = 0;

    for (int i = 0; i < k; i++)
    {
        if (arr[i] < 0)
            countNeg++;
        else
            countPos++;
    }

    int *arr2 = new int[countPos];
    int *arr3 = new int[countNeg];

    for (int i = 0; i < k; i++)
    {
        if (arr[i] < 0)
        {
            arr2[jNeg] = arr[i];
            jNeg++;
        }
        else
        {
            arr2[jPos] = arr[i];
            jPos++;
        }
    }
}



int main()
{
    task_1();


    cout << endl << endl;
    return 0;
}
