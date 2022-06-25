#pragma once

class Book
{
    char* Title;
    char* Author;
    char* Pages;
    int Price;
public:
    Book(char* title, char* author, char* pages, int price);
    ~Book();
    void Print();
};