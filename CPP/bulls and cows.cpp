#include <iostream>
#include <time.h>
#include <vector>
#include <algorithm>

using namespace std;

int isPerm (long num1, long num2) {
    int digits[10];
    int i;

    for (i = 0; i < 10; i++)      // Init all counts to zero.
        digits[i] = 0;

    while (num1 != 0) {           // Process all digits.
        digits[num1%10]++;        // Increment for least significant digit.
        num1 /= 10;               // Get next digit in sequence.
    }

    while (num2 != 0) {           // Same for num2 except decrement.
        digits[num2%10]--;
        num2 /= 10;
    }

    for (i = 0; i < 10; i++)
        if (digits[i] != 0)       // Any count different, not a permutation.
            return false;

    return true;                  // All count identical, was a permutation.
}

int main()
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
    int number[4]; //Генерация случайного числа
    //int numb = rand() % (9999 - 1000 + 1) + 1000;

    int numb = 1984;

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

        int numberTwo[4];
        tmpVarNumb = numbTwo;

        // Получение массива цифр из введенного числа
        for (int i = 3; i >= 0; i--)
        {
            numberTwo[i] = tmpVarNumb % 10; 
            tmpVarNumb /= 10;
        }

        if ((numberTwo[0] == numberTwo[1]) && (numberTwo[2] == numberTwo[3]) && (numberTwo[0] == numberTwo[2]))
        {
            cout << "Вы ввели неверное число..."; // Проверка числа
            break;
        }

        countCoincidencesPos = 0;
        countCoincidences = 0;

        for (int i = 0; i < 4; i++)// Проверка быков и коров
        {
            for (int j = 0; j < 4; j++)
            {
                if (number[i] == numberTwo[j])  
                {
                    if (i == j)
                        countCoincidencesPos++;
                    else
                        countCoincidences++;
                }

            }
        }

        cout << countCoincidences << " " << countCoincidencesPos << endl; // Вывод результатов
        
        // Проверка на угаданное число
        if (countCoincidencesPos == 4)
        {
            cout << "Krasavcheg";
            break;
        }
        

        // Отчечение неверных вариантов
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


        if ((countCoincidences == 2 && countCoincidencesPos == 2) || (countCoincidences == 3 && countCoincidencesPos == 1))
        {
            numbers.erase(remove(numbers.begin(), numbers.end(), numbTwo), numbers.end());
            for (int i = 0; i < numbers.size(); i++)
            {
                bool flag = isPerm(numbTwo, numbers[i]); 
                if (flag == false)
                {
                    numbers.erase(remove(numbers.begin(), numbers.end(), numbers[i]), numbers.end());
                    i--;
                }
            }
        }
        cout << numbers.size() << endl;
    } 
}