#include <iostream>

using namespace std;

typedef void (*func_type)();

void Anna()
{
    cout << "Anna";
}

void Andrey()
{
    cout << "Andrey";
}

void Alex()
{
    cout << "Alex";
}

void Vova()
{
    cout << "Vova";
}

int main()
{
    func_type funcs[4] = {Anna, Andrey, Alex, Vova};
    for (int i = 0; i < 4; i++)
    {
        funcs[i]();
    }
    return 0;
}