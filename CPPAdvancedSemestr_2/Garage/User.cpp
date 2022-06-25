#include "User.h"
#include <cstring>
#include <iostream>

using namespace std;

User::User(char* log, char* pass)
{
    Login = new char[strlen(log) + 1];
    Password = new char[strlen(pass) + 1];
    strcpy(Login, log);
    strcpy(Password, pass);
}

User::~User()
{
    delete[] Login;
    delete[] Password;
    delete this;
}