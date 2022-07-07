#include <iostream>
#include <string>
#include <fstream>
#include <vector>

using namespace std;

void task1()
{
    string tmp;
    string filename;
    cin >> filename;
    ifstream is(filename);
    ofstream os("data.log");

    while (!is.eof())
    {
        is >> tmp;
        if (tmp == "hello")
            continue;
        os << tmp << " ";
    }
    cout << endl;
    system("cat data.log");
    os.close();
    is.close();
}

void task2()
{
    string tmp;
    string filename;
    cin >> filename;
    ifstream is(filename);
    ofstream os("data.log");

    while (!is.eof())
    {
        os << endl;
        is >> tmp;
        os << tmp;
    }
    cout << endl;
    system("cat data.log");
    os.close();
    is.close();
}

void task3()
{
    string filename1;
    string filename2;
    ifstream is1(filename1);
    ifstream is2(filename2);
    ofstream os("data.log");
    vector<string> file1;
    vector<string> file2;

    for (string tmpStr1; !is1.eof(); is1 >> tmpStr1)
        file1.push_back(tmpStr1);
    for (string tmpStr2; !is2.eof(); is2 >> tmpStr2)
        file2.push_back(tmpStr2);
    is1.close();
    is2.close();
    
    for (int i = 0; i < file1.size(); i++)
    {
        for (int j = 0; j < file2.size(); j++)
        {
            if (file1[i] == file2[j])
            {
                os << file1[i];
                break;
            }
        }
    }
}

int main()
{
    task2();
    return 0;
}