#pragma once
#include "Person.h"
#include <iostream>

using namespace std;

class Manager: public Person
{
    unsigned Salary;
    unsigned LockerNumber;
    unsigned Bonuses;
    unsigned RoomNumber;
    unsigned CarNumber;

public:
    Manager
    (
        const char* fname, const char* lname, unsigned age,
        const char* addr, unsigned sal, unsigned lockNum,
        unsigned bonuses, unsigned roomNum, unsigned carNum
    ) : Person(fname, lname, age, addr)
    {
        Salary = sal;
        LockerNumber = lockNum;
        Bonuses = bonuses;
        RoomNumber = roomNum;
        CarNumber = carNum;
    }
    ~Manager() {}
    double GetSalarieSum() { return Salary + Bonuses; }
    
    void Print()
    { 
        cout << "Manager`s name: " <<  FirstName << endl
             << "Manager`s surname: " << LastName << endl
             << "Manager`s age: " << Age << endl
             << "Manager`s address: " << Address << endl
             << "Manager`s salary: " << Salary << endl
             << "Manager`s lock numb: " << LockerNumber << endl
             << "Manager`s bonuses: " << Bonuses << endl
             << "Manager`s room number: " << RoomNumber << endl
             << "Manager`s car number: " << CarNumber << endl;
    }
};