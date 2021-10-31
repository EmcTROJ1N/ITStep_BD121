#include <iostream>
#include <cstring>

using namespace std;

void task_1()
{
    char string[20];
    cin >> string;
    char rev, sep;
    cin >> rev, sep;
    for(int i = 0; i < strlen(string); i++)
    {
        if (string[i] = rev)
            string[i] = sep;
    }
    cout << string;
}

void task_2()
{
    char string[20];
    cin >> string;
    char symbol = string[0];
    bool flag = true;
    for (int i = 0; i < strlen(string); i++)
    {
        if (string[i] != symbol)
        {
            flag = false;
            break;
        }
    }
    if (flag) cout << "yes.";
    else cout << "no.";
}

void task_3()
{
    char string[20];
    cin >> string;
    bool flag = false;
    for (int i = 0; i < strlen(string); i++)
    {
        for (int j = i + 1; j < strlen(string); j++)
        {
            if (string[i] == string[j])
            {
                flag = true;
                break;
            }
        }
    }
    if (flag == true) cout << "yes.";
    else cout << "no.";
}

int main()
{
    task_3();
    return 0;
}