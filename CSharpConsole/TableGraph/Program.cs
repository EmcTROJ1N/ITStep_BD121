﻿using System;

class Program
{
    static void Main(string[] args)
    {
        Graph<string, double> graph = new Graph<string, double>(100);

        graph.AddVertex("A");
        graph.AddVertex("B");
        graph.AddVertex("C");
        graph.AddVertex("D");
        graph.AddVertex("E");

        graph.AddLink("A", "B", 1.65);
        graph.AddLink("A", "C", 1.3);
        graph.AddLink("B", "D", 0.8);
        graph.AddLink("B", "C", 2.5);
        graph.RenameVertex("E", "EEE");

        graph.DeleteVertex("EEE");
        graph.AddVertex("X");
        graph.AddLink("X", "A", 1.0);
        
        graph.Print();

    }
}