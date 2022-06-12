#include <iostream>
#include "Car.h"
#include <cstring>

using namespace std;

Car::Car()
{
	Brand = new char[40];
	strcpy(Brand, "Unknown");

	Model = new char[40];
	strcpy(Model, "Unknown");
	
	Owner = new char[40];
	strcpy(Owner, "Unknown");

	Price = MaxSpeed = 0;
}

Car::Car(const char* brand, const char* model, const unsigned maxSpeed, const unsigned price, const char* owner)
{
	Brand = new char[strlen(brand) + 1];
	strcpy(Brand, brand);

	Model = new char[strlen(model) + 1];
	strcpy(Model, model);

	Owner = new char[40];
	strcpy(Owner, owner);
	
	Price = price;
	MaxSpeed = maxSpeed;
}

Car::Car(const Car& source)
{
	Brand = new char[strlen(source.Brand) + 1];
	strcpy(Brand, source.Brand);

	Model = new char[strlen(source.Model) + 1];
	strcpy(Model, source.Model);

	Owner = new char[strlen(source.Owner) + 1];
	strcpy(Owner, source.Owner);

	Price = source.Price;
	MaxSpeed = source.MaxSpeed;
}

Car::~Car()
{
	delete[] Brand;
	delete[] Model;
	delete[] Owner;
}

Car Car::operator=(const Car& source)
{
	delete[] Brand;
	delete[] Model;
	delete[] Owner;

	Brand = new char[strlen(source.Brand) + 1];
	strcpy(Brand, source.Brand);

	Model = new char[strlen(source.Model) + 1];
	strcpy(Model, source.Model);
	
	Owner = new char[strlen(source.Owner) + 1];
	strcpy(Owner, source.Owner);

	Price = source.Price;
	MaxSpeed = source.MaxSpeed;

	return *this;
}

char* Car::getBrand() { return Brand; }
char* Car::getModel() { return Model; }
char* Car::getOwner() { return Owner; }
unsigned Car::getMaxSpeed() { return MaxSpeed; }
unsigned Car::getPrice() { return Price; }

void Car::Print()
{
	cout << "Brand: " << Brand << endl;
	cout << "Model: " << Model << endl;
	cout << "Max speed: " << MaxSpeed << endl;
	cout << "Price: " << Price << endl;
	cout << "Ownew: " << Owner << endl;
}