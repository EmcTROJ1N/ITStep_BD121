#pragma once
#include "Garage.h"
#include "Father.h"

class App : public Father
{
public:
    App();
    ~App();
    void run();
};