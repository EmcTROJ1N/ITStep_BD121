#pragma once

struct Car
{
    char brand[666];
    char model[666];
    int price;
    int volume;
    char owner[666];
};

Car getCar();
void printCar(Car car);
void printCars(Car* cars, size_t size);
void enterCars(Car* &cars, int size);
bool saveData(const char* filename, const Car* cars, const size_t size);
bool loadData(const char* filename, Car* &cars, size_t size);