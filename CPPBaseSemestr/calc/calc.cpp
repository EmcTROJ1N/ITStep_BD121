#include <iostream>
#include <string>
#include <vector>
#include <stdlib.h>

using namespace std;

vector<string> split(const string& str, const string& delim)
{
    vector<string> tokens;
    size_t prev = 0, pos = 0;
    do
    {
        pos = str.find(delim, prev);
        if (pos == string::npos) pos = str.length();
        string token = str.substr(prev, pos - prev);
        if (!token.empty()) tokens.push_back(token);
        prev = pos + delim.length();
    } while (pos < str.length() && prev < str.length());
    return tokens;
}

int calc(int a, int b, char symb)
{
    int res;
    if (symb == '/' && b == 0)
    {
        cout << "Invalid input";
        exit(0);
    }
    switch (symb)
    {
        case '+': res = a + b; break;
        case '-': res = a - b; break;
        case '*': res = a * b; break;
        case '/': res = a / b; break;
    }
    return res;
}

bool isValid(string N)
{
    bool flag = true;
    for (int i = 0; i < N.size(); i++)
    {
        if (i != 0)
        {
            if (N[i] == N[i - 1])
            {
                if (!((int)N[i] > 48 && (int)N[i] < 57))
                    flag = false;
            }
        }
    }
    for (int i = 0; i < N.size(); i++)
    {
        if (!(((int)N[i] >= 48 && (int)N[i] <= 57) || N[i] == '*' || N[i] == '/' || N[0] == '-' || N[0] == '+'))
            flag = false;
    }
    if (N.size() == 1)
    {
        if (N[0] == '-' || N[0] == '+' || N[0] == '*' || N[0] == '/')
            flag = false;
    }

    if (N.size() == 0)
        flag = false;

    return flag;
}


int calcNumbs(string N)
{
    vector<string> numbers;
    vector<char> symbols;
    
    if (N.size() == 1)
    {
        if (N[0] == '+' || N[0] == '-' || N[0] == '/' || N[0] == '*')
        {
            cout << "Invalid input";
            exit(0);
        }
    }

    for (int i = 0; i < N.size(); i++)
    {
        if (N[i] == ' ') N.erase(i, 1);
        if (N[i] == '-' || N[i] == '*' || N[i] == '/' || N[i] == '+')
        {
            symbols.push_back(N[i]);
            N[i] = '+';
        }
    }
    numbers = split(N, "+");

    int sum = 0;
    
    for (int i = 0; i < symbols.size(); i++)
    {
        if (symbols[i] == '/' || symbols[i] == '*')
        {
            if (!(isValid(numbers[i]) && isValid(numbers[i + 1])))
            {
                cout << "Invalid input";
                exit(0);
            }
            else
            {
                int res = calc(stoi(numbers[i]), stoi(numbers[i + 1]), symbols[i]);
                numbers.erase(numbers.begin() + i + 1);
                symbols.erase(symbols.begin() + i);
                numbers[i] = to_string(res);
                i--;
            }
        }
    }
    cout << endl;
    if (numbers.size() != 1)
    {
        for (int i = 0; i < symbols.size(); i++)
        {
            if (i == 0)
            {
                if (isValid(numbers[i]) && isValid(numbers[i + 1]))
                    sum = calc(stoi(numbers[i]), stoi(numbers[i + 1]), symbols[i]);
                else
                {
                    cout << "Invalid input";
                    exit(0);
                }
            }
            else
            {
                if (isValid(to_string(sum)) && isValid(numbers[i + 1]))
                    sum = calc(sum, stoi(numbers[i + 1]), symbols[i]);
                else
                {
                    cout << "Invalid input";
                    exit(0);
                }
            }
        }
    }
    else
    {
        if (isValid(numbers[0]))
            sum = stoi(numbers[0]);
        else
        {
            cout << "Invalid input";
            exit(0);
        }
    }
    return sum;
}

int main()
{
    return 0;
}