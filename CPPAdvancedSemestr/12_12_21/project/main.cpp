#include "pch.h"
#include "TConoid.h"
#include "utils.h"
#include "App.h"

int main()
{
    init("Ну я сделал красивое название, я старался", "Task 3: Conoid");
    cout << string(2, '\n');

    try
    {
        App app;
        app.demoTask();
    }
    catch(const exception& e)
    {
        cerr << e.what() << '\n';
    }
       
    cout << string(1, '\n');
    cout << "Press key to continue . . .";
    
    int buf;
    cin >> buf;
    return 0;
}
