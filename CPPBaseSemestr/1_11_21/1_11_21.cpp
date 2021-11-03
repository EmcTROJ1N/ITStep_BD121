#include <iostream>

using namespace std;

void PrintMenu()
{
	cout << "1. Add book" << endl;
	cout << "2. Print books" << endl;
	cout << "3. Delete all" << endl;
	cout << "4. Save DB to file" << endl;
	cout << "5. Load DB from file" << endl;
    cout << "6. Delete one book" << endl;
	cout << "7. Exit" << endl << endl;
}

struct Book
{
	char N[40];
	char name[40];
	char author[40];
	int yearOfPublishing;
	int price;
	int countPages;
};

typedef void (*func_type)(size_t&, int, Book*&);

void PrintBook(Book book)
{
	cout << "N: " << book.N << endl;
	cout << "Name of book: " << book.name << endl;
	cout << "Author: " << book.author << endl;
	cout << "Book`s year of publishing: " << book.yearOfPublishing << endl;
	cout << "Price: " << book.price << endl;
	cout << "Count pages: " << book.countPages << endl;
	cout << endl;
}

void PrintBooks(const Book* book, const size_t size)
{
	for (int i = 0; i < size; i++)
		PrintBook(book[i]);
}

Book EnterBook()
{
	Book book;

	cout << "Enter N: ";
    cin >> book.N;
    cout << "Enter name of book: ";
    cin >> book.name;
    cout << "Enter author: ";
    cin >> book.author;
    cout << "Enter book`s year of publishing: ";
    cin >> book.yearOfPublishing;
    cout << "Enter price: ";
    cin >> book.price;
    cout << "Enter count pages: ";
    cin >> book.countPages;

	cout << endl;

	return book;
}

void EnterBooks(Book* book, const size_t size)
{
	for (int i = 0; i < size; i++)
		book[i] = EnterBook();
}

int SaveBook(const char* filename, const Book* book, const size_t size)
{
	FILE* f = NULL;
	f = fopen(filename, "wb");
	if (f != NULL)
	{
		fwrite(&size, sizeof(size_t), 1, f);
		fwrite(book, sizeof(Book), size, f);
		fclose(f);
		return 1;
	}
	else return 0;
}

int LoadBook(const char* filename, Book* book)
{
	FILE* f = NULL;
	f = fopen(filename, "rb");
	size_t size = 0;
	if (f != NULL)
	{
		fread(&size, sizeof(size_t), 1, f);
		fread(book, sizeof(Book), size, f);
		fclose(f);
		return size;
	}
	else return 0;
}


void switchOne(size_t &Size, int MaxSize, Book* &books)
{
	if (Size < MaxSize)
	{
		Book book = EnterBook();
		books[Size] = book;
		Size++;
	} else {
		cout << "There is no space for another book!" << endl;
			}
}

void switchTwo(size_t &Size, int MaxSize, Book* &books)
{
	PrintBooks(books, Size);
}

void switchThree(size_t &Size, int MaxSize, Book* &books)
{
	Size = 0;
	cout << "All records were deleted!!!" << endl << endl;
}

void switchFour(size_t &Size, int MaxSize, Book* &books)
{
	char filename[666];
	cout << "Enter file name for DB saving: ";
	cin >> filename;
	SaveBook(filename, books, Size);
}

void switchFive(size_t &Size, int MaxSize, Book* &books)
{
	char filename[666];
	cout << "Enter file name for DB loading: ";
	cin >> filename;
	Size = LoadBook(filename, books);
}

void switchSix(size_t &Size, int MaxSize, Book* &books)
{
    Size--;
	cout << "One book were deleted" << endl << endl;
}

void switchSeven(size_t &Size, int MaxSize, Book* &books)
{
	exit(0);
}



int main()
{
	func_type menu[7] = {switchOne, switchTwo, switchThree, switchFour, switchFive, switchSix, switchSeven};
	const int MaxSize = 100;
	size_t Size = 0;
	Book* books = new Book[MaxSize];
	int key_code;
	
	while (1)
	{
		PrintMenu();
        int key_code;
        cin >> key_code;
		key_code--;
		menu[key_code](Size, MaxSize, books);
	}

	delete[] books;
    return 0;
}