// Lucifer.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Lucifer.h"
#include <iostream>
#include "..\\..\\Timer\\Timer.h"

using namespace std;
int _tmain(int argc, _TCHAR* argv[])
{
	std::cout << "Input data size = 160 bytes" << "\n*****************************\n";
	char msg[block_size] = {"testwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcodetestwordforcode"};
	char key[key_size] = { "1234567890abcde"};
	Timer *t = new Timer();
	t->Start();
	Lucifer(msg, key, false);
	cout << msg<<endl;
	Lucifer(msg, key, true);
	cout << msg<<endl;
	t->Stop();

	cout << "Elapsed time = " << t->Delta() << "\n";
	}