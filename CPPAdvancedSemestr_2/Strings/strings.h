#pragma once
#include <fstream>
#include <iostream>

using namespace std;

class Strings
{
private:
    char** Strs;
    unsigned MaxRows;
    unsigned CurrentRows;
public:
    Strings();
    Strings(const unsigned maxRows);
    Strings(const Strings& source);
    ~Strings();

    void Print();
    bool Add(char* str);
    bool AddStrings(const int count, ...);
    bool Remove(unsigned index);
    void Sort();
    void RemoveDublicates();

    bool Save();
    bool Load();
    
    Strings operator+(char* str);
    Strings operator+(const Strings& source);
    Strings operator=(const Strings& source);
    Strings& operator-=(char* str);
    Strings& operator-=(const Strings& source);
    bool operator==(const Strings& source);

    friend ostream& operator<<(ostream& os, Strings &strs);
    friend istream& operator>>(istream& is, Strings &strs);

    friend ofstream& operator<<(ofstream& os, Strings &strs);
    friend ifstream& operator>>(ifstream& is, Strings &strs);

};