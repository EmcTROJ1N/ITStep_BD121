#pragma once
#include "Car.h"
#include <cstddef>

class Garage
{
	Car** Cars;
	size_t MaxSize;
	size_t CurrentSize;

public:
	Garage(size_t maxSize);
	Garage();
	Garage(const Garage& source);
	~Garage();

	bool Add(Car* car);
	void Print();
	void Remove(unsigned idx);
	bool Save();
	bool Load();

	int getCurrentSize();

	Garage& operator= (const Garage& source);
	Car& operator[] (const unsigned idx);
};