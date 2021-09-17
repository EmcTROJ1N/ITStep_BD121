#include <iostream>
#include <string>
#include <vector>

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
    switch (symb)
    {
        case '+': res = a + b; break;
        case '-': res = a - b; break;
        case '*': res = a * b; break;
        case '/': res = a / b; break;
    }
    return res;
}

void task_1()
{
    string N;
    vector<string> numbers;
    vector<char> symbols;
    getline(cin, N);

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
    vector<int> deletedIndexes;
    
    for (int i = 0; i < symbols.size(); i++)
    {
        if (symbols[i] == '/' || symbols[i] == '*')
        {
            int res = calc(stoi(numbers[i]), stoi(numbers[i + 1]), symbols[i]);
            numbers.erase(numbers.begin() + i + 1);
            deletedIndexes.push_back(i);
            symbols.erase(symbols.begin() + i);
            numbers[i] = to_string(res);
            i--;
        }
    }
    cout << endl;
    
    if (numbers.size() != 1)
    {
        for (int i = 0; i < symbols.size(); i++)
        {
            if (i == 0)
                sum = calc(stoi(numbers[i]), stoi(numbers[i + 1]), symbols[i]);
            else
                sum = calc(sum, stoi(numbers[i + 1]), symbols[i]);
        }
    }
    else
        sum = stoi(numbers[0]);
    cout << sum;
}

int main()
{
    task_1();
    return 0;
}