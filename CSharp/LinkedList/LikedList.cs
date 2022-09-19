using System.Runtime.InteropServices.ComTypes;
using System;


class Element
{
    public int Elem;
    public Element? next = null;
    public Element? prev = null;
    public Element (int sourceElem) { Elem = sourceElem; }
}

class LinkedList
{
    Element First;
    Element Last;
    int Count;

    public int _Count { get; }

    public LinkedList()
    {
        First = Last = null;
        Count = 0;
    }

    public void Add(int var)
    {
        Element elem = new Element(var);
        elem.next = null;
        
        if (First == null)
        {
            Last = First = elem;
        }
        else
        {
            Last.next = elem;
            elem.prev = Last;
            Last = elem;
        }
        Count++;
    }

    public bool Insert(int idx, int var)
    {
        Element elem = new Element(var);

        if (First == null)
        {
            Add(var);
            return true;
        }
        if (idx == 0)
        {
            elem.next = First;
            elem.prev = null;
            First = elem;
            Count++;
        }

        Element current = First;
        Element prev = null;
        int currIdx = 0;
        while (current != null)
        {
            if (currIdx == idx)
            {
                prev.next = elem;
                elem.prev = prev;
                elem.next = current;
                current.prev = elem;
                Count++;
                return true;
            }
            prev = current;
            current = current.next;
            currIdx++;
        }
        return false;
    }

    public void Print()
    {
        for (Element elem = First; elem != null; elem = elem.next)
            System.Console.Write($"{elem.Elem} ");
        System.Console.WriteLine();
    }

    public void PrintBack()
    {
        for (Element elem = Last; elem != null; elem = elem.prev)
            System.Console.Write($"{elem.Elem} ");
        System.Console.WriteLine();
    }

    public bool Remove (int idx)
    {
        if (Count == 0)
            return false;
        if (idx == 0)
        {
            Element tmp = First;
            First = First.next;
            First.prev = null;
        }

        Element current = First;
        int k = 0;
        while (current.next != null)
        {
            if (k + 1 == idx)
            {
                Element tmp = current.next;
                current.next = current.next.next;
                current.next.prev = current;
            }
            current = current.next;
            k++;
        }
        Last = current;
        Count--;
        return true;
    }

}