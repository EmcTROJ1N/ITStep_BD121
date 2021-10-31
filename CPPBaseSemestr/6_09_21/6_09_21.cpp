#include <iostream>
#include <string>
#include <cstring>

using namespace std;

bool isNumb(char* numb)
{
    bool flag = true;
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
    return flag;
}

void task_1()
{
    FILE* file;
    char filename[20];
    cin >> filename;
    char numb[20];
    file = fopen(filename, "w");
    if (file != NULL)
    {
        while (true)
        {
            cin >> numb; 
            if (strcmp(numb, "exit") == 0) break;
            if (isNumb(numb) == false) continue;
            if (stoi(numb) > 0) fputs(numb, file);
            fputs("\n", file);
        }

        fclose(file);
    }
}

void task_2()
{
    FILE* file;
    char filename[20];
    cin >> filename;
    int counter = 0;
    char str[20];
    file = fopen(filename, "r");
    if (file != NULL)
    {
        while (!feof(file))
        {
            strcpy(str, "");
            fgets(str, 79, file);
            for (int i = 0; i < strlen(str); i++)
            {
                if (str[i] == 'a' || str[i] == 'e' || str[i] == 'i' || str[i] == 'o' || str[i] == 'u')
                    counter++;
            }
        }
        fclose(file);
    }
    cout << counter;
}

void task_3()
{
    FILE* file;
    char filename[20];
    cin >> filename;
    int counter = 0;
    char str[20];
    file = fopen(filename, "r");
    if (file != NULL)
    {
        while (!feof(file))
        {
            strcpy(str, "");
            fgets(str, 79, file);
            for (int i = 0; i < strlen(str); i++)
            {
                if ((int)str[i] > 48 && (int)str[i] > 57)
                    counter++;
            }
        }
        fclose(file);
    }
    cout << counter;
}

int main()
{
    return 0;
}