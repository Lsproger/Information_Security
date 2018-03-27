// GOST.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include <iostream>
#include "..\\Timer\\Timer.h"
typedef unsigned long u4;
typedef unsigned long byte;

typedef struct
{
	u4 k[8];
	/* Constant s-boxes*/
	char k87[256], k65[256], k43[256], k21[256];
}gost_ctx;

void gost_enc(gost_ctx *, u4 *, int);
void gost_dec(gost_ctx *, u4 *, int);
void gost_key(gost_ctx *, u4 *);
void gost_init(gost_ctx *);
void gost_destroy(gost_ctx *);

#ifdef __alpha
typedef unsigned int word32;
#else
typedef unsigned long word32;
#endif

void kboxinit(gost_ctx *c)
{
	int i;

	byte k8[16] =
	{
		14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6,
		12, 5, 9, 0, 7
	};

	byte k7[16] =
	{
		15, 1, 8, 14, 6, 11, 3, 4,
		9, 7, 2, 13, 12, 0, 5, 10
	};

	byte k6[16] =
	{
		10, 0, 9, 14, 6, 3 ,15, 5, 1, 13, 12,
		7, 11, 4, 2, 8
	};

	byte k5[16] =
	{
		7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8,
		5, 11, 12, 4, 15
	};

	byte k4[16] =
	{
		2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3,
		15, 13, 0, 14, 9
	};

	byte k3[16] =
	{
		12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3,
		4, 14, 7, 5,11
	};

	byte k2[16] =
	{
		4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9,
		7, 5, 10, 6, 1
	};

	byte k1[16] =
	{
		13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3,
		14, 5, 0, 12, 7
	};

	for (i = 0; i < 256; i++)
	{
		c->k87[i] = k8[i >> 4] << 4 | k7[i & 15];
		c->k65[i] = k6[i >> 4] << 4 | k5[i & 15];
		c->k43[i] = k4[i >> 4] << 4 | k3[i & 15];
		c->k21[i] = k2[i >> 4] << 4 | k1[i & 15];
	}
}

static word32 f(gost_ctx *c, word32 x)
{
	x =
		c->k87[x >> 24 & 255] << 24 |
		c->k65[x >> 16 & 255] << 16 |
		c->k43[x >> 8 & 255] << 8 |
		c->k21[x & 255];

	/*Rotate 11 left bits*/
	return x << 11 | x >> (32 - 11);
}

void gostcrypt(gost_ctx *c, word32 *d)
{
	register word32 n1, n2;

	n1 = d[0];
	n2 = d[1];


	n2 ^= f(c, n1 + c->k[0]); n1 ^= f(c, n2 + c->k[1]);
	n2 ^= f(c, n1 + c->k[2]); n1 ^= f(c, n2 + c->k[3]);
	n2 ^= f(c, n1 + c->k[4]); n1 ^= f(c, n2 + c->k[5]);
	n2 ^= f(c, n1 + c->k[6]); n1 ^= f(c, n2 + c->k[7]);

	n2 ^= f(c, n1 + c->k[0]); n1 ^= f(c, n2 + c->k[1]);
	n2 ^= f(c, n1 + c->k[2]); n1 ^= f(c, n2 + c->k[3]);
	n2 ^= f(c, n1 + c->k[4]); n1 ^= f(c, n2 + c->k[5]);
	n2 ^= f(c, n1 + c->k[6]); n1 ^= f(c, n2 + c->k[7]);

	n2 ^= f(c, n1 + c->k[0]); n1 ^= f(c, n2 + c->k[1]);
	n2 ^= f(c, n1 + c->k[2]); n1 ^= f(c, n2 + c->k[3]);
	n2 ^= f(c, n1 + c->k[4]); n1 ^= f(c, n2 + c->k[5]);
	n2 ^= f(c, n1 + c->k[6]); n1 ^= f(c, n2 + c->k[7]);


	n2 ^= f(c, n1 + c->k[7]); n1 ^= f(c, n2 + c->k[6]);
	n2 ^= f(c, n1 + c->k[5]); n1 ^= f(c, n2 + c->k[4]);
	n2 ^= f(c, n1 + c->k[3]); n1 ^= f(c, n2 + c->k[2]);
	n2 ^= f(c, n1 + c->k[1]); n1 ^= f(c, n2 + c->k[0]);

	d[0] = n2; d[1] = n1;
}

void gostdecrypt(gost_ctx *c, u4 *d)
{
	register word32 n1, n2;

	n1 = d[0]; n2 = d[1];

	n2 ^= f(c, n1 + c->k[0]); n1 ^= f(c, n2 + c->k[1]);
	n2 ^= f(c, n1 + c->k[2]); n1 ^= f(c, n2 + c->k[3]);
	n2 ^= f(c, n1 + c->k[4]); n1 ^= f(c, n2 + c->k[5]);
	n2 ^= f(c, n1 + c->k[6]); n1 ^= f(c, n2 + c->k[7]);


	n2 ^= f(c, n1 + c->k[7]); n1 ^= f(c, n2 + c->k[6]);
	n2 ^= f(c, n1 + c->k[5]); n1 ^= f(c, n2 + c->k[4]);
	n2 ^= f(c, n1 + c->k[3]); n1 ^= f(c, n2 + c->k[2]);
	n2 ^= f(c, n1 + c->k[1]); n1 ^= f(c, n2 + c->k[0]);

	n2 ^= f(c, n1 + c->k[7]); n1 ^= f(c, n2 + c->k[6]);
	n2 ^= f(c, n1 + c->k[5]); n1 ^= f(c, n2 + c->k[4]);
	n2 ^= f(c, n1 + c->k[3]); n1 ^= f(c, n2 + c->k[2]);
	n2 ^= f(c, n1 + c->k[1]); n1 ^= f(c, n2 + c->k[0]);

	n2 ^= f(c, n1 + c->k[7]); n1 ^= f(c, n2 + c->k[6]);
	n2 ^= f(c, n1 + c->k[5]); n1 ^= f(c, n2 + c->k[4]);
	n2 ^= f(c, n1 + c->k[3]); n1 ^= f(c, n2 + c->k[2]);
	n2 ^= f(c, n1 + c->k[1]); n1 ^= f(c, n2 + c->k[0]);

	d[0] = n2; d[1] = n1;
}

void gost_enc(gost_ctx *c, u4 *d, int blocks)
{
	int i;
	for (i = 0; i < blocks; i++)
	{
		gostcrypt(c, d);
		d += 2;
	}
}

void gost_dec(gost_ctx *c, u4 *d, int blocks)
{
	int i;
	for (i = 0; i < blocks; i++)
	{
		gostdecrypt(c, d);
		d += 2;
	}
}

void gost_key(gost_ctx *c, u4 *k)
{
	int i;
	for (i = 0; i < 8; i++)
	{
		c->k[i] = k[i];
	}
}

void gost_init(gost_ctx *c)
{
	kboxinit(c);
}

void gost_destroy(gost_ctx *c)
{
	int i;
	for (i = 0; i < 8; i++)
	{
		c->k[i] = 0;
	}
}


void main()
{
	Timer *t = new Timer();

	std::cout << "Input data size = 160 bytes" << "\n*****************************\n";

	u4 k[8], data[40];
	int i;
	gost_ctx gc;

	for (i = 0; i < 40; i++)
	{
		data[i] = i;
		std::cout << "data[" << i << "] = " << data[i] << "\n";
	}


	gost_init(&gc);
	for (i = 0; i < 8; i++)
		k[i] = i;

	gost_key(&gc, k);

	for (size_t i = 0; i < t->N; i++)
	{
		t->Start();

		gost_enc(&gc, data, 20);

		/*for (i = 0; i < 4; i += 1)
		{
			std::cout << "Block " << i << " = " << data[i] << "\n";
		}*/

		gost_dec(&gc, data, 20);

		t->Stop();
		/*std::cout << "\n";

		for (i = 0; i < 40; i += 1)
		{
			std::cout << "Block " << i << " = " << data[i] << "\n";
		}*/

		std::cout << "Elapsed time = " << t->Delta() << "\n";
	}
	std::cout << "Average time = " << t->AverageTime() << "\n";
	gost_destroy(&gc);

}

