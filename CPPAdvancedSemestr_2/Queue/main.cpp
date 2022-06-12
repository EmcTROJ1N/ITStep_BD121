#include <iostream>
#include "Queue.h"

using namespace std;

int main()
{
    Queue queue;

    for (int i = 1; i <= 10; i++)
        queue.Enqueue(i);
    queue.Display();
    queue.Dequeue();
    queue.Display();

    return 0;
}