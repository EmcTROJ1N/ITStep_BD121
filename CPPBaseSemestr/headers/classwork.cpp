#include <iostream>
#include "myStruct.h"
#include <cstring>

using namespace std;

int main()
{
    int switcher;
    cout << "Save or load data?" << endl;
    cout << "1 - save" << endl;
    cout << "2 - load" << endl;
    cin >> switcher;
    switch (switcher)
    {
        case 1:
        {
            int size;
            cout << "Enter count of cars: ";
            cin >> size;
            Car* cars = new Car[size];
            FILE* file = fopen("count.dat", "w");
            if (file != NULL)
            {
                char buf[20];
                sprintf(buf, "%d", size);
                fputs(buf, file = file);
                fclose(file);
            }

            enterCars(cars, size);
            if (saveData("log.data", cars, size)) cout << "Data saved successful";
            else cout << "Something went wrong...";
            delete[] cars;
            break;
        }
        case 2:
        {
            int size;
            FILE* file = fopen("count.dat", "r");
            if (file != NULL)
            {
                char str[66];
                fgets(str, 66, file);
                size = atoi(str);
            }
            else
            {
                cout << "Something went wrong...";
                return 0;
            }
            Car* cars = new Car[size];

            if (loadData("log.data", cars, size))
            {
                cout << "Data loaded successful" << endl;
                printCars(cars, size);
            } 
            else cout << "Something went wrong...";
            delete[] cars;
            break;
        }
    
        default: cout << "Incorrect number..."; return 0;
            break;
    }

    return 0;
}