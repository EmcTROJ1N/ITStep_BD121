#include <iostream>
#include "Stack.h"

using namespace std;

Stack::Stack()
{
    CurrentSize = 0;
    MaxSize = 10;
    Arr = new int[MaxSize];
}

Stack::Stack(unsigned maxSize)
{
    CurrentSize = 0;
    MaxSize = maxSize;
    Arr = new int[MaxSize];
}

int Stack::Pop() { return Arr[--CurrentSize]; }
int Stack::Show() { return Arr[CurrentSize - 1]; }
void Stack::Push(int num) { Arr[CurrentSize++] = num; }
int Stack::getCurrentSize() { return CurrentSize; }
int Stack::getMaxSize() { return MaxSize; }
void Stack::Clear() { CurrentSize = 0; }

void Stack::Display()
{
    for (int i = 0; i < CurrentSize; i++)
        cout << Arr[i] << " ";
    cout << endl;
}