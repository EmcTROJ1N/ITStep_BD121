#include "pch.h"
#include "TCylinder.h"

Cylinder operator/(double value, Cylinder &cyl)
{
    Cylinder c(value / cyl.getRadius(), value / cyl.getHeight());
    return c;
}

Cylinder operator+(double value, Cylinder &cyl)
{
    Cylinder c(value + cyl.getRadius(), value + cyl.getHeight());
    return c;
}

istream &operator>>(istream &is, Cylinder &cyl)
{
     double height, radius;
     is >> radius >> height;
      
     cyl.setHeight(height);
     cyl.setRadius(radius);
    return is;
}

ostream &operator<<(ostream &os, Cylinder &cyl)
{
    os << fixed << setw(6) << setprecision(3) 
       << cyl.getHeight()
       << " x " << cyl.getRadius();

    return os;
}

void task_1()
{
    const int SIZECYL = 10;
    Cylinder cylinders[SIZECYL];

    srand(time(0));

    double min = 1;
    double max = 10;
    
    for (int i = 0; i < SIZECYL; i++)
    {
        cylinders[i].setRadius(min + (max - min) * rand() / RAND_MAX);
        cylinders[i].setHeight(min + (max - min) * rand() / RAND_MAX);
    }

	cout
	    << "\t    +—————+—————————————————+————————————————+————————————————+————————————————+\n"
		<< "\t    |  N  |    Радиус, R    |    Высота, H   |   Площадь, S   |    Объем, V    |\n"
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+\n";
	
    for (int i = 0; i < SIZECYL; i++)
        cylinders[i].printTable(i + 1);
        
    cout
        << "\t    +—————+—————————————————+————————————————+—————————————————————————————————+\n";
    cout << string(2, '\n');

    cout << cylinders[0] << endl;
    cout << cylinders[1] << endl;
    Cylinder c = cylinders[0] + cylinders[1];
    cout << c << endl;
    c = 1. + cylinders[0];
    cout << c << endl;
}