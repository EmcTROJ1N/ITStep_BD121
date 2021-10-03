#include <iostream>
#include <cstring>

using namespace std;

bool recursion_task1(FILE* file, char *symb)
{
    char str[666];
    strcpy(str, "");
    fgets(str, 666, file);
    for (int i = 0; i < strlen(str); i++)
    {
        if (str[i] != symb[0] && str[i] != '\n') 
            return false;
    }

    cout << str;
    
    if (!feof(file))
    {
      recursion_task1(file, symb);
    }
    return true;
}

void recursion_task2(int a, int b)
{
    cout << a << endl;
    if (a < b)
        recursion_task2(++a, b);
}

void recursion_task3(int i, char *str)
{
    cout << str[i] << endl;

    if (i < strlen(str))
        recursion_task3(++i, str);
}

void task_1()
{
    char filename[20];
    cin >> filename;
    FILE* file;
    file = fopen(filename, "r");
    
    char symb[1];
    fgets(symb, 2, file);

    if (recursion_task1(file, symb)) cout << "yes.";
    else cout << "no.";
}

void task_2()
{
    int a, b;
    cin >> a >> b;
    recursion_task2(a, b);
}

void task_3()
{
    char str[666];
    cin >> str;
    recursion_task3(0, str);
}

int main()
{
    task_3();
    return 0;
}