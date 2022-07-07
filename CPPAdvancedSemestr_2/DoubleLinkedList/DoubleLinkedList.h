#pragma once

#include <iostream>
#include <cstring>

using namespace std;

template <typename T>
struct Element
{
	T elem;
	Element *next = 0;
	Element *prev = 0;

public:
	Element(T sourceElem)
	{
		elem = sourceElem;
	}
	~Element() {}
};

template<>
struct Element<char*>
{
	char* elem;
	Element *next = 0;
	Element *prev = 0;

public:
	Element(char* sourceElem)
	{
		elem = new char[strlen(sourceElem) + 1];
		strcpy(elem, sourceElem);
	}
	~Element() { delete[] elem; }
};

template <class Type>
class DoubleLinkedList
{
protected:
	Element<Type> *First;
	Element<Type> *Last;
	size_t count;

public:
	DoubleLinkedList()
	{
		First = Last = nullptr;
		count = 0;
	}

	~DoubleLinkedList()
	{
		Element<Type> *current = First;
		while (current != nullptr)
		{
			Element<Type> *tmp = current;
			current = current->next;
			delete tmp;
		}
	}

	size_t GetSize() { return count; }
	int end() { return count; }
	int begin() { return 0; }

	void Add(Type var)
	{
		Element<Type> *elem = new Element<Type>(var);
		elem->next = nullptr;

		if (First == nullptr)
		{
			Last = First = elem;
			elem->prev = nullptr;
		}
		else
		{
			Last->next = elem;
			elem->prev = Last;
			Last = elem;
		}
		count++;
	}
	void Add(Element<Type> *current) { Add(current->elem); }

	void AddFirst(const Type var)
	{
		Element<Type> *elem = new Element<Type>(var);
		elem->prev = nullptr;

		if (First == nullptr)
		{
			Last = First = elem;
			elem->next = nullptr;
		}
		else
		{
			First->prev = elem;
			elem->next = First;
			First = elem;
		}
		count++;
	}

	bool Insert(unsigned index, const Type var)
	{
		Element<Type> *elem = new Element<Type>(var);

		if (First == nullptr)
		{
			Add(var);
			return true;
		}
		if (index == 0)
		{
			elem->next = First;
			elem->prev = nullptr;
			First = elem;
			count++;
			return true;
		}

		Element<Type> *current = First;
		Element<Type> *prev = nullptr;
		unsigned current_index = 0;
		while (current != nullptr)
		{
			if (current_index == index)
			{
				prev->next = elem;
				elem->prev = prev;
				elem->next = current;
				current->prev = elem;
				count++;
				return true;
			}
			prev = current;
			current = current->next;
			current_index++;
		}
		delete elem;
		return false;
	}

	void Print()
	{
		Element<Type> *current = First;
		while (current != nullptr)
		{
			cout << current->elem << " ";
			current = current->next;
		}
		cout << endl;
	}

	void PrintBack()
	{
		Element<Type> *current = Last;
		while (current != nullptr)
		{
			cout << current->elem << " ";
			current = current->prev;
		}
		cout << endl;
	}



	Element<Type> *operator[](int index)
	{
		unsigned current_index = 0;
		if (index < (count / 2))
		{
			Element<Type> *current = First;
			while (current != nullptr)
			{
				if (current_index == index)
					return current;
				current = current->next;
				current_index++;
			}
		}
		else
		{
			current_index = count - 1;
			Element<Type> *current = Last;
			while (current != nullptr)
			{
				if (current_index == index)
					return current;
				current = current->prev;
				current_index--;
			}
		}
	}

	bool operator==(DoubleLinkedList &source)
	{
		Element<Type> *curr = First;
		Element<Type> *currSource = source.First;

		if (count != source.count)
			return false;

		while (curr != nullptr)
		{
			if (curr->elem != currSource->elem)
				return false;
			currSource = currSource->next;
			curr = curr->next;
		}

		return true;
	}

	bool ReverseEquals(DoubleLinkedList &lsd)
	{
		if (count != lsd.count)
			return false;

		Element<Type> *curr = First;
		Element<Type> *currSource = lsd.Last;

		while (curr != nullptr)
		{
			if (*(curr->elem) != *(currSource->elem))
				return false;
			currSource = currSource->prev;
			curr = curr->next;
		}

		return true;
	}

	bool Contains(Type var)
	{
		Element<Type> *current = First;
		while (current != nullptr)
		{
			if (current->elem == var)
				return true;
			current = current->next;
		}
		return false;
	}

	int GetCount(Type var)
	{
		Element<Type> *current = First;
		int k = 0;
		while (current != nullptr)
		{
			if (current->elem == var)
				k++;
			current = current->next;
		}
		return k;
	}

	DoubleLinkedList &operator=(const DoubleLinkedList &source)
	{
		Element<Type> *current = First;
		while (current != nullptr)
		{
			Element<Type> *tmp = current;
			current = current->next;
			delete tmp;
		}

		Element<Type> *currentSource = source.First;
		First = Last = nullptr;
		count = 0;

		while (currentSource != nullptr)
		{
			Add(currentSource->elem);
			currentSource = currentSource->next;
		}
		Last = current;
		return *this;
	}

	bool Remove(unsigned idx)
	{
		if (count == 0)
			return false;

		if (idx == 0)
		{
			Element<Type> *tmp = First;
			First = First->next;
			First->prev = nullptr;
			delete tmp;
		}

		Element<Type> *current = First;
		int k = 0;
		while (current->next != nullptr)
		{
			if (k + 1 == idx)
			{
				Element<Type> *tmp = current->next;
				current->next = current->next->next;
				current->next->prev = current;
				delete tmp;
			}
			current = current->next;
			k++;
		}
		Last = current;
		count--;
		return true;
	}

	bool RemoveAll(Type var)
	{
		if (count == 0)
			return false;
		int k = 0;
		if (First->elem == var)
		{
			First = First->next;
			First->prev = nullptr;
		}
		if (Last->elem == var)
		{
			Last = Last->prev;
			Last->next = nullptr;
		}
		
		Element<Type> *current = First;

		while (current->next != nullptr)
		{
			if (current->next->elem == var)
			{
				Element<Type> *tmp = current->next;
				current->next = current->next->next;
				current->next->prev = current;
				delete tmp;
				count--;
			}
			current = current->next;
			k++;
		}
		Last = current;
		return true;
	}
	
	void Reverse()
    {
        Element<Type>* current_l = First;
        Element<Type>* current_r = Last;

        for (int i = 0; i<=count/2; i++)
        {
            swap(current_l->elem, current_r->elem);

            current_l = current_l->next;
            current_r = current_r->prev;
        }
    }

	void Clear()
	{
		First = nullptr;
		Last = nullptr;
		count = 0;
	}

	bool comp(Element<Type> *elem1, Element<Type> *elem2, int comp)
	{
		switch (comp)
		{
		case 1:
		{
			if (elem1->elem >= elem2->elem)
				return true;
			else
				return false;
			break;
		}

		case 0:
		{
			if (elem1->elem <= elem2->elem)
				return true;
			else
				return false;
			break;
		}
		}
	}

	void MergeSort(int first, int last, int comparator)
	{
		if (last == 0)
			return;
		if (last - first == 1)
			return;
		if (last - first == 2)
		{
			Element<Type> *current = (*this)[first];
			if (comp(current, current->next, comparator))
			{
				swap(current->elem, current->next->elem);
				return;
			}
			else
				return;
		}

		MergeSort(first, first + (last - first) / 2, comparator);
		MergeSort((first + (last - first) / 2), last, comparator);

		DoubleLinkedList<Type> tmp;

		int mid = first + (last - first) / 2;
		int begin1 = first;
		int begin2 = mid;

		Element<Type> *b1 = nullptr;
		Element<Type> *b2 = nullptr;
		Element<Type> *current = First;
		int idx = 0;
		while (current != nullptr)
		{
			if (idx == begin1)
				b1 = current;
			if (idx == begin2)
			{
				b2 = current;
				break;
			}
			idx++;
			current = current->next;
		}

		while (tmp.count < last - first)
		{
			if (begin1 >= mid || (begin2 < last && comp(b1, b2, comparator)))
			{
				tmp.Add(b2);
				begin2++;
				b2 = b2->next;
			}
			else
			{
				tmp.Add(b1);
				begin1++;
				b1 = b1->next;
			}
		}
		idx = 0;
		int i = first;
		Element<Type> *currentTmp = tmp.First;
		current = First;
		while (current != nullptr)
		{
			if (idx >= first && idx < last)
			{
				swap(current->elem, currentTmp->elem);
				currentTmp = currentTmp->next;
			}
			if (idx >= last)
				break;
			idx++;
			current = current->next;
		}
	}

	void BubbleSort()
	{
		for (int i = 0; i < count - 1; i++)
		{
			for (int j = 0; j < count - i - 1; j++)
			{
				Element<Type> *tmp = (*this)[j];
				if (tmp->elem > tmp->next->elem)
					swap(tmp->elem, tmp->next->elem);
			}
		}
	}
};

template<>
class DoubleLinkedList<char*>
{
	Element<char*> *First;
	Element<char*> *Last;
	size_t count;
public:
	DoubleLinkedList()
	{
		First = Last = nullptr;
		count = 0;
	}

	~DoubleLinkedList()
	{
		Element<char*> *current = First;
		while (current != nullptr)
		{
			Element<char*> *tmp = current;
			current = current->next;
			delete tmp;
		}
	}

	size_t GetSize() { return count; }
	int begin() { return 0; }
	int end() { return count; }

	void Clear()
	{
		First = Last = nullptr;
		count = 0;
	}

	void Add(char* var)
	{
		Element<char*> *elem = new Element<char*>(var);
		elem->next = nullptr;

		if (First == nullptr)
		{
			Last = First = elem;
			elem->prev = nullptr;
		}
		else
		{
			Last->next = elem;
			elem->prev = Last;
			Last = elem;
		}
		count++;
	}
	void Add(Element<char*> *current) { Add(current->elem); }

	void AddFirst(char* var)
	{
		Element<char*> *elem = new Element<char*>(var);
		elem->prev = nullptr;

		if (First == nullptr)
		{
			Last = First = elem;
			elem->next = nullptr;
		}
		else
		{
			First->prev = elem;
			elem->next = First;
			First = elem;
		}
		count++;
	}

	bool Insert(unsigned index, char* var)
	{
		Element<char*> *elem = new Element<char*>(var);

		if (First == nullptr)
		{
			Add(var);
			return true;
		}
		if (index == 0)
		{
			elem->next = First;
			elem->prev = nullptr;
			First = elem;
			count++;
			return true;
		}

		Element<char*> *current = First;
		Element<char*> *prev = nullptr;
		unsigned current_index = 0;
		while (current != nullptr)
		{
			if (current_index == index)
			{
				prev->next = elem;
				elem->prev = prev;
				elem->next = current;
				current->prev = elem;
				count++;
				return true;
			}
			prev = current;
			current = current->next;
			current_index++;
		}
		delete elem;
		return false;
	}

	void Print()
	{
		Element<char*> *current = First;
		while (current != nullptr)
		{
			cout << current->elem << " ";
			current = current->next;
		}
		cout << endl;
	}

	void PrintBack()
	{
		Element<char*> *current = Last;
		while (current != nullptr)
		{
			cout << current->elem << " ";
			current = current->prev;
		}
		cout << endl;
	}

	Element<char*> *operator[](int index)
	{
		unsigned current_index = 0;
		if (index < (count / 2))
		{
			Element<char*> *current = First;
			while (current != nullptr)
			{
				if (current_index == index)
					return current;
				current = current->next;
				current_index++;
			}
		}
		else
		{
			current_index = count - 1;
			Element<char*> *current = Last;
			while (current != nullptr)
			{
				if (current_index == index)
					return current;
				current = current->prev;
				current_index--;
			}
		}
	}


	bool operator==(DoubleLinkedList &source)
	{
		Element<char*> *curr = First;
		Element<char*> *currSource = source.First;

		if (count != source.count)
			return false;

		while (curr != nullptr)
		{
			if (strcmp(curr->elem, currSource->elem) != 0)
				return false;
			currSource = currSource->next;
			curr = curr->next;
		}

		return true;
	}

	bool ReverseEquals(DoubleLinkedList &lsd)
	{
		if (count != lsd.count)
			return false;

		Element<char*> *curr = First;
		Element<char*> *currSource = lsd.Last;

		while (curr != nullptr)
		{
			if (strcmp(curr->elem, currSource->elem) != 0)
				return false;
			currSource = currSource->prev;
			curr = curr->next;
		}

		return true;
	}

	bool Contains(char* var)
	{
		Element<char*> *current = First;
		while (current != nullptr)
		{
			if (strcmp(current->elem, var) == 0)
				return true;
			current = current->next;
		}
		return false;
	}

	int GetCount(char* var)
	{
		Element<char*> *current = First;
		int k = 0;
		while (current != nullptr)
		{
			if (strcmp(current->elem, var) == 0)
				k++;
			current = current->next;
		}
		return k;
	}

	bool Remove(unsigned idx)
	{
		if (count == 0)
			return false;

		if (idx == 0)
		{
			Element<char*> *tmp = First;
			First = First->next;
			First->prev = nullptr;
			delete tmp;
		}

		Element<char*> *current = First;
		int k = 0;
		while (current->next != nullptr)
		{
			if (k + 1 == idx)
			{
				Element<char*> *tmp = current->next;
				current->next = current->next->next;
				current->next->prev = current;
				delete tmp;
			}
			current = current->next;
			k++;
		}
		Last = current;
		count--;
		return true;
	}

	bool RemoveAll(char* var)
	{
		if (count == 0)
			return false;
		int k = 0;
		if (First->elem == var)
		{
			First = First->next;
			First->prev = nullptr;
		}
		if (Last->elem == var)
		{
			Last = Last->prev;
			Last->next = nullptr;
		}
		
		Element<char*> *current = First;

		while (current->next != nullptr)
		{
			if (strcmp(current->next->elem, var) == 0)
			{
				Element<char*> *tmp = current->next;
				current->next = current->next->next;
				current->next->prev = current;
				delete tmp;
				count--;
			}
			current = current->next;
			k++;
		}
		Last = current;
		return true;
	}
	
	void Reverse()
    {
        Element<char*>* current_l = First;
        Element<char*>* current_r = Last;

        for (int i = 0; i<=count/2; i++)
        {
            swap(current_l->elem, current_r->elem);

            current_l = current_l->next;
            current_r = current_r->prev;
        }
    }

	bool comp(Element<char*> *elem1, Element<char*> *elem2, int comp)
	{
		switch (comp)
		{
		case 1:
		{
			if (strcmp(elem1->elem, elem2->elem) >= 0)
				return true;
			else
				return false;
			break;
		}

		case 0:
		{
			if (strcmp(elem1->elem, elem2->elem) <= 0)
				return true;
			else
				return false;
			break;
		}
		}
	}

	void MergeSort(int first, int last, int comparator)
	{
		if (last == 0)
			return;
		if (last - first == 1)
			return;
		if (last - first == 2)
		{
			Element<char*> *current = (*this)[first];
			if (comp(current, current->next, comparator))
			{
				swap(current->elem, current->next->elem);
				return;
			}
			else
				return;
		}

		MergeSort(first, first + (last - first) / 2, comparator);
		MergeSort((first + (last - first) / 2), last, comparator);

		DoubleLinkedList<char*> tmp;

		int mid = first + (last - first) / 2;
		int begin1 = first;
		int begin2 = mid;

		Element<char*> *b1 = nullptr;
		Element<char*> *b2 = nullptr;
		Element<char*> *current = First;
		int idx = 0;
		while (current != nullptr)
		{
			if (idx == begin1)
				b1 = current;
			if (idx == begin2)
			{
				b2 = current;
				break;
			}
			idx++;
			current = current->next;
		}

		while (tmp.count < last - first)
		{
			if (begin1 >= mid || (begin2 < last && comp(b1, b2, comparator)))
			{
				tmp.Add(b2);
				begin2++;
				b2 = b2->next;
			}
			else
			{
				tmp.Add(b1);
				begin1++;
				b1 = b1->next;
			}
		}
		idx = 0;
		int i = first;
		Element<char*> *currentTmp = tmp.First;
		current = First;
		while (current != nullptr)
		{
			if (idx >= first && idx < last)
			{
				swap(current->elem, currentTmp->elem);
				currentTmp = currentTmp->next;
			}
			if (idx >= last)
				break;
			idx++;
			current = current->next;
		}
	}

	void BubbleSort()
	{
		for (int i = 0; i < count - 1; i++)
		{
			for (int j = 0; j < count - i - 1; j++)
			{
				Element<char*> *tmp = (*this)[j];
				if (strcmp(tmp->elem, tmp->next->elem) > 0)
					swap(tmp->elem, tmp->next->elem);
			}
		}
	}
};