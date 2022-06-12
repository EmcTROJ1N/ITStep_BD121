#include <iostream>
#include "Stack.h"

using namespace std;

int main()
{
    Stack stack;

    for (int i = 1; i <= 10; i++)
        stack.Push(i);
    cout << endl;

    stack.Display();
    cout << stack.getCurrentSize() << endl;
    cout << stack.Pop() << endl;
    cout << stack.getCurrentSize() << endl;
    stack.Clear();
    cout << stack.getCurrentSize() << endl;

    
    return 0;
}