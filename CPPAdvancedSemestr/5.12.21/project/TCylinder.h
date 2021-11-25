#pragma once

class Cylinder
{
    double _radius;
    double _height;
public:
    double square() { return 2. *  M_PI * _radius * (_radius + _height);}
    double volume() { return M_PI * _radius * _radius * _height; }

    Cylinder(): _radius(0), _height(0) {}
    Cylinder(double radius, double height): _radius(radius), _height(height) {}
    ~Cylinder(){ cout << "Cylinder are destroyed " << endl; }

    void printTable(int number)
    {
        cout 
        	<< "\t    | " << setw(3) << number << " | " << setw(15) << _radius
			<< " | " << setw(14) << _height
			<< " | " << setw(14) << square()
			<< " | " << setw(14) << volume()
		    << " |" << endl;
    }

    void setRadius(double radius)
    {
        if (radius <= 0)
            return;
        _radius = radius;
    }
    
    void setHeight(double height)
    {
        if (height <= 0)
            return;
        _height = height;
    }
    
    double getRadius() { return _radius; }
    double getHeight() { return _height; }
};