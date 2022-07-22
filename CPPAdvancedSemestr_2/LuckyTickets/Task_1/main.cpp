#include <iostream>
#include <string>
#include <list>
#include <fstream>
#include "Ticket.h"
#include "Patterns.h"

using namespace std;

bool Luck(Ticket tic, list<Pattern*> patterns)
{
    for (auto pattern : patterns)
    {
        if (pattern->isLucky(tic))
            return true;
    }
    return false;
}

int main()
{
    list<int> luckyTics;
    list<Pattern*> patterns
    {
        new PattSum,
        new PattUpper,
        new PattDowner,
        new AllNumbs,
        new Repeat
    };
    // Ticket tic(123456);
    // cout << Luck(tic, patterns);

    for (int i = 111111; i < 1000000; i++)
    {
        Ticket tic(i);
        if (Luck(tic, patterns))
            luckyTics.push_back(i);
    }
    for (auto pattern : patterns)
        delete pattern;

    ofstream os("res.txt");
    for (auto ticket : luckyTics)
        os << ticket << endl;
    os.close();
    cout << "Count of lucky tickets: " << luckyTics.size();
    
    cout << endl;
    return 0;
}