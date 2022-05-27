#pragma once
#include "pch.h"
#include "TConoid.h"
#include "utils.h"
#include "ArrayConoid.h"

class App
{
public:

    void demoTask()
    {
        ArrayConoid arr;
        cout << arr;
        cout << endl << endl;
        arr[0]++;
        cout << arr;
    }
};