#include "DefaultUser.h"
#include <iostream>
#include <cstring>

using namespace std;

DefaultUser::DefaultUser(char* login, char* pass) : User(login, pass) {}
DefaultUser::~DefaultUser() {}

void DefaultUser::ShowMenu()
{
    garage.Load();

    int counter = 0;
    garage.Load();
    while (true)
    {
        if (counter == 0)
            cout << "Welcome to the our garage. What do you want to do?" << endl;
        cout << "1 - Add car" << endl
            << "2 - See all cars" << endl
            << "3 - Remove car (just your car)" << endl
            << "4 - exit" << endl;
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
            if (strcmp(garage[idx].getOwner(), Login) == 0)
                garage.Remove(idx);
            else throw "It is not your car";
            break;
        case 4:
            garage.Save();
            exit(0);
        default:
            cout << "Invalid input" << endl;
            break;
        }
        counter++;
    }
    garage.Save();
}