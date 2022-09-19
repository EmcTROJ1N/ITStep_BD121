using System;
using static Graph;

namespace CSharp;

class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph("Test graph");

        graph.AddVertex("A", 10);
        graph.AddVertex("B", 20);
        graph.AddVertex("C", 30);
        graph.AddVertex("D", 25);
        graph.AddVertex("E", 21);

        graph.AddLink("A", "B", "link1", 12);
        graph.AddLink("A", "D", "link2", 23);
        graph.AddLink("B", "D", "link3", 2);
        graph.AddLink("B", "C", "link4", 21);
        graph.AddLink("C", "E", "link4", 21);
        graph.AddLink("D", "E", "link4", 21);

        graph.Print();

        // graph.AddVertex("One", 34);
        // graph.AddVertex("Two", 45);
        // graph.AddVertex("Three", 23);
        // graph.AddVertex("Four", 78);
        // graph.AddVertex("Five", 11);
        // graph.AddVertex("Six", 21);

        // graph.AddLink("One", "Two", "", 7);
        // graph.AddLink("One", "Three", "", 9);
        // graph.AddLink("One", "Six", "", 14);
        // graph.AddLink("Two", "Three", "", 10);
        // graph.AddLink("Two", "Four", "", 15);
        // graph.AddLink("Three", "Four", "", 11);
        // graph.AddLink("Three", "Six", "", 2);
        // graph.AddLink("Four", "Five", "", 6);
        // graph.AddLink("Five", "Six", "", 9);
    }
}