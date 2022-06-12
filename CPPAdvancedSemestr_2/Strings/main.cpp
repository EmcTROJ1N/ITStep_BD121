#include <iostream>
#include "strings.h"

using namespace std;

int main()
{
    Strings strs;
    Strings str3;

    strs.AddStrings(3, "Rammstein", "Hello world", "qqq");

    str3.Add("Rammstein");
    str3.Add("qqq");

    strs -= str3;

    strs.Print();

    strs -= "Hello World";
    
    strs.Add("baaaaab");
    strs.Add("aaaab");
    strs.Add("aaaaa");

    strs.Sort();
    
    strs.Print();

    str3 = strs;
    if (str3 == strs) cout << endl << "== works" << endl;
   
    Strings temp;

    temp.Add("test2");
    temp.Add("test");
    temp.Add("test1");
    temp.Add("test2");
    temp.Add("test");
    temp.Add("test2");
    temp.Add("test1");
    temp.Add("test1");
    temp.Add("test");
    temp.Add("test2");
    temp.Add("test2");
    temp.Add("test2");

    temp.RemoveDublicates();
    temp.Print();

    return 0;
}