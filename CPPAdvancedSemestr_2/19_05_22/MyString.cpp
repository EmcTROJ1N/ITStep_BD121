#include "MyString.h"
#include <iostream>
#include <cstring>

using namespace std;

MyString::~MyString() { delete[] Str; }
MyString::MyString() 
{
	Str = "Hello World";
	MaxSize = strlen(Str);
}

MyString::MyString(char* _str)
{
	MaxSize = strlen(_str);
	delete[] Str;
	Str = new char[strlen(_str) + 1];
	strcpy(Str, _str);
}

MyString::MyString(const MyString& source) 
{
	MaxSize = source.MaxSize;

	if (strlen(source.Str) > MaxSize)
	{
		MaxSize = strlen(source.Str);
		delete[] Str;
		Str = new char[strlen(source.Str) + 1];
	}
	strcpy(Str, source.Str);
}

void  MyString::Print() { cout << Str; }
void MyString::Set(char* _str)
{ 
	if (strlen(_str) > MaxSize)
	{
		MaxSize = strlen(_str);
		delete[] Str;
		Str = new char[strlen(_str) + 1];
	}
	strcpy(Str, _str);
}
char* MyString::Get() { return Str; }
int MyString::GetLength() { return strlen(Str); }

int MyString::GetVowelsCount()
{
	int i = 0;
	int vowlesCount = 0;
	while (Str[i] != 0)
	{
		if (Str[i] == 'a' || Str[i] == 'A'||Str[i] == 'e' || Str[i] == 'E' || Str[i] == 'i' || Str[i] == 'I' || Str[i] == 'o' || Str[i] == 'O' || Str[i] == 'u' || Str[i] == 'U' || Str[i] == 'y' || Str[i] == 'Y')
			vowlesCount++;
		i++;
	}
	return vowlesCount;
}

void  MyString::ToUpper()
{
	int i = 0;
	while (Str[i] != 0)
	{
		if (Str[i] > 96 && Str[i] < 123)
			Str[i] -= 32;
		i++;
	}
}

void MyString::ToLower()
{
	int i = 0;
	while (Str[i] != 0)
	{
		if (Str[i] > 64 && Str[i] < 91)
			Str[i] += 32;
		i++;
	}
}

void MyString::RemoveDigits() {
	int digitsCount = 0;
	for (int i = 0; i < strlen(Str); i++)
	{
		if (Str[i] > '0' && Str[i] < '9')
			digitsCount++;
	}
	char* _str = new char[strlen(Str) - digitsCount];
	
	for (int i = 0, j = 0; i < strlen(_str); i++)
	{
		if (!(Str[i] > '0' && Str[i] < '9'))
		{
			_str[j] = Str[i];
			j++;
		}
	}
	Str = _str;
	MaxSize = strlen(Str);
	delete[] _str;
	
}