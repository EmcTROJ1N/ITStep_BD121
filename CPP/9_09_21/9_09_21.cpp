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
        string token = str.substr(prev, pos-prev);
        if (!token.empty()) tokens.push_back(token);
        prev = pos + delim.length();
    }
    while (pos < str.length() && prev < str.length());
    return tokens;
}


int main()
{
    string str;
    getline(cin, str);
    vector<string> vec = split(str, " ");

    for (int i = 0; i < vec.size(); i++)
    {
        for (int j = 0; j < vec[i].size(); j++)
        {
            if (vec[i][j] == '.' || vec[i][j] == ',')
                vec[i].erase(j, 1);
        }
    }
    int sum = 0;
    for (int i = 0; i < vec.size(); i++)
    {
        try
        {
            sum += stoi(vec[i]);
        }
        catch(const std::exception& e)
        {
            continue;
        }
           
    }
    cout << sum;


    return 0;
}