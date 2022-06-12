#pragma once

class Stack
{
private:
    int* Arr;
    unsigned CurrentSize;
    unsigned MaxSize;
public:
    Stack();
    Stack(unsigned maxSize);
    ~Stack() { delete[] Arr; }

    void Push(int num);
    int Pop();
    int Show();
    void Clear();
    void Display();

    int getCurrentSize();
    int getMaxSize();
};