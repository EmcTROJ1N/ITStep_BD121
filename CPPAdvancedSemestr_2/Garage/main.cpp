#include <iostream>
#include "App.h"
#include "utils.h"

using namespace std;

int main()
{

    init("Garage", "Task 1: Garage");
	cout << string(2, '\n');
	
	App app;
	
	try
	{
		app.run();
	}

	catch (exception& ex)
	{
		cerr << ex.what();
		cout << "Error program, aborting...";
	}
	return 0;
}