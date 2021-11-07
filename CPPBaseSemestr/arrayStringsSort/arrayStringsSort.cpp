#include <iostream>
#include <string>
#include <vector>
#include <cstring>
#include <algorithm>

using namespace std;

string charToString(char* word)
{
    string str(word);
    return str;
}

int main()
{
    char filename[666];
    cin >> filename;
    FILE* file = fopen(filename, "r");
    vector<string> arr;
    char str[666];
    while (!feof(file))
    {
        strcpy(str, "");
        fgets(str, 66, file);
        arr.push_back(charToString(str));
    }
    fclose(file);
    sort(arr.begin(), arr.end());

    file = fopen("res.txt", "w");

    for (auto i: arr)
    {
        char buf[666];
        strcpy(buf, i.c_str());
        fputs(buf, file);
    }
    fclose(file);
    return 0;
}