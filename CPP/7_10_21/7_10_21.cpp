#include <iostream>
#include <string>

using namespace std;

void task_1()
{

}

int task_2(char* str, int i = 0, int k = 0)
{
    if (str[i] == 'a' || str[i] == 'e' || str[i] == 'i' || str[i] == 'o' || str[i] == 'u')
        k++;

    if (str[i] != 0)
    {
        k = task_2(str, ++i, k);
    }
    return k;

}


char* task_3(char* str, int i = 0)
{
    if (str[i] >= '0' && str[i] <= '9')
        str[i] = '!';

    if (str[i] != 0)
    {
        str = task_3(str, ++i);
    }
    return str;

}

int main()
{
    char str[20];
    cin >> str;
    cout << task_3(str);

    return 0;
}