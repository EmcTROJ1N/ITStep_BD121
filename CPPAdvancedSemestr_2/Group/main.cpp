#include <iostream>
#include "Group.h"
#include "Person.h"

using namespace std;

int main()
{
    Group RazvedKorpus(10);
    Group temp(5);
    
    Person* Levi = new Person("Levi", "Akkerman", 30);
    Person* Sasha = new Person("Sasha", "Blaus", 18);
    Person* Eren = new Person("Eren", "Yeger", 19);
    Person* Mikasa = new Person("Mikasa", "Akkerman", 19);


    RazvedKorpus += Levi;
    RazvedKorpus += Sasha;
    temp += Eren;
    temp += Mikasa;
    RazvedKorpus -= Eren;

    RazvedKorpus = RazvedKorpus + temp;

    RazvedKorpus.Print();

    return 0;
}