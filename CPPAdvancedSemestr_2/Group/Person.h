#pragma once

class Person
{
private:
    char* FirstName;
    char* LastName;
    unsigned Age;
public:
    Person();
    Person(const char* name, const char* lname, unsigned age);
    Person(const Person& source);
    ~Person();

    void Print();
    char* getFirstName();
    char* getLastName();
    unsigned getAge();
    void setFirstName(const char* name);
    void setLastName(const char* lname);
    void setAge(const int age);

    bool operator==(const Person& source);
    bool operator!=(const Person& source);
};