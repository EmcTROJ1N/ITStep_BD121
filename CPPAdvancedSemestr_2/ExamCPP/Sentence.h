#pragma once
#include "Word.h"
#include <vector>

class Sentence
{
    vector<Word> Sntns;
    vector<char> Symbols;
public:
    Sentence();
    Sentence(string sentence);
    Sentence(Sentence* source);
    ~Sentence() {}

    void Print();
    int Length();
    void Set(string sentence);
    string Get();
    void Add(Word word);
    bool Remove(int idx);

    Sentence& operator=(Sentence source);
    Sentence& operator+=(Word word);
    Word& operator[](int idx);

    friend ostream& operator<<(ostream& os, Sentence& sentence);
};