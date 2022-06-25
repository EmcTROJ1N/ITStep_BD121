#pragma once
#include <iostream>
#include <fstream>

using namespace std;

struct Element
{
        char* Str;
        Element* next = 0;
public:
        Element(const char* str);
        ~Element();
};

class LinkedList
{
        Element* First;
        Element* Last;
public:
        LinkedList();
        ~LinkedList();
        LinkedList(const LinkedList& source);

        void Add(const char* str);
        void Print();

        int GetSize();
        bool Contains(char* str);
        int GetCount(char* str);
        void Insert(size_t pos, char* str);

        LinkedList operator= (const LinkedList &source);
        bool operator== (LinkedList &lsd);
        friend LinkedList operator+=(LinkedList& lsd, char* str);
        bool Remove(unsigned idx);
        bool RemoveAll(char* str);

        friend ostream& operator<< (ostream& os, LinkedList &lst);
};