#include "Word.h"

ostream &operator<<(ostream &os, Word &word)
{
    os << word.Str;
    return os;
}

Word::Word(string str)
{
    int idx = str.find(' ');
    if (idx != string::npos)
        str.erase(str.begin() + idx, str.end());
    Str = str;
}
Word::Word(char *_str)
{
    string str(_str);
    int idx = str.find(' ');
    if (idx != string::npos)
        str.erase(str.begin() + idx, str.end());
    Str = str;
}
