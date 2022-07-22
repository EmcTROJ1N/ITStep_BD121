#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <set>
#include "Word.h"
#include "Sentence.h"
#include "Text.h"

using namespace std;

Text::Text()
{
    _Text.push_back((string)"Hello world");
    _Text.push_back((string)"C++ better than C");
    Symbols = { '.', '!' };
}

Text::Text(string text)
{
    string str("");
    for (int i = 0; i < text.size(); i++)
    {
        char& ch = text[i];
        if (ch == '.' || ch == '!' || ch == '?') 
        {
            Sentence ss(str);
            _Text.push_back(ss);
            Symbols.push_back(ch);
            str.clear();
        }
        else
            str += text[i];
    }
}

Text::Text(Text* source)
{   
    _Text = source->_Text;
    Symbols = source->Symbols;
}

int Text::Length() { return _Text.size(); }

string Text::Get()
{
    string str("");
    int i;
    for (i = 0; i < _Text.size() - 1; i++)
    {
        str += _Text[i].Get();
        str += Symbols[i];
        str += " ";
    }
    str += _Text[i].Get();
    str += Symbols[i];

    return str;
}

void Text::Add(Sentence sentence)
{
    _Text.push_back(sentence);
    Symbols.push_back(NULL); 
}

bool Text::Remove(int idx)
{
    _Text.erase(_Text.begin() + idx);
    Symbols.erase(Symbols.begin() + idx);
}

bool Text::Save()
{
    ofstream os("data.log");
    for (int i = 0; i < _Text.size(); i++)
        os << _Text[i] << Symbols[i] << endl;
    os.close();
    return true;
}

bool Text::Load()
{
    _Text.clear();
    Symbols.clear();
    ifstream is("data.log");

    string text;
    while(getline(is, text))
    {
        string str("");
        for (int i = 0; i < text.size(); i++)
        {
            char& ch = text[i];
            if (ch == '.' || ch == '!' || ch == '?') 
            {
                Sentence ss(str);
                _Text.push_back(ss);
                Symbols.push_back(ch);
                str.clear();
            }
            else
                str += text[i];
        }
    }

    is.close();
    return true;
}

bool Text::SaveWords()
{
    set<string> words;
    ofstream os("saveWords.txt");

    for (int i = 0; i < _Text.size(); i++)
    {
        for (int j = 0; j < _Text[i].Length(); j++)
            words.insert(_Text[i][j].GetStr());
    }
    for (auto word : words)
        os << word << endl;
    os.close();
}

Text Text::operator=(Text& text)
{
    _Text = text._Text;
    Symbols = text.Symbols;
    return *this;
}

Text& Text::operator+=(Sentence sentence) { Add(sentence); return *this; }
Sentence& Text::operator[](int idx) { return _Text[idx]; }
ostream& operator<<(ostream& os, Text& text)
{
    int i;
    for (i = 0; i < text._Text.size() - 1; i++)
        os << text._Text[i] << text.Symbols[i] << " ";
    os << text._Text[i] << text.Symbols[i];
    return os;
}

ofstream& operator<<(ofstream& os, Text& text)
{
    int i;
    for (i = 0; i < text._Text.size() - 1; i++)
        os << text._Text[i] << text.Symbols[i] << " ";
    os << text._Text[i] << text.Symbols[i];   
    return os;
}

ifstream& operator>>(ifstream& os, Text& text)
{
    return os;
}