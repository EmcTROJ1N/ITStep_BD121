#pragma once

class Car
{
	char* Owner;
	char* Brand;
	char* Model;
	unsigned MaxSpeed;
	unsigned Price;

public:
	Car();
	Car(const char* brand, const char* model, const unsigned maxSpeed, const unsigned price, const char* owner);
	Car(const Car& source);
	~Car();
	Car operator= (const Car& source);
	void Print();
	char* getBrand();
	char* getModel();
	char* getOwner();
	unsigned getMaxSpeed();
	unsigned getPrice();
};

