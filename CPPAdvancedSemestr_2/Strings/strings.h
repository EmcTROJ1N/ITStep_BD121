#pragma once

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
    bool Remove(unsigned index);
    void Sort();
    void RemoveDublicates();

    Strings operator+(char* str);
    Strings operator+(const Strings& source);
    Strings operator=(const Strings& source);
    
    Strings& operator-=(char* str);
    Strings& operator-=(const Strings& source);
    bool operator==(const Strings& source);
    bool AddStrings(const int count, ...);


    bool Save();
    bool Load();

};