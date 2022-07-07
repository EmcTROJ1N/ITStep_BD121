#include <iostream>
#include "DoubleLinkedList.h"
#include <ctime>

using namespace std;

void charTest()
{
	DoubleLinkedList<char*> lst;
	lst.Add("One");
	lst.Add("Two");
	lst.Add("Three");

	lst.Add("Three");
	lst.Add("Two");
	lst.Add("One");

	cout << "Count of elems: " << lst.GetSize() << endl;
	cout << "Contains One: " << lst.Contains("One") << endl;
	cout << "Count of \"One\" " << lst.GetCount("One") << endl;
	
	lst.Insert(4, "Four");

	DoubleLinkedList<char*> lst2;
	lst2 = lst;

	lst.Add("end.");
	
	lst.RemoveAll("One");
	
	lst.Reverse();
	lst.Print();



	lst.Clear();
	srand(time(0));
	int len = 10;
	char* bukvi[] = { "a", "b", "c", "d", "e", "f", "g", "h",
	"i", "g", "k", "l", "m", "n", "o", "p"}; 
	for (int i = 0; i < len; i++)
		lst.Add(bukvi[rand() % (sizeof bukvi/sizeof (char *))]);

	double start = clock();
	lst.MergeSort(lst.begin(), lst.end(), 1);
	double end = clock();
	double resMerge = end - start;
	
	lst.Clear();
	for (int i = 0; i < len; i++)
		lst.Add(bukvi[rand() % (sizeof bukvi/sizeof (char *))]);

	start = clock();
	lst.BubbleSort();
	end = clock();
	double resBubble = end - start;
	
	cout << "Merge worked: " << resMerge << endl;
	cout << "Bubble worked: " << resBubble << endl;
}

void intTest()
{
	DoubleLinkedList<int> lst;
	for (int i = 0; i < 10; i++)
		lst.Add(i);

	cout << "Contains 5: " << lst.Contains(5) << endl;
	cout << "Count of \"5\" " << lst.GetCount(5) << endl;
	
	lst.Insert(5, 666);
	lst.Print();
	cout << endl;

	DoubleLinkedList<int> lst2;
	lst2 = lst;

	lst.Add(666);
	
	lst.RemoveAll(666);
	
	lst.Reverse();
	lst.Print();


	lst.Clear();
	srand(time(0));
	int len = 10;
	int nums[] = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	for (int i = 0; i < len; i++)
		lst.Add(nums[rand() % (sizeof nums/sizeof (int))]);

	double start = clock();
	lst.MergeSort(lst.begin(), lst.end(), 1);
	double end = clock();
	double resMerge = end - start;
	
	lst.Clear();
	for (int i = 0; i < len; i++)
		lst.Add(nums[rand() % (sizeof nums/sizeof (int))]);

	start = clock();
	lst.BubbleSort();
	end = clock();
	double resBubble = end - start;
	
	lst.Clear();
	for (int i = 0; i < len; i++)
		lst.Add(nums[rand() % (sizeof nums/sizeof (int))]);

	cout << "Merge worked: " << resMerge << endl;
	cout << "Bubble worked: " << resBubble << endl;
}


int main()
{
	charTest();
	cout << endl;
	intTest();
	return 0;
}