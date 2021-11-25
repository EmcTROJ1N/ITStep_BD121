#include "pch.h"
#include "TCylinder.h"
#include "TConoid.h"

void task_1()
{
    Cylinder cylinders[8];

    srand(time(0));

    for (int i = 0; i < 8; i++)
    {
        double min = 1;
        double max = 10;
        cylinders[i].setRadius(min + (max - min) * rand() / RAND_MAX);
        cylinders[i].setHeight(min + (max - min) * rand() / RAND_MAX);
    }

	cout
	    << "\t    +—————+—————————————————+————————————————+————————————————+————————————————+\n"
		<< "\t    |  N  |    Радиус, R    |    Высота, H   |   Площадь, S   |    Объем, V    |\n"
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+\n";
	
    for (int i = 0; i < 8; i++)
        cylinders[i].printTable(i + 1);
        
    cout
        << "\t    +—————+—————————————————+————————————————+—————————————————————————————————+\n";
    cout << string(2, '\n');

    int index = 0;
    for (int i = 0; i < 8; i++)
    {
        if (cylinders[index].volume() < cylinders[i].volume())
            index = i;
    }

    double sumSq = 0;
    double sumVol = 0;
    for (int i = 0; i < 8; i++)
    {
        sumSq += cylinders[i].square();
        sumVol += cylinders[i].volume();
    }
    
    cout << "Max cylinder is number: " << ++index << endl;
    cout << "Sum square : " << sumSq << endl;
    cout << "Sum volume: " << sumVol << endl;
    cout << string(2, '\n');

    
}

void task_2()
{
    Conoid conoids[5];

    srand(time(0));

    for (int i = 0; i < 8; i++)
    {
        double min = 1;
        double max = 10;
        conoids[i].setRadiusUpper(min + (max - min) * rand() / RAND_MAX);
        conoids[i].setRadiusDown(min + (max - min) * rand() / RAND_MAX);
        conoids[i].setHeight(min + (max - min) * rand() / RAND_MAX);
    }

	cout
	    << "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n"
		<< "\t    |  N  |  Радиус В.О.,   |   Радиус H.О.  |      Высота    |   Площадь, S   |    Объем, V    |\n"
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n";
	
    for (int i = 0; i < 8; i++)
        conoids[i].printTable(i + 1);
        
    cout
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n";
    cout << string(2, '\n');

    int index = 0;
    for (int i = 0; i < 8; i++)
    {
        if (conoids[index].square() < conoids[i].square())
            index = i;
    }

    double sumSq = 0;
    double sumVol = 0;
    for (int i = 0; i < 8; i++)
    {
        sumSq += conoids[i].square();
        sumVol += conoids[i].volume();
    }
    
    cout << "Max conoids is number: " << ++index << endl;
    cout << "Sum square : " << sumSq << endl;
    cout << "Sum volume: " << sumVol << endl;
    cout << string(2, '\n');

    
}