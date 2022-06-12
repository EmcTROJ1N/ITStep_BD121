#include <iostream>
#include "Worker.h"
#include "Person.h"
#include "Staff.h"
#include "Manager.h"
#include "Freelancer.h"


using namespace std;

int main()
{
    Staff staff(10);
    
    Manager* manager = new Manager
    (
        "Sherlok", "Holmes", 30,
        "BakerStreet 221b", 1000, 12342,
        1000, 221, 1234
    );
    Worker* worker = new Worker
    (
        "Anton", "Trojanov", 25,
        "lala", 1000, 12345
    );
    Freelancer* freelancer = new Freelancer
    (
        "Illya", "Illyich", 54,
        "Moscau", 1000, 1
    );

    staff.Add(manager);
    staff.Add(worker);
    staff.Add(freelancer);

    staff.Print();
    cout << staff.GetSalarieSum();
    return 0;
}