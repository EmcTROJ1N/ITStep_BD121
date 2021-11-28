#include "pch.h"
#include "TConoid.h"
#include "TCylinder.h"
#include "tasks.h"

int main()
{
    init("Ну я сделал красивое название, я старался", "Task 1: Cylinder");
    cout << string(2, '\n');
    
    task_1();
    // task_2();

    cout << string(1, '\n');
    cout << "Press key to continue . . .";
    
    int buf;
    cin >> buf;
    return 0;
}
