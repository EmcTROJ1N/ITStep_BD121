#include "Graph.h"
#include <map>
#include <iostream>
#include <algorithm>
#include <iterator>
#include <iomanip>

using namespace std;

Graph::Graph(size_t maxVertexCount)
{
	MaxVertexCount = maxVertexCount;
	CurrentVertexCount = 0;

	// словарь вершин графа (связь названия вершины и номером вершины в двумерном массиве)
	Vertices = new map<string, unsigned>;
	IndexTitleVertices = new map<unsigned, string>;

	// двумерный массив связей между вершинами
	Links = new double* [MaxVertexCount];
	for (size_t i = 0; i < MaxVertexCount; i++)
	{
		Links[i] = new double[MaxVertexCount];

		for (size_t k = 0; k < MaxVertexCount; k++)
			Links[i][k] = 0;
	}
}

Graph::Graph(Graph& source)
{
	MaxVertexCount = source.MaxVertexCount;
	CurrentVertexCount = source.CurrentVertexCount;

	Vertices = new map<string, unsigned>;
	IndexTitleVertices = new map<unsigned, string>;

	for (auto it = source.Vertices->begin(); it != source.Vertices->end(); it++)
		Vertices->insert(make_pair(it->first, it->second));
	for (auto it = source.IndexTitleVertices->begin(); it != source.IndexTitleVertices->end(); it++)
		IndexTitleVertices->insert(make_pair(it->first, it->second));

	Links = new double* [MaxVertexCount];
	for (size_t i = 0; i < MaxVertexCount; i++)
	{
		Links[i] = new double[MaxVertexCount];

		for (size_t k = 0; k < MaxVertexCount; k++)
			Links[i][k] = source.Links[i][k];
	}
}

Graph::~Graph()
{
	delete Vertices;
	delete IndexTitleVertices;

	for (int i = 0; i < MaxVertexCount; i++)
		delete[] Links[i];
	delete[] Links;
}

bool Graph::AddVertex(string title)
{
	if (Vertices->find(title) == Vertices->end())
	{
		Vertices->insert(make_pair(title, CurrentVertexCount));
		IndexTitleVertices->insert(make_pair(CurrentVertexCount++, title));
		return true;
	}
	else
	{
		cout << "The vertex with name: '" << title << "' already exists!!!" << endl;
		return false;
	}
}

void Graph::PrintVertices()
{
	for (auto pos = Vertices->begin(); pos != Vertices->end(); pos++)
		cout << pos->first << " -> " << pos->second << endl;
}

bool Graph::AddLink(string title1, string title2, double weight)
{
	if (Vertices->find(title1) == Vertices->end() || Vertices->find(title2) == Vertices->end())
	{
		cout << "Wrong vertex name" << endl;
		return false;
	}

	// получить по названию вершины её индекс в массиве рёбер
	unsigned frstVertIdx = Vertices->find(title1)->second;
	unsigned secVertIdx = Vertices->find(title2)->second;

	Links[frstVertIdx][secVertIdx] = weight;
	Links[secVertIdx][frstVertIdx] = weight;
	
	return true;
}

bool Graph::RemoveLink(string title1, string title2)
{
	// если вершины с такими именами в графе отсутсвуют
	if (Vertices->find(title1) == Vertices->end() || Vertices->find(title2) == Vertices->end())
	{
		cout << "Wrong vertex name" << endl;
		return false;
	}

	// получить по названию вершины её индекс в массиве рёбер
	unsigned frstVertIdx = Vertices->find(title1)->second;

	// получить по названию вершины её индекс в массиве рёбер
	unsigned secVertIdx = Vertices->find(title2)->second;

	Links[frstVertIdx][secVertIdx] = 0;
	Links[secVertIdx][frstVertIdx] = 0;

	return true;
}

void Graph::PrintLinks(string title)
{
	if (Vertices->find(title) != Vertices->end())
	{
		cout << title << " links: ";

		// получить по названию вершины её индекс в массиве рёбер
		unsigned vertIdx = Vertices->find(title)->second;

		bool isFirstVertex = false;
		for (size_t i = 0; i < CurrentVertexCount; i++)
		{
			if (Links[vertIdx][i] != 0)
			{
				if (isFirstVertex == false)
				{
					cout << IndexTitleVertices->find(i)->second << "(" << Links[vertIdx][i] << ")";
					isFirstVertex = true;
				}
				else
					cout << ", " << IndexTitleVertices->find(i)->second << "(" << Links[vertIdx][i] << ")";
			}
		}
		cout << endl;
	}
}

void Graph::Print()
{
	int indent = 7;
	cout << setw(indent) << "X" << setw(indent);

	for (auto it = Vertices->begin(); it != Vertices->end(); it++)
		cout << it->first << setw(indent);
	cout << endl;

	for (int i = 0; i < CurrentVertexCount; i++)
	{
		cout << IndexTitleVertices->find(i)->second << setw(indent);
		for (int j = 0; j < CurrentVertexCount; j++)
			cout << Links[i][j] << setw(indent);
		cout << endl;
	}
}

void Graph::operator=(Graph& source)
{
	delete Vertices;
	delete IndexTitleVertices;
	Vertices = new map<string, unsigned>;
	IndexTitleVertices = new map<unsigned, string>;
	
	for (auto it = source.Vertices->begin(); it != source.Vertices->end(); it++)
		Vertices->insert(make_pair(it->first, it->second));
	for (auto it = source.IndexTitleVertices->begin(); it != source.IndexTitleVertices->end(); it++)
		IndexTitleVertices->insert(make_pair(it->first, it->second));

	if (MaxVertexCount != source.MaxVertexCount)
	{
		for (int i = 0; i < MaxVertexCount; i++)
			delete[] Links[i];
		delete[] Links;
		Links = new double*[source.MaxVertexCount];
		for (int i = 0; i < source.MaxVertexCount; i++)
			Links[i] = new double[source.MaxVertexCount];
		MaxVertexCount = source.MaxVertexCount;
	}
	for (size_t i = 0; i < MaxVertexCount; i++)
	{
		for (size_t k = 0; k < MaxVertexCount; k++)
			Links[i][k] = source.Links[i][k];
	}
	CurrentVertexCount = source.CurrentVertexCount;
}

bool Graph::operator==(Graph& source)
{
	if (CurrentVertexCount != source.CurrentVertexCount)
		return false;

	auto sourceIt = source.Vertices->begin();
	auto curentIt = Vertices->begin();
	for (; sourceIt != source.Vertices->end(); sourceIt++, curentIt++)
	{
		if (sourceIt->first != curentIt->first || sourceIt->second != curentIt->second)
			return false;
	}
	
	auto sourceItIdx = source.IndexTitleVertices->begin();
	auto curentItIdx = IndexTitleVertices->begin();
	for (; sourceItIdx != source.IndexTitleVertices->end(); sourceItIdx++, curentItIdx++)
	{
		if (sourceItIdx->first != curentItIdx->first || sourceItIdx->second != curentItIdx->second)
			return false;
	}

	for (int i = 0; i < CurrentVertexCount; i++)
	{
		for (int j = 0; j < CurrentVertexCount; j++)
		{
			if (Links[i][j] != source.Links[i][j])
				return false;
		}
	}

	return true;
}

void Graph::RenameVertex(string oldName, string newName)
{
	int vertIdx = Vertices->find(oldName)->second;
	Vertices->erase(oldName);
	Vertices->insert(make_pair(newName, vertIdx));

	IndexTitleVertices->erase(vertIdx);
	IndexTitleVertices->insert(make_pair(vertIdx, newName));
}

void Graph::DeleteVertex(string title)
{
	if (Vertices->find(title) == Vertices->end() || Vertices->find(title) == Vertices->end())
	{
		cout << "Wrong vertex name" << endl;
		return;
	}
	unsigned idx = Vertices->find(title)->second;

	for (int i = 0; i < CurrentVertexCount; i++)
		Links[i][idx] = 0;
	for (int i = 0; i < CurrentVertexCount; i++)
		Links[idx][i] = 0;

	Vertices->erase(title);
	IndexTitleVertices->erase(idx);

	auto vertIt = Vertices->begin();
	auto idxIt = IndexTitleVertices->begin();
	for (int i = 0; vertIt != Vertices->end(); vertIt++, idxIt++, i++)
	{
		vertIt->second = i;

		string tmp = IndexTitleVertices->find(idxIt->first)->second;
		IndexTitleVertices->erase(idxIt->first);
		IndexTitleVertices->insert(make_pair(i, tmp));
	}
	CurrentVertexCount--;
}