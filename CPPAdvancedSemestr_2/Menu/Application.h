#pragma once

#include "Menu.h"
#include <list>
#include <string>

class Application
{
	Menu* menu = new Menu(this);

	list<string> lines;
public:
	Application();
	~Application();
	void Run();

	void Add_Pressed();
	void Show_Pressed();
	void SayHello_Pressed();
	void SayHi_Pressed();
	void SayOk();
	void Exit_Pressed();
};

