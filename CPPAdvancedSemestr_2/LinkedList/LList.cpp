#include "LList.h"
#include <iostream>
#include <cstring>

using namespace std;

Element::Element(const char* str)
{
    Str = new char[strlen(str) + 1];
    strcpy(Str, str);
}

Element::~Element() { delete[] Str; }
LinkedList::LinkedList() { First = Last = 0; }

LinkedList::LinkedList(const LinkedList& source)
{
    Element* currentSource = source.First;

    while (currentSource != nullptr)
    {
        this->Add(currentSource->Str);
        currentSource = currentSource->next;
    }
}

LinkedList::~LinkedList()
{
    Element* current = First;

    while (current != nullptr)
    {
        Element* tmp = current;
        current = current->next;
        delete tmp;
    }
}

void LinkedList::Add(const char* str)
{
    Element* elem = new Element(str);

    elem->next = nullptr;

    if (First == nullptr)
        Last = First = elem;
    else
    {
        Last->next = elem;
        Last = elem;
    }
}

void LinkedList::Print()
{
    Element* current = First;

    while (current != nullptr)
    {
        cout << current->Str << endl;
        current = current->next;
    }
} 

int LinkedList::GetSize()
{
    Element* current = First;
    int k = 0;

    while (current != nullptr)
    {
        k++;
        current = current->next;
    }
    return k;
}

bool LinkedList::Contains(char* str)
{
    Element* current = First;
    while (current != nullptr)
    {
        if (strcmp(current->Str, str) == 0)
            return true;
        current = current->next;
    }
    return false;
}

int LinkedList::GetCount(char* str)
{
    Element* current = First;
    int k = 0;
    while (current != nullptr)
    {
        if (strcmp(current->Str, str) == 0)
            k++;
        current = current->next;
    }
    return k;
}

void LinkedList::Insert(size_t pos, char *str)
{
    Element* current = First;
    Element* insertedEl = new Element(str);

    int k = 0;
    while (current != nullptr)
    {
        if (k + 1 == pos)
        {
            insertedEl->next = current->next;
            current->next = insertedEl;
            break;
        }
        current = current->next;
        k++;
    }
}

LinkedList LinkedList::operator=(const LinkedList& source)
{
    Element* current = First;

    while (current != nullptr)
    {
        Element* tmp = current;
        current = current->next;
        delete tmp;
    }

    Element* currentSource = source.First;
    First = Last = nullptr;

    while (currentSource != nullptr)
    {
        this->Add(currentSource->Str);
        currentSource = currentSource->next;
    }

    return *this;
}

bool LinkedList::operator==(LinkedList &source)
{
    Element* curr = First;
    Element* currSource = source.First;
    
    if (this->GetSize() != source.GetSize())
        return false;
    
    while (curr != nullptr)
    {
        if (strcmp(curr->Str, currSource->Str) != 0)
            return false;
        currSource = currSource->next;
        curr = curr->next;
    }

    return true;
}

bool LinkedList::Remove(unsigned idx)
{
    if (idx == 0)
    {
        Element* tmp = First;
        First = First->next;
        delete tmp;
    }
    if (idx == GetSize() - 1)
    {

    }

    Element* current = First;
    int k = 0;
    while (current != nullptr)
    {
        if (k + 1 == idx)
        {
            Element* tmp = current->next;
            current->next = current->next->next;
            delete tmp;
            break;
        }
        current = current->next;
        k++;
    }
    Last = current;
}

bool LinkedList::RemoveAll(char* str)
{
    if (GetSize() == 0)
        return false;
    Element* current = First;
    int k = 0;

    if (strcmp(current->Str, str) == 0)
        First = First->next;

    while (current->next != nullptr)
    {
        if (strcmp(current->next->Str, str) == 0)
        {
            Element* tmp = current->next;
            current->next = current->next->next;
            delete tmp;
        }
        current = current->next;
        k++;
    }
    Last = current;
    return true;
}


ostream& operator<< (ostream& os, LinkedList &lst)
{
    Element* current = lst.First;

    while (current != nullptr)
    {
        os << current->Str << endl;
        current = current->next;
    }
}

LinkedList operator+=(LinkedList& lsd, char* str)
{
    lsd.Add(str);
}