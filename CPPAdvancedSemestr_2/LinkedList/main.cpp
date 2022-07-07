#include <iostream>
#include "LList.h"

using namespace std;

int main()
{   
    LinkedList lsd;
    lsd.Add("1");
    lsd.Add("2");
    // lsd.Add("1");
    // lsd.Add("2");
    // lsd.Add("1");
    // lsd.Add("3");
    // lsd += "4";

    // lsd.Remove(1);
    lsd += "end";
    // lsd.RemoveAll("1");
    cout << lsd;
    return 0;
}