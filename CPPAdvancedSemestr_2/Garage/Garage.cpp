#include <cstddef>
#include <iostream>
#include "Garage.h"
#include <fstream>

using namespace std;

Garage::Garage(size_t maxSize)
{
	MaxSize = maxSize;
	CurrentSize = 0;

	Cars = new Car*[MaxSize];
}

Garage::Garage()
{
	MaxSize = 30;
	CurrentSize = 0;

	Cars = new Car*[MaxSize];
}

Garage::Garage(const Garage& source)
{
	MaxSize = source.MaxSize;
	CurrentSize = source.CurrentSize;

	Cars = new Car*[MaxSize];

	for (size_t i = 0; i < source.CurrentSize; i++)
		Cars[i] = new Car(*source.Cars[i]);
}

Garage::~Garage()
{
	for (size_t i = 0; i < CurrentSize; i++)
		delete Cars[i];
	delete[] Cars;
}

bool Garage::Add(Car* car)
{
	if (CurrentSize < MaxSize)
	{
		Cars[CurrentSize++] = car;
		return true;
	}
	return false;
}

void Garage::Print()
{
	for (int i = 0; i < CurrentSize; i++)
    {
		Cars[i]->Print();
        cout << endl;
    }
}

void Garage::Remove(unsigned index)
{
    Car* temp;
    temp = Cars[index];
    Cars[index] = Cars[CurrentSize - 1];
    Cars[CurrentSize - 1] = temp;

    delete Cars[CurrentSize - 1]; 
    CurrentSize--;
}

bool Garage::Save()
{
    ofstream out;
    out.open("output.bin");
    if (out.is_open())
    {
        out << CurrentSize << " " << MaxSize << endl;
        for (int i = 0; i < CurrentSize; i++)
        {
            out << Cars[i]->getModel() << " " << Cars[i]->getBrand()
            << " " << Cars[i]->getMaxSpeed() << " " << Cars[i]->getPrice()
            << " " << Cars[i]->getOwner() << endl;
        }
    }
        out.close();
    return true;
}

bool Garage::Load()
{
    
    ifstream in;
    in.open("output.bin");
    
    if (in.is_open())
    {
        for (int i = 0; i < CurrentSize; i++)
            delete Cars[i];
        delete[] Cars;
        in >> CurrentSize >> MaxSize;
        
        Cars = new Car*[MaxSize];
        for (int i = 0; i < CurrentSize; i++)
        {
            char* model = new char[40];
            char* brand = new char[40];
            char* owner = new char[40];
            int maxSpeed, price;
            in >> model >> brand >> maxSpeed >> price >> owner;
            
            Cars[i] = new Car(brand, model, maxSpeed, price, owner);

            delete[] model;
            delete[] brand;
            delete[] owner;
        }
        in.close();
        return true;
    }
    return false;
}



Garage& Garage::operator= (const Garage& source)
{
	for (size_t i = 0; i < CurrentSize; i++)
		delete Cars[i];

	if (CurrentSize != source.CurrentSize)
	{
		delete[] Cars;
		Cars = new Car* [source.MaxSize];
	}

	MaxSize = source.MaxSize;
	CurrentSize = 0;

	for (size_t i = 0; i < source.CurrentSize; i++)
	{
		Car* car = new Car(*source.Cars[i]);
		Add(car);
	}

	return *this;
}
Car& Garage::operator[] (const unsigned idx) { return *Cars[idx]; }
int Garage::getCurrentSize() { return CurrentSize; }