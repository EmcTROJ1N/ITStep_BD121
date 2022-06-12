#pragma once
#include "Person.h"
#include <iostream>

class Student : public Person
{
private:
    unsigned Stipend;
    unsigned RoomNumb;
    unsigned NumBilet;
public:
    Student();
    Student(const char* fname, const char* lname,
    const unsigned age, const char* addr,
    const unsigned stip, const unsigned roomNumb, 
    const unsigned numbBiletr);
    ~Student() { std::cout << "Destr" << std::endl; }
 
    void PrintInfo();
    unsigned getStipend() { return Stipend; }
    unsigned getRoomNumb() { return RoomNumb; }
    unsigned getNumbBilet() { return NumBilet; }
    void setStidend(const unsigned stipend);
    void setRoomNumb(const unsigned roomNumb);
    void setNumBilet(const unsigned numBilet);
};