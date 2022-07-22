#include <iostream>
#include "Graph.h"

using namespace std;

int main()
{
	Graph graph(100);
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
	cout << endl;
	graph.Print();

	return 0;
}