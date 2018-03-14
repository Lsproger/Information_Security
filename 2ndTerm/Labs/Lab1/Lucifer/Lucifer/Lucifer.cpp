// Lucifer.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Lucifer.h"
#include <iostream>
using namespace std;
int _tmain(int argc, _TCHAR* argv[])
{
	char msg[block_size] = {"Vladosadosadosa"};
	char key[key_size] = { "keykeykey"};
	Lucifer(msg, key, false);
	cout << msg<<endl;
	Lucifer(msg, key, true);
	cout << msg<<endl;
	}

