#include <iostream>
#include "myStruct.h"
#include <cstring>
#include <stdio.h>

using namespace std;

Car getCar()
{
    Car car;
    cout << "Enter car owner: ";
    cin >> car.owner;
    cout << "Enter car brand: ";
    cin >> car.brand;
    char price[66];
    do
    {
        cout << "Enter car price: ";
        cin >> price;
    } while (atoi(price) == 0);
    car.price = atoi(price);

    cout << "Enter car model: ";
    cin >> car.model;
    char vol[66];
    do
    {
        cout << "Enter car volume: ";
        cin >> vol;
    } while (atoi(vol) == 0);
    car.volume = atoi(vol);

    return car;
}

void printCar(Car car)
{
    cout << "Car owner: " << car.owner << endl;
    cout << "Car brand: " << car.brand << endl;
    cout << "Car price: " << car.price << endl;
    cout << "Car model: " << car.model << endl;
    cout << "Car volume: " << car.volume << endl;
}

void printCars(Car* cars, size_t size)
{
    for (int i = 0; i < size; i++)
    {
        printCar(cars[i]);
        if (i < size - 1)
            cout << "-------------------" << endl;
    } 
}

void enterCars(Car* &cars, int size)
{
    for (int i = 0; i < size; i++) cars[i] = getCar();
}

bool saveData(const char* filename, const Car* cars, const size_t size)
{
    FILE* file = NULL;
    file = fopen(filename, "w");
    if (file == NULL) return false;
    for (int i; i < size; i++)
    {
        char buf[66];

        fputs(cars[i].owner, file = file);
        fputs(" ", file = file);
        fputs(cars[i].brand, file = file);
        fputs(" ", file = file);
        fputs(cars[i].model, file = file);
        fputs(" ", file = file);

        sprintf(buf, "%d", cars[i].price);
        fputs(buf, file = file);
        fputs(" ", file = file);


        sprintf(buf, "%d", cars[i].volume);
        fputs(buf, file = file);
        fputs(" ", file = file);

        fputs("\n", file = file);
    }
    fclose(file);
    return true;
}

bool loadData(const char* filename, Car* &cars, size_t size)
{
    FILE* file = NULL;
    file = fopen(filename, "r");
    if (file == NULL) return false;
    char str[66];
    int i = 0;
    while(!feof(file))
    {
        strcpy(str, "");
        fgets(str, 66, file);
        cout << str;
        sscanf(str, "%s %s %s %d %d", &cars[i].owner, &cars[i].brand, &cars[i].model, &cars[i].price, &cars[i].volume);
        i++;
        }
    }
    fclose(file);
    return true;
}