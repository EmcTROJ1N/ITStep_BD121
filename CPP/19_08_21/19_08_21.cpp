#include <iostream>
#include <string>
#include <ncurses.h>

using namespace std;

void enter_array(int a[], int size)
{
	for (int i = 0; i < size; i++)
	{
		cin >> a[i];
	}
}

void print_array(int* a, int size)
{
	for (int i = 0; i < size; i++)
	{
		cout << a[i] << " ";
	}
	cout << endl;
}

void enter_dyn_array2D(int** a, int rows, int cols)
{
	for (int i = 0; i < rows; i++) {
		for (int k = 0; k < cols; k++) {
			cin >> a[i][k];
		}
	}
}

void print_dyn_array2D(int** a, int rows, int cols)
{
	for (int i = 0; i < rows; i++) {
		for (int k = 0; k < cols; k++) {
			cout << a[i][k] << " ";
		}
		cout << endl;
	}
	cout << endl;
}


void task_1()
{

	int rows, cols;
	cin >> rows >> cols;
    int** arr = new int* [rows];
	for (int i = 0; i < rows; i++)
	{
		arr[i] = new int[cols];
	}

    enter_dyn_array2D(arr, rows, cols);
    
    int countNumbs = 0;
    
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if ((arr[i][j] < 0) && (arr[i][j] % 2 == 0))
                countNumbs++;
        }
    }

    int* arr2 = new int[countNumbs];
    int k = 0;
    
    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if ((arr[i][j] < 0) && (arr[i][j] % 2 == 0))
            {
                arr2[k] = arr[i][j];
                k++;
            }
        }
    }
    print_array(arr2, countNumbs);
}

void task_2()
{
    int k1;
    cin >> k1;
    int* arr1 = new int[k1];
    enter_array(arr1, k1);

    int k2;
    cin >> k2;
    int* arr2 = new int[k2];
    enter_array(arr2, k2);

    int k3 = 0;

    for (int i = 0; i < k1; i++)
    {
        for (int j = 0; j < k2; j++)
        {
            if (arr1[i] == arr2[j])
                k3++;
        }
    }

    int counter = 0;
    int* arr3 = new int[k3];

    for (int i = 0; i < k1; i++)
    {
        for (int j = 0; j < k2; j++)
        {
            if (arr1[i] == arr2[j])
            {
                bool flag = true;
                for (int x = 0; x < counter; x++)
                {
                    if (arr3[x] == arr1[i])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    arr3[counter] = arr1[i];
                    counter++;
                }
            }
        }
    }
    for (int i = 0; i < counter; i++)
    {
        cout << arr3[i];
    }
    // print_array(arr3, k3);

}

void task_3()
{
    char ch;
    int counter = 0;
    while (true)
    {
        cin >> ch;
        int counter = 0;
        if ((int)ch == 113)
            break;
        if ((int)ch > 122 && (int)ch < 97)
        {
            if ((int)ch > 90 && (int)ch < 65)
            {
                if ((int)ch < 48 && (int)ch > 57)
                {
                    counter++;
                }
            }
        }
        cout << (int)ch << endl;
    }
    cout << counter;
}

void task_4()
{
    char ch;
    int counter = 0;
    while (true)
    {
        cin >> ch;
        //ch = getch();
        int counter = 0;
        if ((int)ch < 48 && (int)ch > 57)
            break;

        if ((int)ch <= 90 && (int)ch >= 65)
        {
            counter++;
        }
        cout << (int)ch << endl;
    }
    cout << counter;
}

int main()
{
    task_2();
    return 0;
}