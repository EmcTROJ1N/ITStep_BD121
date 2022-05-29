#include <iostream>
#include "Group.h"
#include "Person.h"

using namespace std;

int main()
{
    Person* Levi = new Person("Levi", "Akkerman", 30);
    Person* Sasha = new Person("Sasha", "Blaus", 18);
    Person* Eren = new Person("Eren", "Yeger", 19);
    Person* Mikasa = new Person("Mikasa", "Akkerman", 19);

    Group RazvedKorpus(10);
    RazvedKorpus += Levi;
    RazvedKorpus += Sasha;
    RazvedKorpus += Eren;
    RazvedKorpus = RazvedKorpus + Mikasa;

    Group test = RazvedKorpus;

    if (test == RazvedKorpus) cout << "copy const work" << endl;

    Group test2;
    test2 = RazvedKorpus;

    if (test2 == RazvedKorpus) cout << "operator= work" << endl;

    cout << endl;
    RazvedKorpus.Print();

    return 0;
}