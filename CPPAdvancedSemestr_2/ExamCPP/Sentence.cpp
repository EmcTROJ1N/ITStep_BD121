#include <iostream>
#include <vector>
#include <list>
#include <algorithm>
#include "Word.h"
#include "Sentence.h"

using namespace std;

Sentence::Sentence()
{
    Sntns.push_back("Hello");
    Sntns.push_back("World");
    Symbols = { ',', '.', };
}

Sentence::Sentence(string sentence) { Set(sentence); }
Sentence::Sentence(Sentence* source)
{
    Sntns = source->Sntns;
    Symbols = source->Symbols;
}

void Sentence::Print()
{
    for (int i = 0; i < Sntns.size(); i++)
        cout << Sntns[i] << Symbols[i] << " ";
    cout << endl;
}

int Sentence::Length() { return Sntns.size(); }
void Sentence::Set(string sentence)
{
    Sntns.clear();
    Symbols.clear();

    string str("");
    for (int i = 0; i < sentence.size(); i++)
    {
        if (sentence[i] == '.' || sentence[i] == '!' ||
            sentence[i] == '?')
        {
            str += sentence[i];
            break;
        }

        if (sentence[i] == ' ')
        {
            if (str == "," || str == "-" || str == "." ||
                str == ":" || str == ";")
                Sntns[Sntns.size() - 1] += str[0];
            else
                Sntns.push_back(str);
            str.clear();
        }
        else
            str += sentence[i];
    }

    if (str == "," || str == "-" || str == "." ||
        str == ":" || str == ";")
        Sntns[Sntns.size() - 1] += str[0];
    else
        Sntns.push_back(str);
    str.clear();

    auto it = Sntns.begin();
    for (int i = 0; it != Sntns.end(); i++, it++)
    {
        if (Sntns[i].Length() == 0)
            Sntns.erase(Sntns.begin() + i);


        char lCh = Sntns[i][Sntns[i].Length() - 1];
        char fCh = Sntns[i][0];
        
        if (fCh == ',' || fCh == '-' || fCh == '.' ||
            fCh == ':' || fCh == '!' || fCh == '?' ||
            fCh == ';')
        {
            Sntns[i].Erase(Sntns[i].Begin());
            Symbols[Symbols.size() - 1] = fCh;
        }
        
        if (lCh == ',' || lCh == '-' || lCh == '.' ||
            lCh == ':' || lCh == '!' || lCh == '?' ||
            lCh == ';')
        {
            Sntns[i].Erase(Sntns[i].Begin() + Sntns[i].Length() - 1);
            Symbols.push_back(lCh);
        }
        else
            Symbols.push_back(NULL);
    }
}

string Sentence::Get()
{
    string res;
    for (int i = 0; i < Sntns.size(); i++)
    {
        res += Sntns[i].GetStr();
        res += Symbols[i];
        res += " ";
    }
    return res;
}

void Sentence::Add(Word word)
{
    Sntns.push_back(word);
    Symbols.push_back(NULL);
}

bool Sentence::Remove(int idx)
{
    Sntns.erase(Sntns.begin() + idx);
    Symbols.erase(Symbols.begin() + idx);
}

Sentence& Sentence::operator=(Sentence source)
{
    Sntns.clear();
    Symbols.clear();
    
    for (int i = 0; i < source.Sntns.size(); i++)
        Sntns.push_back(source.Sntns[i]);
    Symbols = source.Symbols;
}

Sentence& Sentence::operator+=(Word word) { Add(word); return *this; }
Word& Sentence::operator[](int idx) { return Sntns[idx]; }

ostream& operator<<(ostream& os, Sentence& sentence)
{
    int i;
    for (i = 0; i < sentence.Sntns.size() - 1; i++)
        os << sentence.Sntns[i] << sentence.Symbols[i] << " ";
    os << sentence.Sntns[i] << sentence.Symbols[i];
    return os;
}