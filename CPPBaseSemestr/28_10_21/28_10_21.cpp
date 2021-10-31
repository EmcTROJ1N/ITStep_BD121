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

// сохранение массива структур в файл
int LoadBook(const char* filename, Book* book)
{
	FILE* f = NULL;
	f = fopen(filename, "rb");
	size_t size = NULL;
	if (f != NULL)
	{
		fread(&size, sizeof(size_t), 1, f);
		fread(book, sizeof(Book), size, f);
		fclose(f);
		return size;
	}
	else return 0;
}


int main()
{
	// размер массива (количество людей)
	const int MaxSize = 100;

	size_t Size = 0;

	// объявление динамиского массива типа Person
	Book* books = new Book[MaxSize];

	int key_code;
	while (1)
	{
		PrintMenu();

        char key_code;
        cin >> key_code;

		char filename[80];
		switch (key_code)
		{
			case '1':
				if (Size < MaxSize)
				{
					Book book = EnterBook();
					books[Size] = book;
					Size++;
				} else {
					cout << "There is no space for another book!" << endl;
				}
				break;
			case '2':
				PrintBooks(books, Size);
				break;
			case '3':
				Size = 0;
				cout << "All records were deleted!!!" << endl << endl;
				break;
			case '4':
				cout << "Enter file name for DB saving: ";
				cin >> filename;
				SaveBook(filename, books, Size);
				break;
			case '5':
				cout << "Enter file name for DB loading: ";
				cin >> filename;
				Size = LoadBook(filename, books);
				break;
			case '6':
                Size--;
				cout << "One book were deleted" << endl << endl;
				break;
            case '7':
                return 0;
                break;
		}
	}

	delete[] books;
    return 0;
}