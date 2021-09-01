#include <iostream>
#include <cstring>

using namespace std;

void stringRev()
{
    char string[] = "hello";
    char revString[20];
    for (int i = strlen(string) - 1, j = 0; i >= 0; i--, j++)
    {
        revString[j] = string[i];
    }
}

void task_1()
{
    char string[20];
    char ch;
    int counter = 0;
    cin >> string >> ch;
    for (int i = 0; i < strlen(string); i++)
    {
        if (string[i] == ch)
            counter++;
    }
    cout << counter;
}

void task_2()
{
    char string[20];
    char revString[20];
    char string2[20];
    cin >> string >> string2;

    for (int i = strlen(string) - 1, j = 0; i >= 0; i--, j++)
    {
        revString[j] = string[i];
    }
    bool flag = true;
    if (strlen(revString) == strlen(string2))
    {
        for (int i = 0; i < strlen(revString); i++)
        {
            if (revString[i] != string2[i])
                flag = false;
        }
    }
    else
    {
        flag = false;
    }
    if (flag == true)
    {
        cout << "yes.";
    }
    else
    {
        cout << "No.";
    }
}

void task_4()
{
    char string[20];
    cin >> string;
    char string2[20];
    bool flag;
    int x = 0;
    for (int i = 0; i < 20; i++)
    {
        flag = true;
        for (int j = 0; j < strlen(string2); j++)
        {
            if (string[i] == string2[j])
                flag = false;
        }
        if (flag == true)
        {
            string2[x] = string[i];
            x++;
        }
    }
    cout << string2;
}


int main()
{
    task_4();
}