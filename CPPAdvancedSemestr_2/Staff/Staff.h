#pragma once
#include "Person.h"


class Staff
{
    Person** _Staff;
    size_t MaxSize;
    size_t CurrentSize;
public:
    Staff(size_t maxSize);
    ~Staff();
    void Add(Person* person);
    void Print();
    double GetSalarieSum();
};