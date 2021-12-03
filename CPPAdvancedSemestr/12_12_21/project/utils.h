#pragma once
#include "TConoid.h"

void init(char title[666], char header[666]);
double getRandom(double min, double max);
Conoid operator/(double value, Conoid &con);
Conoid operator*(double value, Conoid &con);
Conoid operator-(double value, Conoid &con);
istream &operator>>(istream &is, Conoid &con);
ostream &operator<<(ostream &os, Conoid &con);