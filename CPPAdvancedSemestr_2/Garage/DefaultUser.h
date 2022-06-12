#pragma once
#include "User.h"

class DefaultUser : public User
{
public:
    DefaultUser(char* login, char* pass);
    ~DefaultUser();
    void ShowMenu();
};