#include <iostream>
#include "Person.h"
#include <cstring>

using namespace std;

Person::Person()
{
    FirstName = new char[40];
    LastName = new char[40];
    Address = new char[40];
    strcpy(FirstName, "None");
    strcpy(LastName, "None");
    strcpy(Address, "None");
    Age = 0;
}

Person::Person(const char* name, const char* lname, unsigned age, const char* add)
{
    FirstName = new char[40];
    LastName = new char[40];
    Address = new char[40];
    strcpy(FirstName, name);
    strcpy(LastName, lname);
    strcpy(Address, add);

    Age = age;
}

Person::Person(const Person& source)
{

    FirstName = new char[strlen(source.FirstName) + 1];
    LastName = new char[strlen(source.LastName) + 1];
    Address = new char[strlen(source.Address) + 1];
    strcpy(FirstName, source.FirstName);
    strcpy(LastName, source.LastName);
    strcpy(Address, source.Address);
    Age = source.Age;
}

Person::~Person()
{
    delete[] FirstName;
    delete[] LastName;
    delete[] Address;
}

void Person::Print()
{
    cout << "Person`s name: " <<  FirstName << endl
         << "Person`s surname: " << LastName << endl
         << "Person`s age: " << Age << endl
         << "Person`s address: " << Address << endl;
}

char* Person::getFirstName() { return FirstName; }
char* Person::getLastName() { return LastName; }
unsigned Person::getAge() { return Age; }
char* Person::getAddress() { return Address; }

void Person::setFirstName(const char* name)
{
    if (strlen(name) < 40)
        strcpy(FirstName, name);
}

void Person::setLastName(const char* lname)
{
    if (strlen(lname) < 40)
        strcpy(LastName, lname);
}

void Person::setAge(const int age)
{
    if (age < 115)
        Age = age;
}

void Person::setAddress(const char* addr)
{
    if (strlen(addr) < 40)
        strcpy(Address, addr);
}

bool Person::operator==(const Person& source)
{
    if (Age != source.Age) return false;
    if (strcmp(FirstName, source.FirstName) != 0) return false;
    if (strcmp(LastName, source.LastName) != 0) return false;

    return true;
}

bool Person::operator!=(const Person& source) { return !(*this == source); }