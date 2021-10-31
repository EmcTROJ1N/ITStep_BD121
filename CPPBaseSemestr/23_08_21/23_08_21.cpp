#include <iostream>

using namespace std;

void task_1()
{
    char string[20];
    int len = 0;
    cin >> string;
    for (int i = 0; i < 20; i++)
    {
        if (string[i] != 0)
            len++;
        else
            break;
    }
    cout << len;
}

void task_2()
{
    char string[20];
    int countNumbs = 0;
    cin >> string;
    for (int i = 0; i < 20; i++)
    {
        if ((int)string[i] >= 48 && (int)string[i] <= 57)
            countNumbs++;
        if (string[i] == 0) 
            break;
    }
    cout << countNumbs;
}

void task_3()
{
    char string[20];
    cin >> string;
    for (int i = 0; i < 20; i++)
    {
        if (string[i] == 'a')
            string[i] = '!';
        if (string[i] == 0) 
            break;
    }
    cout << string;
}

void task_4()
{ 
    char string[20];

    cin >> string;

    int len = 0;
    
    for (int i = 0; i < 20; i++)
    {
        if (string[i] != 0)
            len++;
        else
            break;
    }
    
    for (int i = len; i >= 0; i--)
    {
        cout << string[i];
    }
}

int main()
{
    task_4();

    return 0;
}