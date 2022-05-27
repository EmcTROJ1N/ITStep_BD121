#include <iostream>
#include "Matrix.h"
#include <ctime>

using namespace std;

int main()
{
    srand(time(0));

    Matrix* mats = new Matrix[5];
    mats[0].Print();

    delete[] mats;
    return 0;
}