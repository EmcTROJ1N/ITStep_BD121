#include <iostream>
#include <cstring>

using namespace std;


/*
- число не должно быть содержать лишних символов
любой знак должен встречаться не более одного раза
плюс и минус должны быть в начале числа

*/

int main()
{
    char numb[20];
    cin >> numb;

    bool flag = true;
    // Проверка на лишние символы
    for (int i = 0; i < strlen(numb); i++)
    {
        if ((int)numb[i] < 48 || (int)numb[i] > 57)
        {
            if ((int)numb[i] != 43 && (int)numb[i] != 45 && (int)numb[i] != 46)
            {
                flag = false;
            }
        }
    }

    int kPlus = 0;
    int kMinus = 0;
    int kPoint = 0;
    for (int i = 0; i < strlen(numb); i++)
    {
        if (numb[i] == '.') kPoint++;
        if (numb[i] == '+') kPlus++;
        if (numb[i] == '-') kMinus++;
    }

    if (kPlus > 1 || kMinus > 1 || kPoint > 1) flag = false;

    for (int i = 0; i < strlen(numb); i++)
    {
        if (numb[i] == '+' && i != 0) flag = false;
        if (numb[i] == '-' && i != 0) flag = false;
    }

    if (flag)
        cout << "Yes.";
    else
        cout << "No.";


    return 0;
}