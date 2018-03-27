#include "stdafx.h"
#include<iostream>
#include <time.h>
#include "IDEA.h"
#include "..\\..\\Timer\\Timer.h"
using namespace std;
#pragma warning(2:4235)
#define SIZE 128

/*
обработка сдвига влево: новый индекс + сдвиг*номер сдвига % размера = старый индекс
корреляция = 1/N SUM( (2xi-1)(2yi-1) ) ~ 0
*/


void main(int argc, char* argv[]) {
	std::cout << "Input data size = 160 bytes" << "\n*****************************\n";
	IDEA idea;
	srand(time(NULL));
	int mas[SIZE];//наш ключ

	for (int i = 0; i < SIZE; i++) 
	{
		mas[i] = rand() % 2;
	}
	
	cout << "\nkey: ";
	for (int i = 0; i < SIZE; i++) {
		cout << mas[i];
		if (i % 4 == 3) cout << " ";
	}
	cout << endl;

	Timer *t = new Timer();
	t->Start();
	idea.coding(argv[1], argv[2], mas);

	idea.decoding(argv[2], argv[3], mas);
	t->Stop();

	cout << "Elapsed time = " << t->Delta() << "\n";

	//	system("PAUSE");
}

