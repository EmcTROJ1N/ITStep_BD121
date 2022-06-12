#include <iostream>
#include "Queue.h"

using namespace std;

Queue::Queue()
{
    CurrentSize = 0;
    MaxSize = 10;
    Arr = new int[MaxSize];
}

Queue::Queue(unsigned maxSize)
{
    CurrentSize = 0;
    MaxSize = maxSize;
    Arr = new int[MaxSize];
}

int Queue::Dequeue()
{
    if (CurrentSize == 0) return 0;
    int num = Arr[0];
        
    for (int i = 0; i < CurrentSize; i++)
        Arr[i] = Arr[i + 1];

    Arr[CurrentSize] = NULL;
    CurrentSize--;
    return num;
}
int Queue::Show() { return Arr[0]; }
void Queue::Enqueue(int num) { Arr[CurrentSize++] = num; }
int Queue::getCurrentSize() { return CurrentSize; }
int Queue::getMaxSize() { return MaxSize; }
void Queue::Clear() { CurrentSize = 0; }

void Queue::Display()
{
    for (int i = 0; i < CurrentSize; i++)
        cout << Arr[i] << " ";
    cout << endl;
}