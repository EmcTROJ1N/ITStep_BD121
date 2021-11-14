#include <iostream>

using namespace std;

int main()
{
    int size;
    cin >> size;
    int* arr = new int[size];
    for (int i = 0; i < size; i++)
        cin >> arr[i];
    
    int sum = 0;
    for (int i = 0; i < size; i++)
        sum += arr[i];
    
    int srAr = sum / size;
    int sumNeg = 0;
    int countTF;

    for (int i = 0; i < size; i++)
    {
        if (arr[i] % 2 == -1)
            sumNeg += arr[i];
        if (arr[i] % 3 == 0 && arr[i] % 5 == 0)
            countTF++;
    }

    cout << "---" << endl;
    cout << srAr << endl << sumNeg << endl << countTF;

    return 0;
}