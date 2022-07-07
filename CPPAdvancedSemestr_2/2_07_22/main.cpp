#include <iostream>
#include <vector>
#include <list>
#include <string>
#include <fstream>
#include <math.h>
#include <queue>
#include <deque>
#include <algorithm>
#include <time.h>

using namespace std;

int convert(string sNum)
{
    int num = 0;
    int k = sNum.length() - 1;

    for (auto ch : sNum)
    {
        num += ((int)ch - 48) * pow(10, k);
        k--;
    }

    return num;
}

template<typename Type>
Type getRandom(vector<Type>& vec) 
{ 
    int min = 0;
    int max = vec.size();
    return vec[min + (rand() % (max - min))];
}

void task1()
{
    string filename1;
    string filename2;
    cin >> filename1 >> filename2;
    list<string> lst1;
    list<string> lst2;
    ifstream is1(filename1);
    ifstream is2(filename2);

    for (string str; !is1.eof(); is1 >> str)
        lst1.push_back(str);
    for (string str; !is2.eof(); is2 >> str)
        lst2.push_back(str);
    is1.close();
    is2.close();
    
    ofstream os(filename1);
    for (auto str : lst1)
    {
        bool flag = true;

        for (auto strj : lst2)
        {
            if (str == strj)
            {
                flag = false;
                break;
            }
        }
        if (flag)
            os << str << endl;
    }
    os.close();
}   

void task2()
{
    string filename;
    cin >> filename;
    ifstream is(filename);
    ofstream os("res.txt");
    list<int> lst;

    int num;
    is >> num;
    for (; !is.eof(); is >> num)
    {
        bool isPrime = true;
        for (int i = 2; i < num; i++)
        {
            if (num % i == 0)
            {
                isPrime = false;
                break;
            }
        }
        if (isPrime)
            lst.push_back(num);
    }
    is.close();
    lst.sort();
    for (auto i: lst)
        os << i << endl;
    os.close();
}

void task3()
{
    string filename;
    cin >> filename;
    ifstream is(filename);
    ofstream os("res.txt");
    vector<string> vec;

    string str;
    is >> str;
    for(; !is.eof(); is >> str)
        vec.push_back(str);
    is.close();

    vector<int> idxs;
    for (int i = 0; i < vec.size(); i++)
        idxs.push_back(i);

    vector<string> reVec;
    reVec.resize(vec.size());
    for (int i = 0; i < vec.size(); i++)
    {
        int ran = getRandom(idxs);
        reVec[ran] = vec[i];
        idxs.erase(remove(idxs.begin(), idxs.end(), ran), idxs.end());
    }
    for (auto i : reVec)
        os << i << endl; 
    os.close();
}

int main()
{
    srand(time(0));
    task3();

    return 0;
}