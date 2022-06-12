#pragma once

class Person
{
private:
    char* FirstName;
    char* LastName;
    unsigned Age;
    char* Address;
public:
    Person();
    Person(const char* name, const char* lname, unsigned age, const char* add);
    Person(const Person& source);
    ~Person();

    void Print();
    char* getFirstName();
    char* getLastName();
    unsigned getAge();
    char* getAddress();
    void setFirstName(const char* name);
    void setLastName(const char* lname);
    void setAge(const int age);
    void setAddress(const char* addr);

    bool operator==(const Person& source);
    bool operator!=(const Person& source);
};