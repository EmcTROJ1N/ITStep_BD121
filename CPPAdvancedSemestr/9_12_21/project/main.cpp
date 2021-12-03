#include "pch.h"
#include "TConoid.h"
#include "tasks.h"

int main()
{
    init("Ну я сделал красивое название, я старался", "Task 3: Conoid");
    cout << string(2, '\n');
    
    task_1();

    cout << string(1, '\n');
    cout << "Press key to continue . . .";
    
    int buf;
    cin >> buf;
    return 0;
}
