#pragma once
#include "Person.h"

class Group
{
private:
    Person** peoples;
    unsigned MaxLength;
    unsigned CurrentLength;
public:
    Group();
    Group(const Group& source);
    Group(const unsigned maxLength);
    ~Group();
    void Print();
    void Add(Person* person);
    bool Save();
    bool Load();

    bool operator==(const Group& source);
    Group operator=(const Group& source);
    Group operator+ (Person* person);
    Group operator+= (Person* person);
};