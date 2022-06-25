#include "Book.h"
#include <iostream>
#include <cstring>

using namespace std;

Book::Book(char* title, char* author, char* pages, int price)
{
    Title = new char[40];
    Author = new char[40];
    Pages = new char[40];
    strcpy(Title, title);
    strcpy(Author, author);
    strcpy(Pages, pages);
    Price = price;
}

Book::~Book()
{
    delete[] Title;
    delete[] Author;
    delete[] Pages;
}

void Book::Print()
{
    cout << "Book`s title: " << Title << endl
         << "Book`s author: " << Author << endl
         << "Book`s pages: " << Pages << endl
         << "Book`s price: " << Price << endl;
}