#pragma once
#include "Person.h"
#include <iostream>

using namespace std;

class Worker: public Person
{
    unsigned Salary;
    unsigned LockerNumber;
public:
    Worker
    (
        const char* fname, const char* lname, unsigned age,
        const char* addr, unsigned sal, unsigned lockNum
    ) : Person(fname, lname, age, addr)
    {
        Salary = sal;
        LockerNumber = lockNum;
    }
    ~Worker() {}
    double GetSalarieSum() { return Salary; }
    
    void Print()
    {
        cout << "Worker`s name: " <<  FirstName << endl
         << "Worker`s surname: " << LastName << endl
         << "Worker`s age: " << Age << endl
         << "Worker`s address: " << Address << endl
         << "Worker`s salary: " << Salary << endl
         << "Worker`s lock numb: " << LockerNumber << endl;
    }

};