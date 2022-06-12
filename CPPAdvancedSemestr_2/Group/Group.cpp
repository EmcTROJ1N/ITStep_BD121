#include <iostream>
#include "Group.h"
#include <fstream>
#include <cstring>

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
        peoples[i] = new Person(source.peoples[i]->getFirstName(), source.peoples[i]->getLastName(), source.peoples[i]->getAge());
}

Group::Group(const unsigned maxLength)
{
    CurrentLength = 0;
    MaxLength = maxLength;
    peoples = new Person*[MaxLength];
}

Group::~Group()
{
    for (int i = 0; i < CurrentLength; i++)
        delete peoples[i];
    delete[] peoples; 
}

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
    ofstream out;
    out.open("output.bin");
    if (out.is_open())
    {
        out << CurrentLength << " " << MaxLength << endl;
        for (int i = 0; i < CurrentLength; i++)
        {
            out << peoples[i]->getFirstName() << " " << peoples[i]-> getLastName()
            << " " << peoples[i]->getAge() << endl;
        }
    }
        out.close();
    return true;
}

bool Group::Load()
{
    for (int i = 0; i < CurrentLength; i++)
        delete peoples[i];
    delete[] peoples;
    
    ifstream in;
    in.open("output.bin");
    
    if (in.is_open())
    {
        in >> CurrentLength >> MaxLength;
        
        peoples = new Person*[MaxLength];
        for (int i = 0; i < CurrentLength; i++)
        {
            char* fname = new char[40];
            char* lname = new char[40];
            int age;
            in >> fname >> lname >> age;
            
            peoples[i] = new Person(fname, lname, age);

            delete[] fname;
            delete[] lname;
        }
        in.close();
    }
    return true;
}

void Group::Remove(unsigned index)
{
    Person* temp;
    temp = peoples[index];
    peoples[index] = peoples[CurrentLength - 1];
    peoples[CurrentLength - 1] = temp;

    delete peoples[CurrentLength - 1]; 
    CurrentLength--;
}

void Group::Remove(Person* person)
{
    for (int i = 0; i < CurrentLength; i++)
    {
        if (*person == *peoples[i])
            Remove(i);
    }
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
    return true;
}

Group& Group::operator=(const Group& source)
{
    for (int i = 0; i < CurrentLength; i++)
        delete peoples[i];
    delete[] peoples;

    CurrentLength = source.CurrentLength;
    MaxLength = source.MaxLength;

    peoples = new Person*[MaxLength];
    for (int i = 0; i < CurrentLength; i++)
        peoples[i] = new Person(source.peoples[i]->getFirstName(), source.peoples[i]->getLastName(), source.peoples[i]->getAge());
    
    return *this;
}

Group Group::operator+(Person* person)
{
    Group res = *this;
    res.Add(person);
    
    return res;
}

Group& Group::operator+=(Person* person)
{
    Add(person);
    return *this;
}

Group& Group::operator-=(Person* person)
{
    Remove(person);
    return *this;
}

Group Group::operator-(Person* person)
{
    Group temp = *this;
    temp.Remove(person);
    return temp;
}

Group Group::operator+(const Group& source)
{
    Group res(MaxLength + source.MaxLength);
    int i = 0;
    res.CurrentLength = CurrentLength + source.CurrentLength;
    for (; i < CurrentLength; i++)
        res.peoples[i] = new Person(peoples[i]->getFirstName(), peoples[i]->getLastName(), peoples[i]->getAge());
    for (int j = 0; j < source.CurrentLength; j++, i++)
        res.peoples[i] = new Person(source.peoples[j]->getFirstName(), source.peoples[j]->getLastName(), source.peoples[j]->getAge());
    return res;
}