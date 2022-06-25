#include "Moder.h"
#include <iostream>
#include "Garage.h"
#include "Car.h"
#include <fstream>
#include <cstring>
#include <stdio.h>

using namespace std;

Moderator::Moderator(char* login, char* pass) : User(login, pass) {}
Moderator::~Moderator() {}

void Moderator::ShowMenu()
{
    int counter = 0;
    garage.Load();
    while (true)
    {
        if (counter == 0)
            cout << "Welcome to the our garage. What do you want to do?" << endl;
            cout << "1 - See all cars" << endl
                 << "2 - Show all users" << endl
                 << "3 - exit" << endl;
        int num;
        cin >> num;

        switch (num)
        {
        case 1:
            system("clear");
            garage.Print();
            break;
        case 2:
        {
            ifstream in("data.log");
            if (in.is_open())
            {
                while(!in.eof())
                {
                    int level;
                    char* login = new char[40];
                    char* password = new char[40];
                    in >> login >> password >> level;
                    cout << login << " " << password << " " << level << endl;
                    delete[] login;
                    delete[] password;
                }
            }
            in.close();
            break;
        }
        case 3:
            garage.Save();
            exit(0);
            break;
        default:
            cout << "Invalid input" << endl;
            system("clear");
            break;
        }
        cout << endl;
        counter++;
    }
}