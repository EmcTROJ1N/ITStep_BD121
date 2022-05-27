#pragma once
#include "TConoid.h"
#include "pch.h"
#include "utils.h"

class ArrayConoid
{
    int _length;
    Conoid* _conoids;

public:
    ArrayConoid():_length(3), _conoids()
    {
        _conoids = new Conoid[_length];

        for (int i = 0; i < _length; i++)
        {
            _conoids[i].setHeight(getRandom(1, 10));
            _conoids[i].setRadiusDown(getRandom(1, 10));
            _conoids[i].setRadiusUpper(getRandom(1, 10));
        }
    }
    ~ArrayConoid() { delete[] _conoids; }

    int size() const { return _length; }

    Conoid &operator[](int index)
    {
        if (index < 0) index = 0;
        if (index >= _length) index = _length - 1; // throw не работает

        return _conoids[index];
    }

    friend ostream &operator<<(ostream &os, ArrayConoid &arr)
    {
       os 
	    << "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n"
		<< "\t    |  N  |  Радиус В.О.,   |   Радиус H.О.  |      Высота    |   Площадь, S   |    Объем, V    |\n"
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n";

        for (int i = 0; i < arr.size(); i++)
        {
            os << arr[i];
        }
        os 
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n";
        return os;
    }

    double sumVolume() const
    {
        double sumVolume;
        for (int i = 0; i < size(); i++)
            sumVolume += _conoids[i].volume();
        return sumVolume;
    }

    double sumSquare() const
    {
        double sumSquare;
        for (int i = 0; i < size(); i++)
            sumSquare += _conoids[i].square();
        return sumSquare;
    }
};