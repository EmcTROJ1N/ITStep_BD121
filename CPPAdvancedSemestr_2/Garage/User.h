#pragma once
#include "Father.h"

class User : public Father
{
protected:
    char* Login;
    char* Password;
public:
    User(char* log, char* pass);
    ~User();
    virtual void ShowMenu();

};