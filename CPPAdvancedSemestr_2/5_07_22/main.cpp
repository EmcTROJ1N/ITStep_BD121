#include <iostream>
#include <set>
#include <unordered_set>
#include <fstream>
#include <string>

using namespace std;

void task1()
{
    string filename;
    cin >> filename;
    ifstream is(filename);
    set<string> st;

    string tmpStr;
    is >> tmpStr;
    for (; !is.eof(); is >> tmpStr)
        st.insert(tmpStr);
    is.close();

    ofstream os("res.txt");
    for (set<string>::iterator it = st.begin(); it != st.end(); it++)
        os << *it << endl;
    os.close();
}

void task2()
{
    string filename;
    cin >> filename;
    ifstream is(filename);
    multiset<char> ms;
    char ch;
    is >> ch;
    for (; !is.eof(); is >> ch)
        ms.insert(ch);
    is.close();

    ofstream os("res.txt");
    for (multiset<char>::iterator it = ++ms.begin(); it != ms.end(); it++)
    {
        multiset<char>::iterator lst = --it;
        it++;
        if (*it != *lst)
            os << *it << " - " << ms.count(*it) << endl;

    }
    os.close();
}

void task3()
{
    string filename("code.cpp");
    // cin >> filename;
    ifstream is(filename);
    int strIdx = 1;

    for (string line; getline(is, line);)
    {
        int commPos = line.find("//");
        if (commPos != string::npos)
            line.erase(line.begin() + commPos, line.end());
    
        commPos = line.find("/*");
        if (commPos != string::npos)
        {
            int lPos = line.find("*/");
            if (lPos != string::npos)
                line.erase(line.begin() + commPos, line.begin() + lPos + 2);
            else
            {
                while (lPos == string::npos)
                {
                    getline(is, line);
                    strIdx++;
                    lPos = line.find("*/");
                }
                line.erase(line.begin() + commPos, line.begin() + lPos + 2);
            }

        }
        int quotePos = line.find("\"");
        if (quotePos != string::npos)
        {
            string tmp = line;
            tmp.erase(tmp.begin(), tmp.begin() + quotePos + 1);
            int quotePos2 = tmp.find("\"");
            quotePos2 += quotePos + 2;
            line.erase(line.begin() + quotePos, line.begin() + quotePos2);
        }

        if (line.size() < 2)
        {
            strIdx++;
            continue;
        }

        for (int i = 0; i < line.size() - 2; i++)
        {
            if (line[i] == 'i' && line[i + 1] == 'f')
                cout << "If found in " << strIdx << " line" << endl;
        }

        // cout << strIdx << " " << line << endl;
        strIdx++;
        cout << "";
    }

    is.close();
}


int main()
{
    task3();
    return 0;
}