#pragma once

class Queue
{
private:
    int* Arr;
    unsigned CurrentSize;
    unsigned MaxSize;
public:
    Queue();
    Queue(unsigned maxSize);
    ~Queue() { delete[] Arr; }

    void Enqueue(int num);
    int Dequeue();
    int Show();
    void Clear();
    void Display();

    int getCurrentSize();
    int getMaxSize();
};