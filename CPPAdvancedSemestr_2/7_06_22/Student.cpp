#include "Person.h"
#include "Student.h"
#include <iostream>

using namespace std;

Student::Student()
{
    Stipend = 1000;
    RoomNumb = 31;
    NumBilet = 23408532;
}

Student::Student(const char* fname, const char* lname,
    const unsigned age, const char* addr,
    const unsigned stip, const unsigned roomNumb, 
    const unsigned numBilet) : Person(fname, lname, age, addr)
{
    Stipend = stip;
    RoomNumb = roomNumb;
    NumBilet = numBilet;
}

void Student::PrintInfo()
{
    Print();

    cout << "Student`s stidend: " << Stipend << endl
         << "Student`s room numb: " << RoomNumb << endl
         << "Student`s number bilet: " << NumBilet << endl;
}