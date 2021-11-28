#include "pch.h"
#include "TCylinder.h"

void init(char titleWindow[666], char header[666])
{
    system("clear");
    
    printf("%c]0;%s%c", '\033', titleWindow, '\007');
    cout << header;
}

// istream &operator>>(istream &is, Cylinder &cyl)
// {
//      double height, radius;
//      is >> radius >> height;
      
//      cyl.setHeight(height);
//      cyl.setRadius(radius);
//     return is;
// }

// ostream &operator<<(ostream &os, Cylinder &cyl)
// {
//     os << fixed << setw(6) << setprecision(3) 
//        << cyl.getHeight()
//        << " x " << cyl.getRadius();

//     return os;
// }

// Cylinder &operator/(double value, Cylinder &cyl)
// {
//     Cylinder c(cyl.getRadius() / value, cyl.getHeight() / value);
//     return c;
// }