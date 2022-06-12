#pragma once
#include "Person.h"
#include <iostream>

using namespace std;

class Freelancer: public Person
{
    unsigned HourCost;
    unsigned NumberOfHours;
public:
    Freelancer
    (
        const char* fname, const char* lname, unsigned age,
        const char* addr, unsigned hourCost, unsigned numberOfHours
    ) : Person(fname, lname, age, addr)
    {
        HourCost = hourCost;
        NumberOfHours = numberOfHours;
    }
    ~Freelancer() {}
    double GetSalarieSum() { return HourCost * NumberOfHours; }
    
    void Print()
    {
        cout << "Freelancer`s name: " <<  FirstName << endl
            << "Freelancer`s surname: " << LastName << endl
            << "Freelancer`s age: " << Age << endl
            << "Freelancer`s address: " << Address << endl
            << "Freelancer`s hour cost: " << HourCost << endl
            << "Freelancer`s number of hours: " << NumberOfHours << endl;
    }
};