#pragma once

class Matrix
{
private:
    int** arr;
    int Width;
    int Height;
public:
    ~Matrix();
    Matrix();
    Matrix(int width, int height);
    Matrix(const Matrix &mat);

    int GetWidth();
    int GetHeight();
    void Print();
    void Set(int i, int j, int value);
    int Get(int i, int j);
    int Min();
    bool Save();      // 1 2 3  0,0 0,1 0,2  7 4 1  2,0 1,0 0,0
    bool Load();      // 4 5 6  1,0 1,1 1,2  8 5 2  2,1 1,1 0,1
    void Rotate90();  // 7 8 9  2,0 2,1 2,2  9 6 3  2,2 1,2 0,2
    void Sort();
    int Surface();

    int operator()(int i, int j);
    bool operator==(Matrix mat);
    bool operator!=(Matrix mat);
    bool operator<=(Matrix mat);
    bool operator>=(Matrix mat);
    Matrix operator=(const Matrix& source);
    Matrix operator+(Matrix& source);
    Matrix operator+(const int n);
    Matrix operator-(const Matrix& source);
    Matrix operator-(const int n);
    operator int();
    operator double();
    Matrix operator++();
    Matrix& operator++(int);
    Matrix operator--();
    Matrix& operator--(int);
    Matrix& operator+=(const int n);
    Matrix& operator-=(const int n);
    void* operator new(size_t size);
    void operator delete(void* p);
    void* operator new[](size_t size);
    void operator delete[](void* p);

};