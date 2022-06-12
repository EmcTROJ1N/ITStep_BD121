#include "Admin.h"
#include <iostream>
#include "Garage.h"
#include "Car.h"
#include <fstream>
#include <cstring>
#include <stdio.h>

using namespace std;

Admin::Admin(char* login, char* pass) : User(login, pass) {}
Admin::~Admin() {}

void Admin::ShowMenu()
{
    int counter = 0;
    garage.Load();
    while (true)
    {
        if (counter == 0)
            cout << "Welcome to the our garage. What do you want to do?" << endl;
        cout << "1 - Add car" << endl
            << "2 - See all cars" << endl
            << "3 - Remove car" << endl
            << "4 - Show all users" << endl
            << "5 - Add users" << endl
            << "6 - Remove users (with his cars)" << endl
            << "7 - exit" << endl;
        int num;
        cin >> num;

        switch (num)
        {
        case 1:
        {
            char* brand = new char[40];
            char* model = new char[40];
            char* owner = new char[40];
            unsigned maxSpeed, price;
            cout << "Enter car`s brand: ";
            cin >> brand;
            cout << "Enter car`s model: ";
            cin >> model;
            cout << "Enter car`s max speed: ";
            cin >> maxSpeed;
            cout << "Enter car`s price: ";
            cin >> price;
            cout << "Enter car`s owner: ";
            cin >> owner;

            garage.Add(new Car(brand, model, maxSpeed, price, owner));
            delete[] brand;
            delete[] model;
            delete[] owner;
            break;
        }
        case 2:
            garage.Print();
            break;
        case 3:
            cout << "Enter idx of car: ";
            unsigned idx;
            cin >> idx;
            garage.Remove(idx);
            break;
        case 4:
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
        case 5:
        {
            char* log = new char[40];
            char* pass = new char[40];
            int level;
            cout << "Enter user`s login: ";
            cin >> log;
            cout << "Enter user`s password: ";
            cin >> pass;
            cout << "Enter user`s level: ";
            cin >> level;
            
            ofstream out("data.log", ios::app);
            if (out.is_open())
                out << log << " " << pass << " " << level << endl;
            out.seekp(0);
            delete[] log;
            delete[] pass;
            break;
        }
        case 6:
        {
            char* name = new char[40];
            cout << "Enter name of user for delete: ";
            cin >> name;

            for (int i = 0; i < garage.getCurrentSize(); i++)
            {
                if (strcmp(garage[i].getOwner(), name) == 0)
                    garage.Remove(i);
            }
           
            ifstream in("data.log");
            ofstream out("data2.log");    
            if (in.is_open())
            {
                while (!in.eof())
                {
                    unsigned level;
                    char* login = new char[40];
                    char* pass = new char[40];
                    in >> login >> pass >> level;
                    if (strcmp(login, name) != 0)
                        out << login << " " << pass << " " << level << endl;
                }
            }
            in.close();
            out.close();
            system("rm data.log");
            system("mv data2.log data.log");
            break;
        }
        case 7:
            garage.Save();
            exit(0);
        default:
            cout << "Invalid input" << endl;
            break;
        }
        cout << endl;
        counter++;
    }
}