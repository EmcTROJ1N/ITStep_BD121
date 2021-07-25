#include <iostream>
#include <time.h>
#include <vector>
#include <algorithm>
#include <set>

using namespace std;

int isPerm (long num1, long num2) {
    int digits[10];
    int i;

    for (i = 0; i < 10; i++)    
        digits[i] = 0;

    while (num1 != 0) {          
        digits[num1%10]++;       
        num1 /= 10;              
    }

    while (num2 != 0) {          
        digits[num2%10]--;
        num2 /= 10;
    }

    for (i = 0; i < 10; i++)
        if (digits[i] != 0)       
            return false;

    return true;                  
}

bool isRepeatNumb(int number) {
    std::set<int> d;
    int counter = 0;
    
    while (number != 0) {
        d.insert(number % 10);
        number /= 10;
        counter++;
    }
    
    return counter != d.size();
}

bool isRepeatNumbs(int num1, int num2)
{
    int lDigit;
    for (int j = 0, k = 1000; j < 4; j++, k /= 10)
        {
            int tmpVar = num1;
            while (tmpVar != 0)
            {
                lDigit = tmpVar % 10;
                tmpVar /= 10;
                if (lDigit == (num2 / k) % 10)
                    return true;
            }
        }
        return false;
}

int main()
{
    srand(time(0));
    int counter = 0;

    vector<int> numbers;
    vector<int> lNumbers;

    for (int i = 1000, j = 0; j < 5040; j++, i++) // Заполнение вектора
    {
        if (isRepeatNumb(i) == false)
            numbers.push_back(i);
    }
    
    int number[4]; 
    
    //Генерация случайного числа
    int numb = numbers[rand() % numbers.size()];
    int tmpVarNumb = numb;
    for (int i = 3; i >= 0; i--)
    {
        number[i] = tmpVarNumb % 10; // Создание массива цифр из загаданного числа
        tmpVarNumb /= 10;
    }

    int sumCountCoincidences = 0;
    int sumCountCoincidencesPos = 0;

    int lcountCoincidencesPos;
    int lcountCoincidences;
    int countCoincidencesPos;
    int countCoincidences;
    int numbTwo;
    int lNumb;
    int index;
    bool flager = false;

    while (true)
    {
        counter++;
        if (counter == 1) numbTwo = 1234;
        if (counter == 2) numbTwo = 5678;
       if (counter >= 3)
        {
            if (flager == false)
            {
                index = -1;
                do
                {
                    index++;
                } while (isRepeatNumbs(numbers[index], lNumb));
                numbTwo = numbers[index];
            }
            else
            {
                index = rand() % numbers.size();
                numbTwo = numbers[index];
            }
        }
        
        int numberTwo[4];
        tmpVarNumb = numbTwo;

        // Получение массива цифр из введенного числа
        for (int i = 3; i >= 0; i--)
        {
            numberTwo[i] = tmpVarNumb % 10; 
            tmpVarNumb /= 10;
            lNumbers.push_back(numberTwo[i]);
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
            cout << endl << numb << endl;
            cout << "Krasavcheg" << endl;
            break;
        }
        else
        {
            numbers.erase(remove(numbers.begin(), numbers.end(), numbTwo), numbers.end());
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

        if ((countCoincidences == 2 && countCoincidencesPos == 2) || (countCoincidences == 3 && countCoincidencesPos == 1) || (countCoincidences == 4))
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
                    cout << endl << numb << endl;
                    cout << "Krasavcheg." << endl;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                break;
            }
        }
        
        if (flager == false)
        {
            sumCountCoincidencesPos += countCoincidencesPos;
            sumCountCoincidences += countCoincidences;
        }
        
        if (sumCountCoincidences + sumCountCoincidencesPos >= 4)
        {
                flager = true;
                int lDigit;
                int tmpVar;
                for (int i = 0; i < numbers.size(); i++)
                {
                    bool flag = true;
                    tmpVar = numbers[i];
                    while (tmpVar != 0)
                    {
                        lDigit = tmpVar % 10;
                        tmpVar /= 10;
                        if (find(lNumbers.begin(), lNumbers.end(), lDigit) != lNumbers.end() == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        numbers.erase(remove(numbers.begin(), numbers.end(), numbers[i]), numbers.end());
                        i--;
                    }
                }

        } 
        if (counter % 2 == 0)
        {
            lNumbers.clear();
            sumCountCoincidences = 0;
            sumCountCoincidencesPos = 0;
        }

        if (flager == true)
        {
            sumCountCoincidences = 0;
            sumCountCoincidencesPos = 0;
        }
        cout << "Vector size: " << numbers.size() << " " << endl;
        
        lNumb = numbTwo;
        lcountCoincidences = countCoincidences;
        lcountCoincidencesPos = lcountCoincidencesPos;
    } 

    cout << "Counter: " << counter << endl << endl;
}