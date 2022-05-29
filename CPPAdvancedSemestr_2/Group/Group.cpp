#include <iostream>
#include "Group.h"

using namespace std;

Group::Group()
{
    CurrentLength = 0;
    MaxLength = 100;
    peoples = new Person*[MaxLength];
}

Group::Group(const Group& source)
{
    CurrentLength = source.CurrentLength;
    MaxLength = source.MaxLength;
    peoples = new Person*[MaxLength];
    for (int i = 0; i < CurrentLength; i++)
        peoples[i] = source.peoples[i];
}

Group::Group(const unsigned maxLength)
{
    CurrentLength = 0;
    MaxLength = maxLength;
    peoples = new Person*[MaxLength];
}

Group::~Group() { delete[] peoples; }

void Group::Print()
{
    for (int i = 0; i < CurrentLength; i++)
    {
        peoples[i]->Print();
        cout << endl;
    }
}

void Group::Add(Person* person)
{
    if (CurrentLength < MaxLength)
        peoples[CurrentLength++] = person;
}

bool Group::Save()
{
    return true;
}

bool Group::Load()
{
    return true;
}

bool Group::operator==(const Group& source)
{
    if (CurrentLength != source.CurrentLength)
        return false;

    for (int i = 0; i < CurrentLength; i++)
    {
        if (*peoples[i] != *source.peoples[i])
            return false;
    }
}

Group Group::operator=(const Group& source)
{
    CurrentLength = source.CurrentLength;
    MaxLength = source.MaxLength;

    delete[] peoples;
    peoples = new Person*[MaxLength];
    for (int i = 0; i < CurrentLength; i++)
        peoples[i] = source.peoples[i];
    
    return *this;
}

Group Group::operator+(Person* person)
{
    Group res = *this;
    res.Add(person);
    
    return res;
}

Group Group::operator+=(Person* person)
{
    this->Add(person);
    return *this;
}