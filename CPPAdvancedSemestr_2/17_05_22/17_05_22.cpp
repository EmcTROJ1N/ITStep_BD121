#include <iostream>
#include <ctime>

using namespace std;

class Array
{
private:
    // поля
    int* Arr;
    int Size;
public:
    
    ~Array() { delete[] Arr; } // деструктор
   
    Array() // конструктор без параметра
    {
        Size = 5;
        int a = -10;
        int b = 10;
        Arr = new int[Size];
        for (int i = 0; i < Size; i++)
            Arr[i] = rand() % (b - a + 1) + a;
    }

    Array(const int size) // конструктор с параметром
    {
        int a = -10;
        int b = 10;
        Size = size;
        Arr = new int[Size];
        for (int i = 0; i < Size; i++)
            Arr[i] = rand() % (b - a + 1) + a;
    }
    
    void printArr() // выводит массив на экран
    {
        for (int i = 0; i < Size; i++)
            cout << Arr[i] << " ";
        cout << endl;
    }

    int getSize() { return Size; } // получить размер массиваj
    int* getArr() { return Arr; } // возвращает сам массив
    bool set(int position, int value) 
    { 
        if (position < Size)
        {
            Arr[position] = value; 
            return true;
        }
        else
            return false;
    }
    int getValue(int position)
    {
        if (position < Size) 
            return Arr[position];
        else
            return 0;
    }
    int sum()
    {
        int sum = 0;
        for (int i = 0; i < Size; i++)
            sum += Arr[i];
        return sum;
    }
    int min()
    {
        int min = Arr[0];
        for (int i = 0; i < Size; i++)
        {
            if (Arr[i] < min)
                min = Arr[i];
        }
        return min;
    }

    int max()
    {
        int max = Arr[0];
        for (int i = 0; i < Size; i++)
        {
            if (Arr[i] > max)
                max = Arr[i];
        }
        return max;
    }
    bool writeToBin()
    {
        FILE* file = fopen("output.bin", "wb");
        if (file == NULL)
            return false;
        fwrite(&Size, sizeof(int), 1, file);
        fwrite(Arr, sizeof(int), Size, file);
        fclose(file);
        return true;
    }

    bool readToBin()
    {
        FILE* file = fopen("output.bin", "rb");
        if (file == NULL)
            return false;
        fread(&Size, sizeof(int), 1, file);
        delete[] Arr;
        Arr = new int[Size];
        fread(Arr, sizeof(int), Size, file);
        fclose(file);
        return true;
    }

    bool deleteNegative()
    {
        int newSize = 0;
        for (int i = 0; i < Size; i++)
        {
            if (Arr[i] >= 0)
                newSize++;
        }
        int* Arr2 = new int[newSize];
        for (int i = 0, j = 0; i < Size; i++)
        {
            if (Arr[i] >= 0)
            {
                Arr2[j] = Arr[i];
                j++;
            }
        }
        delete[] Arr;
        Size = newSize;
        int* Arr = new int[Size];
        for (int i = 0; i < Size; i++)
            Arr[i] = Arr2[i];
        delete[] Arr2;
        return true;
    }
};


int main()
{
    srand(time(0));
    Array arr(10);
    
    arr.printArr();
    arr.deleteNegative();
    arr.printArr();
    cout << endl;

    return 0;
}