#include <iostream>
#include <string>
#include <vector>
#include <stdlib.h>
#include <cstring>

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

string calcNumbs(string N)
{
    vector<string> numbers;
    vector<char> symbols;
    
    if (N.size() == 1)
    {
        if (N[0] == '+' || N[0] == '-' || N[0] == '/' || N[0] == '*')
        {
            return N;
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
                return N;
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
                    return N;
                }
            }
            else
            {
                if (isValid(to_string(sum)) && isValid(numbers[i + 1]))
                    sum = calc(sum, stoi(numbers[i + 1]), symbols[i]);
                else
                {
                    return N;
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
            return N;
        }
    }
    return to_string(sum);
}

string convertString(string sentence)
{
    vector<char> symbols;
    string res;
    for (int i = 0; i < sentence.size(); i++)
    {
        if (sentence[i] == ',' || sentence[i] == '.' || sentence[i] == '-')
        {
            symbols.push_back(sentence[i]);
            sentence.erase(i, 1);
        }
        else
            symbols.push_back(NULL);
    }
    vector<string> ss = split(sentence, " ");

    for (int i = 0; i < ss.size(); i++)
    {
        res += calcNumbs(ss[i]);
        res.push_back(' ');
    }
    for (int i = 0; i < res.size(); i++)
    {
        if (symbols[i] != NULL)
        {
            res.insert(i, 1, symbols[i]);
        }
    }
   
    return res;
}

int main()
{

    string sentence;
    FILE* inputData;
    FILE* outputData;
    char filenameInput[99];
    char filenameOutput[99];
    cin >> filenameInput;
    cin >> filenameOutput;
    inputData = fopen(filenameInput, "r");
    if (inputData == NULL)
    {
        cout << "Error";
        return 0;
    }
    vector<string> inputDataVector;
    while (!feof(inputData))
    {
        char sent[99];
        strcpy(sent, "");
        fgets(sent, 999, inputData);
        inputDataVector.push_back(sent);
    }
    fclose(inputData);
    outputData = fopen(filenameOutput, "w");
    for (int i = 0; i < inputDataVector.size(); i++)
    {
        inputDataVector[i] = convertString(inputDataVector[i]);
    }
    for (int i = 0; i < inputDataVector.size(); i++)
    {
        char buf[255];
        strcpy(buf,inputDataVector[i].c_str());
        fputs(buf, outputData);
    }
    fclose(outputData);
}