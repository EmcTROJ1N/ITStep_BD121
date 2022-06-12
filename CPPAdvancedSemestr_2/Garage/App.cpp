#include "App.h"
#include <iostream>
#include <fstream>
#include <stdio.h>
#include <cstring>
#include "DefaultUser.h"
#include "Admin.h"

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
    if (level == 0)
    {
        DefaultUser user(log, pass);
        user.ShowMenu();
    }
    if (level == 1)
    {
        Admin user(log, pass);
        user.ShowMenu();
    }

    delete[] log;
    delete[] pass;
}