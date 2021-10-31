#include <iostream>

using namespace std;

struct Car
{
    char brand[666];
    char model[666];
    int price;
    int volume;
    char owner[666];
};


Car getCar()
{
    Car car;
    cout << "Enter car owner: ";
    cin >> car.owner;
    cout << "Enter car brand: ";
    cin >> car.brand;
    char price[66];
    do
    {
        cout << "Enter car price: ";
        cin >> price;
    } while (atoi(price) == 0);
    car.price = atoi(price);

    cout << "Enter car model: ";
    cin >> car.model;
    char vol[66];
    do
    {
        cout << "Enter car volume: ";
        cin >> vol;
    } while (atoi(vol) == 0);
    car.volume = atoi(vol);

    return car;
}

void printCar(Car car)
{
    cout << "Car owner: " << car.owner << endl;
    cout << "Car brand: " << car.brand << endl;
    cout << "Car price: " << car.price << endl;
    cout << "Car model: " << car.model << endl;
    cout << "Car volume: " << car.volume << endl;
}

void printCars(Car* cars, size_t size)
{
    for (int i = 0; i < size; i++)
    {
        printCar(cars[i]);
        if (i < size - 1)
            cout << "-------------------" << endl;
    } 
}

void enterCars(Car* &cars, int size)
{
    for (int i = 0; i < size; i++) cars[i] = getCar();
}

bool saveData(const char* filename, const Car* cars, const size_t size)
{
    FILE* file = NULL;
    file = fopen(filename, "wb");
    if (file == NULL) return false;
    fwrite(cars, sizeof(Car), size, file);
    fclose(file);
    return true;
}

bool loadData(const char* filename, Car* &cars, size_t size)
{
    FILE* file = NULL;
    file = fopen(filename, "rb");
    if (file == NULL) return false;
	fread(cars, sizeof(Car), size, file);
    fclose(file);
    return true;
}

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
            FILE* file = fopen("count.dat", "wb");
            if (file != NULL)
            {
                fwrite(&size, sizeof(int), 1, file);
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
            FILE* file = fopen("count.dat", "rb");
            if (file != NULL)
            {
                fread(&size, sizeof(int), 1, file);
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