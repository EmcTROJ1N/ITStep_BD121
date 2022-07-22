#pragma once
#include <vector>
#include "Word.h"
#include "Sentence.h"

class Text
{
    vector<Sentence> _Text;
    vector<char> Symbols;
public:
    Text();
    Text(string text);
    Text(Text* source);
    ~Text() {}

    int Length();
    void Set(string str);

    string Get();
    void Add(Sentence sentence);
    bool Remove(int idx);
    bool Save();
    bool Load();
    bool SaveWords();

    Text operator=(Text& text);
    Text& operator+=(Sentence sentence);
    Sentence& operator[](int idx);
    
    friend ostream& operator<<(ostream& os, Text& text);
    friend ofstream& operator<<(ofstream& os, Text& text);
    friend ifstream& operator>>(ifstream& os, Text& text);
};