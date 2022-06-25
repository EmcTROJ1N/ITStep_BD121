#include "App.h"
#include <iostream>
#include <fstream>
#include <stdio.h>
#include <cstring>
#include "DefaultUser.h"
#include "Admin.h"
#include "Moder.h"

using namespace std;

App::App() {}
App::~App() {}
void App::run()
{
    int level;
    char* log = new char[40];
    char* pass = new char[40];
    cout << "Enter login: ";
    cin >> log;
    cout << "Enter pass: ";
    cin >> pass;

    ifstream in("data.log");
    bool flag = false;
    if (in.is_open())
    {
        while(!in.eof())
        {
            char* login = new char[40];
            char* password = new char[40];
            in >> login >> password >> level;

            if (strcmp(login, log) == 0 && strcmp(password, pass) == 0)
            {
                flag = true;
                delete[] login;
                delete[] password;
                break;
            }
            delete[] login;
            delete[] password;
        }
    }
    if (!flag)
    {
        cout << "Invalid Log/Pass";
        exit(0);
    }
    
    User* user;
    switch (level)
    {
        case 1: 
            user = new Admin(log, pass);
            break;
        case 0: 
            user = new DefaultUser(log, pass);
            break;
        case 3:
            user = new Moderator(log, pass);

    }

    delete[] log;
    delete[] pass;
    
    user->ShowMenu();

}