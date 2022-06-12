#include <iostream>
#include "Staff.h"
#include "Person.h"

using namespace std;

Staff::Staff(size_t maxSize)
{
    MaxSize = maxSize;
    CurrentSize = 0;
    _Staff = new Person*[MaxSize];
}

Staff::~Staff()
{
    for (int i = 0; i < CurrentSize; i++)
        delete _Staff[i];
    delete[] _Staff;
}

void Staff::Add(Person* person)
{
    if (CurrentSize < MaxSize)
        _Staff[CurrentSize++] = person;
}

void Staff::Print()
{
    for (int i = 0; i < CurrentSize; i++)
    {
        _Staff[i]->Print();
        cout << endl;
    }
}

double Staff::GetSalarieSum()
{
    double sum;
    for (int i = 0; i < CurrentSize; i++)
    {
        sum += _Staff[i]->GetSalarieSum();
        cout << endl;
    }
    return sum;
}