#include <iostream>
#include <time.h>
#include <vector>
#include <algorithm>
#include <set>

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

bool isRepeat(int number) {
    std::set<int> d;
    int counter = 0;
    
    while (number != 0) {
        d.insert(number % 10);
        number /= 10;
        counter++;
    }
    
    return counter != d.size();
}

int main()
{
    srand(time(0));
    int counter = 1;

    vector<int> numbers;

    for (int i = 1000, j = 0; j < 5040; j++, i++) // Заполнение вектора
    {
        if (isRepeat(i) == false)
            numbers.push_back(i);
    }
   
    for (int i = 1111; i < 9999; i += 1111)
    {
        numbers.erase(remove(numbers.begin(), numbers.end(), i), numbers.end());
    }

    int index = rand() % numbers.size();
    int number[4]; //Генерация случайного числа
    //int numb = rand() % (9999 - 1000 + 1) + 1000;

    int numb = 1254;

    int tmpVarNumb = numb;
    for (int i = 3; i >= 0; i--)
    {
        number[i] = tmpVarNumb % 10; // Создание массива цифр из загаданного числа
        tmpVarNumb /= 10;
    }

    int sumCountCoincidences = 0;
    int sumCountCoincidencesPos = 0;

    int countCoincidencesPos;
    int countCoincidences;
    int numbTwo;
    while (true)
    {
        if (counter == 1) numbTwo = 1234;
        if (counter == 2) numbTwo = 5678; 
        if (counter == 3)
        {
            if (sumCountCoincidencesPos + sumCountCoincidences >= 4)
            {
                int tmpVar;
                int lDigit;
                for (int i = 0; i < numbers.size(); i++)
                {
                    tmpVar = numbers[i];
                    while (tmpVar != 0)
                    {
                        lDigit = tmpVar % 10;
                        tmpVar /= 10;
                        if (lDigit == 9 || lDigit == 0)
                        {
                            numbers.erase(remove(numbers.begin(), numbers.end(), numbers[i]), numbers.end());
                            i--;
                            break;
                        }
                    }
                }
            }
        }
        if (counter == 4)
            cin >> numbTwo;
        //cin >> numbTwo;

        int numberTwo[4];
        tmpVarNumb = numbTwo;

        // Получение массива цифр из введенного числа
        for (int i = 3; i >= 0; i--)
        {
            numberTwo[i] = tmpVarNumb % 10; 
            tmpVarNumb /= 10;
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

        cout << numbTwo << " " << countCoincidences << " " << countCoincidencesPos << endl; // Вывод результатов
        
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

            bool flag = false;
            for (int i = 0; i < numbers.size(); i++)
            {
                if (numbers[i] == numb)
                {
                    cout << "Krasavcheg.";
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                break;
            }
        }
        

        if (counter <= 2)
        {
            sumCountCoincidencesPos += countCoincidencesPos;
            sumCountCoincidences += countCoincidences;
        }
        
        counter++;
        cout << "Vector size: " << numbers.size() << endl;
    } 
}