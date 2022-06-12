#include <iostream>
#include "Matrix.h"
#include <fstream>
#include <malloc.h>


using namespace std;

Matrix::~Matrix()
{
    for (int i = 0; i < Width; i++)
        delete[] arr[i];
    delete[] arr;
}

Matrix::Matrix()
{
    Width = 3;
    Height = 3;
    arr = new int*[Width];
    for (int i = 0; i < Width; i++)
        arr[i] = new int[Height];

    int a = 0;
    int b = 9;

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            arr[i][j] = rand() % (b - a + 1) + a;

    }
}

Matrix::Matrix(int width, int height)
{
    Width = width;
    Height = height;

    arr = new int*[Width];
    for (int i = 0; i < Width; i++)
        arr[i] = new int[Height];

    int a = 0;
    int b = 9;

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            arr[i][j] = rand() % (b - a + 1) + a;

    }
}

Matrix::Matrix(const Matrix &mat)
{
    Width = mat.Width;
    Height = mat.Height;
    
    arr = new int*[Width];
    for (int i = 0; i < Width; i++)
        arr[i] = new int[Height];
    
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            arr[i][j] = mat.arr[i][j];
    }
}


void Matrix::Print()
{
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            cout << arr[i][j] << " ";
        cout << endl;
    }
    cout << endl;
}

int Matrix::GetWidth() { return Width; }
int Matrix::GetHeight() { return Height; }

void Matrix::Set(int i, int j, int value)
{ 
    if (i < Width && i > 0)
    {
        if (j < Height && j > 0)
        {
            arr[i][j] = value; 
        }
    }

}

int Matrix::Get(int i, int j)
{ 
    if (i < Width && i > 0)
    {
        if (j < Height && j > 0)
        {
            return arr[i][j]; 
        }
    }
    return 0; //надо влепить exception
}

int Matrix::Min()
{
    int min = arr[0][0];
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
        {
            if (arr[i][j] < min)
                min = arr[i][j];
        }
    }
    return min;
}

bool Matrix::Save()
{

    FILE* file = fopen("output.bin", "wb");
    if (file == NULL)
        return false; 
    
    rewind(file);
    fwrite(&Width, sizeof(int), 1, file);
    fwrite(&Height, sizeof(int), 1, file);
    
    for (int i = 0; i < Width; i++)
    {
        fwrite(arr[i], sizeof(arr[i]), Height, file);
    }
    fclose(file);
    return true;
}

bool Matrix::Load()
{
    FILE* file = fopen("output.bin", "rb");
    if (file == NULL)
        return false; 
    
    int width = 0;
    int height = 0;
    rewind(file);
    fread(&width, sizeof(int), 1, file);
    fread(&height, sizeof(int), 1, file);

    if (width != Width || height != Height)
    {
        for (int i = 0; i < Width; i++)
            delete[] arr[i];
        delete[] arr;
        
        arr = new int*[width];
        for (int i = 0; i < width; i++)
            arr[i] = new int[height];
        Width = width;
        Height = height;
    }

    for (int i = 0; i < Width; i++)
        fread(arr[i], sizeof(arr[i]), Height, file);

    fclose(file);
    return true;
}

void Matrix::Rotate90()
{
    int tmp;
    int** matr = arr;
    int n = Width;
    
    for(int i = 0; i < Width / 2; i++)
    {
        for(int j = i; j < Width - 1 - i; j++)
        {
            tmp = matr[i][j];
            matr[i][j] = matr[j][n - 1 - i];
            matr[j][n - 1 - i] = matr[n - 1 - i][n - 1 - j];
            matr[n - 1 - i][n - 1 - j] = matr[n - 1 - j][i];
            matr[n - 1 - j][i] = tmp;
        }
    }
}

int Matrix::operator()(int i, int j) { return arr[i][j]; }

bool Matrix::operator==(Matrix mat)
{
    if (Width != mat.Width || Height != mat.Height)
        return false;

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
        {
            if (arr[i][j] != mat.arr[i][j])
                return false;
        }
    }

    return true;
}

bool Matrix::operator!=(Matrix mat) { return !(*this == mat); }

int* BubbleSort(int* arr, int height)
{
    for (int i = 0; i < height - 1; i++)
    {
        for (int j = 0; j < height - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
    return arr;
}

void Matrix::Sort()
{
    bool flag = true;
    while (flag)
    {
        flag = false;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height - 1; j++)
            {
                if (arr[i][j] > arr[i][j + 1])
                    flag = true;
            }
        }


        for (int n = 1; n < Width; n++)
        {
            arr[n] = BubbleSort(arr[n], Height);
            arr[n - 1] = BubbleSort(arr[n - 1], Height);
            if (arr[n][0] < arr[n - 1][Height - 1])
            {
                int temp = arr[n][0];
                arr[n][0] = arr[n - 1][Height - 1];
                arr[n - 1][Height - 1] = temp;
            }
        }
    }
}


bool Matrix::operator<=(Matrix mat)
{
    int sum1 = 0;
    int sum2 = 0;
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            sum1 += arr[i][j];
    }
    for (int i = 0; i < mat.Width; i++)
    {
        for (int j = 0; j < mat.Height; j++)
            sum2 += mat.arr[i][j];
    }
    if (sum1 <= sum2) return true;
    else return false;
}

bool Matrix::operator>=(Matrix mat)
{
    int sum1 = 0;
    int sum2 = 0;
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            sum1 += arr[i][j];
    }
    for (int i = 0; i < mat.Width; i++)
    {
        for (int j = 0; j < mat.Height; j++)
            sum2 += mat.arr[i][j];
    }
    if (sum1 >= sum2) return true;
    else return false;
}

Matrix Matrix::operator=(const Matrix& source)
{
    if (Width != source.Width || Height != source.Height)
    {
        for (int i = 0; i < Width; i++)
            delete[] arr[i];
        delete[] arr;
        Width = source.Width;
        Height = source.Height;

        arr = new int*[Width];
        for (int i = 0; i < Width; i++)
            arr[i] = new int[Height];
    }

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            arr[i][j] = source.arr[i][j];
    }
    return *this;
}

Matrix Matrix::operator+(Matrix& source)
{
    if (Width != source.Width || Height != source.Height)
        return *this; // тут надо вернуть exception
    
    Matrix res(Width, Height);

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            res.arr[i][j] = arr[i][j] + source.arr[i][j];
    } 
    
    return res;
}

Matrix Matrix::operator-(const Matrix& source)
{
    if (Width != source.Width || Height != source.Height)
        return *this;
    
    Matrix res(Width, Height);

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            res.arr[i][j] = arr[i][j] - source.arr[i][j];
    } 
    
    return res;
}

Matrix Matrix::operator+(const int n)
{
    Matrix res = *this;

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            res.arr[i][j] = res.arr[i][j] + n;
    } 
    
    return res;
}

Matrix Matrix::operator-(const int n)
{
    Matrix res = *this;

    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            res.arr[i][j] = res.arr[i][j] - n;
    } 
    
    return res;
}

Matrix::operator int()
{
    int sum = 0;
    for (int i = 0; i < Width; i++)
    {
        for (int j = 0; j < Height; j++)
            sum += arr[i][j];
    }
    return sum;
}

Matrix::operator double()
{
    int sum = *this;
    return static_cast<double>(sum) / static_cast<double>(Width * Height);
}

Matrix Matrix::operator++()
{
    Matrix old = *this;
    *this = *this + 1;
    return old;
}

Matrix& Matrix::operator++(int)
{
    *this = *this + 1;
    return *this;
}

Matrix Matrix::operator--()
{
    Matrix old = *this;
    *this = *this - 1;
    return old;
}

Matrix& Matrix::operator--(int)
{
    *this = *this - 1;
    return *this;
}

Matrix& Matrix::operator+=(const int n)
{
    *this = *this + n;
    return *this;
}

Matrix& Matrix::operator-=(const int n)
{
    *this = *this - n;
    return *this;
}

void* Matrix::operator new(size_t size)
{
    time_t seconds = time(NULL);
    tm* timeinfo = localtime(&seconds);

    ofstream out("data.log", ios::app);
    if (out.is_open())
        out << size << " bytes new at  " << asctime(timeinfo);
    out.close();
    return malloc(size);
}

void Matrix::operator delete(void* p)
{
    time_t seconds = time(NULL);
    tm* timeinfo = localtime(&seconds);

    ofstream out("data.log", ios::app);
    if (out.is_open())
        out << malloc_usable_size(p) << " bytes deleted at  " << asctime(timeinfo);
    out.close();
    free(p); 
}

void* Matrix::operator new[](size_t size)
{ 
    time_t seconds = time(NULL);
    tm* timeinfo = localtime(&seconds);

    ofstream out("data.log", ios::app);
    if (out.is_open())
        out << size << " bytes new at  " << asctime(timeinfo);
    out.close();
    return malloc(size); 
}

void Matrix::operator delete[](void* p)
{
    time_t seconds = time(NULL);
    tm* timeinfo = localtime(&seconds);

    ofstream out("data.log", ios::app);
    if (out.is_open())
        out << malloc_usable_size(p) << " bytes deleted at  " << asctime(timeinfo);
    out.close();
    free(p); 
}