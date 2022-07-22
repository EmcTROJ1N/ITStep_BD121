#pragma once
#include <string>
#include <map>

using namespace std;

class Graph
{
	size_t MaxVertexCount;
	size_t CurrentVertexCount;

	// массив весов рёбер между вергинами графа
	double** Links;
	
	// словарь с названиями и номерами вершин
	map<string, unsigned>* Vertices;

	// словарь с номерами вершин и названиями
	map<unsigned, string>* IndexTitleVertices;
public:
	Graph(size_t maxVertexCount);
	Graph(Graph& source);
	~Graph();

	bool AddVertex(string title);
	void RenameVertex(string oldName, string newName);
	void DeleteVertex(string title);

	bool AddLink(string title1, string title2, double weight);
	bool RemoveLink(string title1, string title2);

	void PrintVertices();
	void PrintLinks(string title);
	void Print();
	
	void operator=(Graph& source);
	bool operator==(Graph& source);
};

