#include <iostream>
#include "Book.h"

using namespace std;

int main()
{
    Book book("Eragon", "Kris Paolini", "666", 999);

    book.Print();
    return 0;
}