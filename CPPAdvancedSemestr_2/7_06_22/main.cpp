#include <iostream>
#include "Person.h"
#include "Student.h"

using namespace std;

int main()
{
    Student stud
    (
        "Levi", "Akkerman",
        30, "Underground City",
        5000, 6, 23435
    );

    stud.PrintInfo();
    return 0;
}