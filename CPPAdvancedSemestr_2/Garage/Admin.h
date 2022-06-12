#pragma once
#include "User.h"

class Admin : public User
{
public:
    Admin(char* login, char* pass);
    ~Admin();
    void ShowMenu();
};