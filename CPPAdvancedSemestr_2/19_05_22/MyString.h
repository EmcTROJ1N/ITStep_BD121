#pragma once
class MyString
{
	char* Str;
	unsigned int MaxSize;
	

public:
	~MyString();
	MyString();
	MyString(char newstr[]);
	MyString(const MyString& sourse);
	void Print();
	void Set( char newstr[]);
	char* Get();
	int GetLength();
	int GetVowelsCount();
	void ToUpper();
	void ToLower();

	void RemoveDigits();

};

