#include <iostream>
#include <time.h>
#include <vector>
#include <algorithm>

using namespace std;

void bullsCows()
{
    srand(time(0));

    vector<int> numbers;
    for (int i = 1000, j = 0; j < 8999; j++, i++)
    {
        numbers.push_back(i);
    }

    for (int i = 1111; i < 9999; i += 1111)
    {
        numbers.erase(remove(numbers.begin(), numbers.end(), i), numbers.end());
    }

    int index = rand() % numbers.size();
    numbers[index];






    int number[4]; //Генерация случайного числа
    int numb = rand() % (9999 - 1000 + 1) + 1000;
    int tmpVarNumb = numb;
    for (int i = 3; i >= 0; i--)
    {
        number[i] = tmpVarNumb % 10; // Создание массива цифр из загаданного числа
        tmpVarNumb /= 10;
    }

    int countCoincidencesPos;
    int countCoincidences;
    int numbTwo;

    while (true)
    {
        cin >> numbTwo;

        if (numbTwo == numb)
        {
            // Проверка на угаданное число
            cout << "Krasavcheg";
            break;
        }

        int numberTwo[4];
        tmpVarNumb = numbTwo;
        bool flag = true;

        for (int i = 3; i >= 0; i--)
        {
            numberTwo[i] = tmpVarNumb % 10; // Получение массива цифр из введенного числа
            tmpVarNumb /= 10;
        }

        if (numberTwo[0] == numberTwo[1] == numberTwo[2] == numberTwo[3])
        {
            cout << "Вы ввели неверное число..."; // Проверка числа
            break;
        }

        countCoincidencesPos = 0;
        countCoincidences = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (number[i] == numberTwo[j])  // Проверка быков и коров
                {
                    if (i == j)
                        countCoincidencesPos++;
                    else
                        countCoincidences++;
                }

            }
        }

        cout << countCoincidences << " " << countCoincidencesPos << endl; // Вывод результатов

        if (countCoincidencesPos == 0 && countCoincidences == 0)
        {
            for (int j = 0, k = 1000; j < 4; j++, k /= 10)
            {
                for (int i = 0; i < numbers.size(); i++)
                {
                    if (numberTwo[j] == (numbers[i] / k) % 10)
                    {
                        numbers.erase(remove(numbers.begin(), numbers.end(), numbers[i]), numbers.end());
                    }
                }
            }
        }
    }
}

int main()
{
}

