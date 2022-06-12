#include <iostream>
#include "strings.h"
#include <cstring>
#include <fstream>
#include <stdarg.h>

using namespace std;

Strings::Strings()
{
    MaxRows = 10;
    CurrentRows = 0;
    Strs = new char*[MaxRows];
}

Strings::Strings(const unsigned maxRows)
{
    MaxRows = maxRows;
    CurrentRows = 0;
    Strs = new char*[MaxRows];
}

Strings::Strings(const Strings& source)
{
    MaxRows = source.MaxRows;
    CurrentRows = source.CurrentRows;
    Strs = new char*[MaxRows];
    
    for (int i = 0; i < CurrentRows; i++)
    {
        Strs[i] = new char[strlen(source.Strs[i]) + 1];
        strcpy(Strs[i], source.Strs[i]);
    }
}

Strings::~Strings()
{
    for (int i = 0; i < CurrentRows; i++)
        delete Strs[i];
    delete[] Strs;
}

void Strings::Print()
{
    for (int i = 0; i < CurrentRows; i++)
        cout << Strs[i] << endl;
    cout << endl;
}

bool Strings::Add(char* str)
{
    if (CurrentRows < MaxRows)
    {
        Strs[CurrentRows] = new char[strlen(str) + 1];
        strcpy(Strs[CurrentRows], str);
        CurrentRows++;
        return true;
    }
    else
        return false;
}

bool Strings::Remove(unsigned index)
{
    char* temp;
    temp = Strs[index];
    Strs[index] = Strs[CurrentRows - 1];
    Strs[CurrentRows - 1] = temp;

    delete Strs[CurrentRows - 1]; 
    CurrentRows--;
    return true;
}

void Strings::Sort()
{
    for (int i = 0; i < CurrentRows - 1; i++)
    {
        for (int j = 0; j < CurrentRows - i - 1; j++)
        {
            if (strcmp(Strs[j], Strs[j + 1]) > 0)
            {
                char* buf = Strs[j];
                Strs[j] = Strs[j + 1];
                Strs[j + 1] = buf;
            }
        }
    }
}

void Strings::RemoveDublicates()
{
    for (int i = 0; i < CurrentRows - 1; i++)
    {
        for (int j = i + 1; j < CurrentRows; j++)
        {
            if (strcmp(Strs[i], Strs[j]) == 0)
            {
                Remove(j);
                j--;
            }
        }
    }
}

bool Strings::AddStrings(const int count, ...)
{
    va_list lst;
    va_start(lst, count);
    for (int i = 0; i < count; i++)
        this->Add(va_arg(lst, char*));
    
    va_end(lst);
}

Strings Strings::operator+(char* str)
{
    Strings temp = *this;
    temp.Add(str);
    return temp;
}
Strings Strings::operator+(const Strings& source)
{
    Strings res(MaxRows + source.MaxRows);
    int i = 0;
    res.CurrentRows = CurrentRows + source.CurrentRows;
    for (; i < CurrentRows; i++)
    {
        res.Strs[i] = new char[strlen(Strs[i]) + 1];
        strcpy(res.Strs[i], Strs[i]);
    }
    for (int j = 0; j < source.CurrentRows; j++, i++)
    {
        res.Strs[i] = new char[strlen(source.Strs[j]) + 1];
        strcpy(res.Strs[i], source.Strs[j]);
    }
    return res;
}

Strings Strings::operator=(const Strings& source)
{
    for (int i = 0; i < CurrentRows; i++)
        delete Strs[i];
    delete[] Strs;

    MaxRows = source.MaxRows;
    CurrentRows = source.CurrentRows;
    Strs = new char*[MaxRows];
    
    for (int i = 0; i < CurrentRows; i++)
    {
        Strs[i] = new char[strlen(source.Strs[i]) + 1];
        strcpy(Strs[i], source.Strs[i]);
    }
    return *this;
}

Strings& Strings::operator-= (char* str)
{
    for (int i = 0; i < CurrentRows; i++)
    {
        if (strcmp(Strs[i], str) == 0)
        {
            Remove(i);
            i--;
        }
    }
    return *this;
}

Strings& Strings::operator-=(const Strings& source)
{
    for (int i = 0; i < source.CurrentRows; i++)
        *this -= source.Strs[i];
    return *this;
}

bool Strings::operator==(const Strings& source)
{
    if (CurrentRows != source.CurrentRows)
        return false;
    for (int i = 0; i < CurrentRows; i++)
    {
        if (strcmp(Strs[i], source.Strs[i]) != 0)
            return false;
    }

    return true;
}

bool Strings::Save()
{
    ofstream out("data.log");
    if (out.is_open())
    {
        out << CurrentRows << " " << MaxRows << " ";
        for (int i = 0; i < CurrentRows; i++)
            out << Strs[i] << " ";
        return true;
    }
    else return false;
}

bool Strings::Load()
{
    ifstream in("data.log");
    if (in.is_open())
    {
        in >> CurrentRows >> MaxRows;
        Strs = new char*[MaxRows];

        for (int i = 0; i < CurrentRows; i++)
        {
            char* buf = new char[40];
            in >> buf;
            Strs[i] = new char[strlen(buf) + 1];
            strcpy(Strs[i], buf);
            delete buf;
        }
        return true;
    }
    else return false;
}