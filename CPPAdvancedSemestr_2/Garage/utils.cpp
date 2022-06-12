#include <iostream>
#include "utils.h"

using namespace std;

void init(char titleWindow[666], char header[666])
{
    system("clear");
    
    printf("%c]0;%s%c", '\033', titleWindow, '\007');
    srand(time(0));
    cout << header;
}
double getRandom(double min, double max) { return (min + (max - min) * rand() / RAND_MAX); }



// istream &operator>>(istream &is, Conoid &con)
// {
//      double radiusDown, radiusUpper, height;
//      is >> radiusDown >> radiusUpper >> height;
      
//      con.setRadiusDown(radiusDown);
//      con.setRadiusUpper(radiusUpper);
//      con.setHeight(height);
//     return is;
// }

// ostream &operator<<(ostream &os, Conoid &con)
// {
//         os	
//         	<< "\t    | " << setw(3) << 0 << " | " << setw(15) << con.getRadiusUpper() << " | " << setw(14) << con.getRadiusDown() << " | " << setw(14) << con.getHeight()
// 			<< " | " << setw(14) << con.square()
// 			<< " | " << setw(14) << con.volume()
// 		    << " |" << endl;

//     return os;
// }