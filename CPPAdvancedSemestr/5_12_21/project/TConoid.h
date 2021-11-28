#pragma once

class Conoid
{
    double _radiusUpper;
    double _radiusDown;
    double _height;
public:
    double square() { return M_PI * _height * (_radiusDown + _radiusUpper);}
    double volume() { return M_PI * 1 /3 * _height * (pow(_radiusUpper, 2) + _radiusUpper * _radiusDown + pow(_radiusDown, 2)); }

    Conoid(): _radiusDown(0), _radiusUpper(0), _height(0) {}
    Conoid(double radiusDown, double radiusUpper, double height): _radiusUpper(radiusUpper), _radiusDown(radiusDown), _height(height) {}
    ~Conoid(){ cout << "Conoid are destroyed " << endl; }

    void printTable(int number)
    {
        cout 
        	<< "\t    | " << setw(3) << number << " | " << setw(15) << _radiusUpper << " | " << setw(14) << _radiusDown << setw(14) << _height
			<< " | " << setw(14) << square()
			<< " | " << setw(14) << volume()
		    << " |" << endl;
    }

    void setRadiusUpper(double radius)
    {
        if (radius <= 0)
            return;
        _radiusUpper = radius;
    }
    void setRadiusDown(double radius)
    {
        if (radius <= 0)
            return;
        _radiusDown = radius;
    }
    
    void setHeight(double height)
    {
        if (height <= 0)
            return;
        _height = height;
    }
    
    double getRadiusDown() { return _radiusDown; }
    double getRadiusUpper() { return _radiusUpper; }
    double getHeight() { return _height; }
};