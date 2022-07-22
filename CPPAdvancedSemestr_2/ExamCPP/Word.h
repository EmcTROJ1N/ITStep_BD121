#pragma once
#include <string>
#include <iostream>

using namespace std;

class Word
{
    string Str;

public:
    Word() { Str = ""; }
    Word(string str);
    Word(char* _str);

    Word(Word *source) { Str = source->Str; }
    ~Word() {}
    Word& operator=(Word source) { Str = source.Str; }
    void Print() { cout << Str; }
    int Length() { return Str.size(); }
    void SetStr(string str) { Str = str; }
    string GetStr() { return Str; }
    void Erase(auto it) { Str.erase(it); }
    void Erase(auto it, auto it2) { Str.erase(it, it2); }
    string::iterator Begin() { return Str.begin(); }
    string::iterator End() { return Str.end(); }
    

    char& operator[](int idx) { return Str[idx]; }
    void operator+=(char ch) { Str += ch; }
    bool operator==(string str) { return Str == str; }
    bool operator==(char* str) { return Str == string(str); }

    friend ostream &operator<<(ostream &os, Word &word);
};