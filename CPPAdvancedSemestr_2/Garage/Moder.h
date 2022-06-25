#pragma once
#include "User.h"

class Moderator : public User
{
public:
    Moderator(char* login, char* pass);
    ~Moderator();
    void ShowMenu();
};