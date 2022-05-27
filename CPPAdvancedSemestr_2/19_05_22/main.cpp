#include <iostream>
#include "MyString.h"

using namespace std;
 
int main() 
 {
	MyString str("Rammstein");
	cout << str.Get() << endl;
	// str.RemoveDigits();
	// cout << str.Get();
	
	cout << endl;
	return 0;
};