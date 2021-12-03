#include "pch.h"
#include "TConoid.h"

Conoid operator/(double value, Conoid &con)
{
    Conoid c(value / con.getRadiusDown(), value / con.getRadiusUpper(), value / con.getHeight());
    return c;
}

Conoid operator*(double value, Conoid &con)
{
    Conoid c(value * con.getRadiusDown(), value * con.getRadiusUpper(), value * con.getHeight());
    return c;
}

Conoid operator-(double value, Conoid &con)
{
    Conoid c(con.getRadiusDown() - value, con.getRadiusUpper() - value, con.getHeight() - value);
    return c;
}

istream &operator>>(istream &is, Conoid &con)
{
     double radiusDown, radiusUpper, height;
     is >> radiusDown >> radiusUpper >> height;
      
     con.setRadiusDown(radiusDown);
     con.setRadiusUpper(radiusUpper);
     con.setHeight(height);
    return is;
}

ostream &operator<<(ostream &os, Conoid &con)
{
        os	
        	<< "\t    | " << setw(3) << 0 << " | " << setw(15) << con.getRadiusUpper() << " | " << setw(14) << con.getRadiusDown() << " | " << setw(14) << con.getHeight()
			<< " | " << setw(14) << con.square()
			<< " | " << setw(14) << con.volume()
		    << " |" << endl;

    return os;
}

void task_1()
{
    int k = 3;
    cin >> k;
    Conoid* conoids = new Conoid[k];

    srand(time(0));

    for (int i = 0; i < k; i++)
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
	
    for (int i = 0; i < k; i++)
        cout << conoids[i];
        
    cout
		<< "\t    +—————+—————————————————+————————————————+————————————————+————————————————+—————————————————\n";
    cout << string(2, '\n');

    int index = 0;
    for (int i = 0; i < k; i++)
    {
        if (conoids[index].square() < conoids[i].square())
            index = i;
    }

    double sumSq = 0;
    double sumVol = 0;
    for (int i = 0; i < k; i++)
    {
        sumSq += conoids[i].square();
        sumVol += conoids[i].volume();
    }
    
    cout << "Max conoids is number: " << ++index << endl;
    cout << "Sum square : " << sumSq << endl;
    cout << "Sum volume: " << sumVol << endl;
    cout << string(2, '\n');

    delete[] conoids;  
}