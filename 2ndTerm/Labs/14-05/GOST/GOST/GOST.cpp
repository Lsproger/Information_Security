#include "stdafx.h"
#include "cgost.h"
/*Эти определения для основного алгоритма и генератора алгоритма*/
#define C1   0x1010101
#define C2   0x1010104
#define m32  0x4000000000000
#define m321 0xFFFFFFFF
/* переменная генератора СЧ*/
static UINT_32 iran;
/**/
const BLOCK Sb =   //таблица замен - просто константные значения
{
	{ 4, 14,  5,  7,  6,  4, 13,  1 },
	{ 10, 11,  8, 13, 12, 11, 11, 15 },
	{ 9,  4,  1, 10,  7, 10,  4, 13 },
	{ 2, 12, 13,  1,  1,  0,  1,  0 },
	{ 13,  6, 10,  0,  5,  7,  3,  5 },
	{ 8, 13,  3,  8, 15,  2, 15,  7 },
	{ 0, 15,  4,  9, 13,  1,  5, 10 },
	{ 14, 10,  2, 15,  8, 13,  9,  4 },
	{ 6,  2, 14, 14,  4,  3,  0,  9 },
	{ 11,  3, 15,  4, 10,  6, 10,  2 },
	{ 1,  8, 12,  6,  9,  8, 14,  3 },
	{ 12,  1,  7, 12, 14,  5,  7, 14 },
	{ 7,  0,  6, 11,  0,  9,  6,  6 },
	{ 15,  7,  0,  2,  3, 12,  8, 11 },
	{ 5,  5,  9,  5, 11, 15,  2,  8 },
	{ 3,  9, 11,  3,  2, 14, 12, 12 }
};
const Key32 K32 = //ключ - 256 бит - просто константные значения
{
	0x10CBC8CD,
	0x2FC0CFC5,
	0x34C0CCC1,
	0x92CEC2C8,
	0x81CCD8C3,
	0x705DCDC8,
	0x64C8D8C2,
	0x5ACECBDF

};

/*=============== НАЧАЛО АЛГОРИТМА ШИФВРОВАНИЯ ================================*/
/*============================================================================*/
/*************** Конструктор по умолчанию ********************************/
TGost::TGost(void)
{
	type = fgame_os; // по умолчанию, кодируем гаммированием с обратной связью
	mode = b256;     // по умолчанию, кодируем полным ключем - 256 бит
	SetBlock(Sb);    // инициализируем таблицу замен
	icount_iter = 7; // количество итераций внутреннего цикла = 7
	SetKey(K32);     // инициализация ключа
	time_t t = 0;
	time(&t);
	Seed(t);  // инициализация генератора СЧ (просто берём время)
}
/*************** Деструктор ***********************************************/
TGost::~TGost(void)
{
}
/*************** левый циклический сдвиг Х на n битов *********************/
UINT_32 TGost::ROL(UINT_32 X, BYTE n)
{
	_asm {
		mov  eax, X
		mov  cl, n
		rol  eax, cl
		mov  X, eax
	}

	return UINT_32(X);
}
/********************* простой обмен 2 х 32 = 64 бита **************************/
UINT_64 TGost::SWAP32(UINT_32 N1, UINT_32 N2)
{
	UINT_64 N;
	N = N1;
	N = (N << 32) | N2;
	return UINT_64(N);
}
/********************* простой обмен 64 бита*************************************/
UINT_64 TGost::SWAP64(UINT_64 X)
{
	UINT_64 N;
	UINT_32 N1, N2;
	N1 = UINT_32(X);     // младшая
	N2 = X >> 32;         // старшая
	N = N1;
	N = (N << 32) | N2;
	return N;
}
/*********************** гениратор СВ ******************************************/
UINT_64 TGost::RPGCH(UINT_64 N)
{
	UINT_32 N1;
	UINT_32 N2;
	N1 = UINT_32(N);//младшая
	N2 = N >> 32;  //старшая
	N1 = (N1 + C1) % 0x100000000; //(x1 + c1)mod 2^32     { c1 = 1010101}
	N2 = (N2 + C2) % 0xFFFFFFFF;  //(x2 + c2)mod (2^32-1) { c2 = 1010104}
	N = N2;
	N = (N << 32) | N1;
	return N;
}

/*****************  Подстановка таблицы замены - блочная замена ****************/
UINT_32 TGost::ReplaceBlock(UINT_32 x)
{
	int i;
	UINT_32 res = 0UL;
	for (i = 7; i >= 0; i--)
	{
		ui4_0 = x >> (i * 4);
		ui4_0 = BS[ui4_0][i];
		res = (res << 4) | ui4_0;
	}
	return res;
}
/* ОСНОВНОЙ АЛГОРИТМ КОДИРОВАНИЯ ГОСТ 28147-89 ********************************/
UINT_64 TGost::BaseKod(UINT_64 N, UINT_32 X)
{
	UINT_32 N1, N2, S = 0UL;    //Результат операций с младшей частью
	N1 = UINT_32(N);     // младшая
	N2 = N >> 32;         // старшая
	S = N1 + X % 0x4000000000000;//Сложение по модулю 2^32
	S = ReplaceBlock(S);//Блочная замена - по 4 бита
	S = ROL(S, 11);    //Циклический сдвиг на 11 бит в лево
	_asm {             // исключающее ИЛИ ( S = S xor N2)
		mov eax, N2
		xor S, eax
	}
	// Меняем 32-е блоки местами
	N2 = N1;
	N1 = S; // результат кодирования
	return SWAP32(N2, N1);// меняем местами старшую на младшую часть  -> N_64
}
/*---   ШИФРОВАНИЕ по ГОСТ 28147-89 -----------------------------------*/
UINT_64 TGost::CodeGost(UINT_64 N)
{
	int i, j;
	// первый цикл - 24 итерации (3 х icount_iter)
	for (i = 0; i<3; i++)
	{
		for (j = 0; j <= icount_iter; j++)
		{
			N = BaseKod(N, K[j]);
		}
	}
	// второй цикл - icount_iter итерации
	for (i = icount_iter; i >= 0; i--)
	{
		N = BaseKod(N, K[i]);
	}
	// всего было 32 итерации - 32/8 = 4 - раза отработал кажый ключ.
	return SWAP64(N);// поменяли местами 32-х разрядные блоки, вернули 64 бита.
}
/*-- РАСШИФРОВАНИЕ по ГОСТ 28147-89 -------------------------------------*/
UINT_64 TGost::DeCodeGost(UINT_64 N)
{
	int i, j;
	for (i = 0; i <= icount_iter; i++)
	{
		N = BaseKod(N, K[i]);
	}

	for (i = 0; i<3; i++)
	{
		for (int j = icount_iter; j >= 0; j--)
		{
			N = BaseKod(N, K[j]);
		}
	}

	return SWAP64(N);
}
/*-----кодирование с гаммирование по ГОСТ - работает с 64-и разрядн. блоками -------------*/
UINT_64 TGost::GostGUM(UINT_64 N, UINT_64 &X)
{
	X = RPGCH(X);
	X = CodeGost(X);
	N = N^X;
	return N;
}
/*---------------- Кодирование по ГОСТ с гаммированием с обратной связъю ------------*/
UINT_64 TGost::GostGamOS(UINT_64 N, UINT_64&X, bool kode)
{
	UINT_64 T;
	T = N;
	N = N^CodeGost(X);
	X = (kode) ? N : T;
	return N;
}
/*-------------------- Возвращает имитовставку -----------------------------------*/
UINT_64 TGost::GetImito(UINT_64 X)
{
	// Выработка имитовставки.То же самое что и хеш функция.
	BYTE j;
	UINT_32 A;
	N1 = UINT_32(X);
	N2 = X >> 32;
	for (j = 1; j <= 12; j++)
	{
		A = N2^ReplaceBlock(N1) % 0x1000000000;
		N2 = N1;
		N1 = A;
	}
	return (N2 << 32) | N1;
}

/*шифрование по основному алгоритму*/
UINT_64 TGost::KriptData(UINT_64 N, bool kode)
{
	if (kode)
		N = CodeGost(N);
	else
		N = DeCodeGost(N);
	return UINT_64(N);
}
/*---------ОСНОВНОЙ АЛГОРИТМ пользователя- кодирование файла
fname_in и fname_out, имена входного и выходного файла
kript - флаг, указывающий на шифрование(true) или разшифрование (false)---------*/
UINT_32 TGost::KodeFile(const char *fname_in, const char* fname_out, bool kript)
{
	FILE *FileIn, *FileOut;
	UINT_32 nitem = 0;
	long count, fsize, readf;
	UINT_64 X = 0UL, *N;// = 0UL;
	X = SK; // получае синхропосылку
	if ((FileIn = fopen(fname_in, "rb")) == NULL)
	{
		return 0;
	}

	if ((FileOut = fopen(fname_out, "wb+")) == NULL)
	{
		fclose(FileIn);
		return 0;
	}
	if ((fsize = GetSizeFile(fname_in)) == 0)//получаем размер файла
	{
		fclose(FileIn);
		fclose(FileOut);
		return 0;
	}
	nitem = fsize / 8 + (fsize % 8 >0); // устанавливаем число 64 бит элементов
	N = (UINT_64*)calloc(nitem, sizeof(UINT_64)); // выделеяем под них память
	readf = fread(N, sizeof(UINT_64), nitem, FileIn); // читаем весь файл, недостаток - нули в 64 бит чисе.
	if (readf>0)
	{
		for (UINT_32 i = 0; i < nitem; i++)
		{
			N[i] = Kode(N[i], X, kript);// кодируем
			count += fwrite(&N[i], sizeof(UINT_64), 1, FileOut);//пишем по 64 бита в файл
		}
	}
L:  free(N);
	fclose(FileIn);
	fclose(FileOut);
	return count * 8;
}

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
/*-----  внутренняя функция, вызываеися из KodeFile------------------------------*/
UINT_64 TGost::Kode(UINT_64 N, UINT_64 &X, bool kript)
{
	UINT_64 RES = 0UL;
	switch (type)// взависимости от метода шифрования
	{
	case fbase:RES = (kript) ? CodeGost(N) : DeCodeGost(N); break;
	case fgame:RES = GostGUM(N, X); break;
	case fgame_os:RES = GostGamOS(N, X, kript); break;
	}

	return RES; //зашифровываный блок

}
/*=============== КОНЕЦ АЛГОРИТМА ШИФВРОВАНИЯ ================================*/
/*============ дальше вспомогательные функции класса =========================*/
/* устанавливаем синхропосылку ***********************************************/
/******************** возвращяет число - синхропосылку *************************/
UINT_64 TGost::GetSinhro()
{
	UINT_64 r = _rand32();
	r = (r << 32) | _rand32();
	return r;
}
// устанавливает синхропосылку -----------------------------------------------
UINT_64 TGost::SetSinhro(UINT_64 X)
{
	SK = X;
	return UINT_64(SK);
}

/*************** Установка ключа 8 х 8 = 64 = 2 х 32 бита ****************/
bool TGost::SetKey(char* KeyCh)
{
	int i = 1;
	if (strlen(KeyCh)<8)
		return false;
	K[0] = 0UL; K[1] = 0UL;
	K[0] = (BYTE)KeyCh[0];
	for (; i < 4; i++)
		K[0] = (K[0] << 8) | (BYTE)KeyCh[i];
	K[1] = (BYTE)KeyCh[i++];
	for (; i < 8; i++)
		K[1] = (K[1] << 8) | (BYTE)KeyCh[i];
	return true;
}
/*   Получение ключа **********************************************************/
UINT_32* TGost::GetKey(Key32 &key)
{
	int i;
	for (i = 0; i<8; i++)
		key[i] = K[i];
	return K;
}
/*--------Очистка данных алгоритма ---------------------------*/
void TGost::Clear(void)
{
	int i, j;
	SK = 0;
	for (i = 0; i<8; i++)
		K[i] = 0;

	for (i = 0; i<16; i++)
	{
		for (j = 0; j<8; j++)
			BS[i][j] = 0;
	}

}

/*--------- Инициализация алгоритма --------------------------*/
void TGost::InitGost(UINT_64 S, UINT_32 k32[], BLOCK tbl, TYPEKOD tk, MODEKOD mk)
{
	SetKey(k32);  // установка ключа
	SetBlock(tbl);// установка таблицы
	SetType(tk);  // тип алгоритма
	SetMode(mk);    // режим шифрования
	SK = S;     // синхропосылка
}
/*--------- Инициализация алгоритма --------------------------*/
void TGost::InitGost(GOSTPAR gp)
{
	SetKey(gp.K);      // установка ключа
	SetBlock(gp.BS);   // установка таблицы
	SetType(gp.type);  // тип алгоритма
	SetMode(gp.mode);  // режим шифрования
	SK = gp.SP;     // синхропосылка
}
/*--------- Инициализация алгоритма --------------------------*/
void TGost::InitGost()
{
	param.GetKey(K);   // установка ключа
	param.GetTable(BS);// установка таблицы
	SetType(param.type);  // тип алгоритма
	SetMode(param.mode);  // режим шифрования
	SK = param.SP;     // синхропосылка
}
/* Установка ключа - внешний ключ -> ключ класа*/
void TGost::SetKey(const UINT_32 a[])
{
	for (int i = 0; i<8; i++)
		K[i] = a[i];
}

/*------ Установка таблицы замен -------------------------------*/
void TGost::SetBlock(const BLOCK b)
{
	int i, j;
	for (i = 0; i<16; i++)
	{
		for (j = 0; j<8; j++)
			BS[i][j] = b[i][j];
	}
}
/*------ установка режима----------------------------*/

void TGost::SetMode(MODEKOD mk)
{
	mode = mk;
	icount_iter = (mode == b256) ? 7 : 1;
}
/*----- установка типа шифрования-------------------------------*/
void TGost::SetType(TYPEKOD tk)
{
	type = tk;
}
/*------получить режим ---------------------------------*/
MODEKOD TGost::GetMode()
{
	return mode;
}
/*---- получить тип шифрования -----------------------------------*/
TYPEKOD TGost::GetType()
{
	return type;
}

/*-------------------------------------------------------------*/
UINT_32 TGost::GetRand32()
{
	return _rand32();
}
/*-------------------------------------------------------------*/
UINT_64 TGost::GetRand64()
{
	UINT_64 x = 0UL;
	x = _rand32();
	x = (x << 32) | _rand32();
	return x;
}

/*-------------------------------------------------------------*/
UINT_32 TGost::Rand32Char()
{
	int i = 1;
	char ch = 0;
	UINT_32 x = 0Ul;
	do {
		ch = _rand32() >> 24;
		if (isalnum(ch))
			x = ch;
	} while (x == 0);
	while (i<4)
	{
		ch = _rand32() >> 24;
		if (isalnum(ch))
		{
			x = (x << 8) | ch;
			i++;
		}
	}
	return x;
}

/*-------------------------------------------------------------*/
void TGost::InitTable(BLOCK & bl)
{
	int i, j = 0;
	BYTE b[16];

	for (i = 0; i<8; i++)
	{
		memset(b, 100, 16);
		do
		{
			ui4_0 = _rand32() >> 16;
			if (IsExistsBlock(b, ui4_0))continue;
			b[j] = ui4_0;
			bl[j++][i] = ui4_0;
		} while (j<16);
		j = 0;
	}
}

// Возвращает таблицу заен
BLOCK* TGost::GetTable(BLOCK & bl)
{
	int i, j;
	for (i = 0; i<16; i++)
		for (j = 0; j<8; j++)
			bl[i][j] = BS[i][j];
	return &BS;
}
/*-------------------------------------------------------------*/
void TGost::InitKey(Key32 & k)
{
	for (int i = 0; i<8; i++)
		k[i] = _rand32();
}
/*-------------------------------------------------------------*/
bool TGost::IsExistsBlock(BYTE* b, BYTE ch)
{
	int i;
	for (i = 0; i<16; i++)
		if (b[i] == ch) return true;
	return false;
}

/*        Сохранение параметров шифрования      ---------------------------------------*/
bool TGost::SaveParametr(LPCSTR fname, void *data)
{
	FILE * f;
	bool ret = false;
	LPGOSTPAR p = (LPGOSTPAR)data;
	if (p == NULL)
		p = GetParamter();
	if ((f = fopen(fname, "wb+")) == NULL)
	{
		return ret;
	}

	ret = fwrite(p, sizeof(GOSTPAR), 1, f);
	fclose(f);
	return ret;
}

/*        Загрузка пареметров из файла          ---------------------------------------*/
bool TGost::LoadParametr(LPCSTR fname, void *data)
{
	FILE * f;
	bool ret = false;
	LPGOSTPAR p = (LPGOSTPAR)data;
	if (p == NULL)
		p = &param;
	if ((f = fopen(fname, "rb")) == NULL)
	{
		return ret;
	}

	ret = fread(p, sizeof(GOSTPAR), 1, f);
	fclose(f);
	return ret;
}
/* Возвращает параметры */
LPGOSTPAR TGost::GetParamter()
{
	int i, j;
	param.SetKey(K);
	param.SetTable(BS);
	param.SP = SK;
	param.type = type;
	param.mode = mode;
	return &param;
}

/* Устанавливает параметры */
void TGost::SetParamter(LPGOSTPAR par)
{
	param = *par;
	InitGost(param);
}
//************ ФУНКЦИИ ************************************************
/*------------преобразование числа (32 бита) в двойчный формат --------------------------*/
LPSTR Int32ToBin(UINT_32 X)
{
	int  i;
	char s[64];

	for (i = 0; i<32; i++)
	{
		if (((X << (i - 1)) >> 31) == 0)
			s[i] = '0';
		else
			s[i] = '1';
	}

	return (LPSTR)s;
}
/*------------преобразование числа (64 бита) в двойчный формат --------------------------*/
LPSTR Int64ToBin(UINT_64 X)
{
	int  i;
	char s[64];

	for (i = 0; i<64; i++)
	{
		if (((X << (i - 1)) >> 63) == 0)
			s[i] = '0';
		else
			s[i] = '1';
	}
	return s;

}

/*-------------------------------------------------------------*/
LPSTR Int32ToHex(LPSTR s, UINT_32 Value)
{
	memset(s, 0, 10);
	sprintf(s, "%LX", Value);
	return (LPSTR)s;
}

/*-------------------------------------------------------------*/
LPSTR Int64ToHex(LPSTR s, UINT_64 Value)
{
	sprintf(s, "%LX", Value);
	return (LPSTR)s;
}
/*-------- получаем размер файла в байтах (не пользуемся windows.h)----------------------------*/
UINT_32 GetSizeFile(const char* fname)
{
	FILE* f;
	UINT_32 fsize = 0UL;
	if ((f = fopen(fname, "rb")) == NULL)
		return 0;
	fseek(f, 0, SEEK_END);
	fsize = ftell(f);
	fseek(f, 0, SEEK_SET);
	fclose(f);
	return fsize;
}
/*-------------------- Преоразует строку в UINT64 поблочно - 8 бит ----------------*/
UINT_64 StrToInt64(LPCBYTE text)
{
	UINT_64 N = 0UL;
	BYTE ch;
	for (int i = 0; i<7; i++)
	{
		if (text[i] == 0)
		{
			N = N << (i * 8);
			return N;
		}
		ch = text[i];
		N = (N | ch);
		N = N << 8;
	}
	ch = text[7];
	N = (N | ch);
	return N;
}
/*---------64 бит число в строку - по байтно -------------------------*/
LPCSTR Int64ToStr(UINT_64 N)
{
	int i = 7;
	char buf[9] = { 0 };
	for (; i >= 0; i--)
		buf[i] = (N >> (8 * i));

	return (LPCSTR)buf;
}
/*-------------------------------------------------------------*/
LPCSTR Int32ToStr(LPSTR s, UINT_32 x)
{
	memset(s, 0, 5);
	for (int i = 3; i >= 0; i--)
		s[i] = ((x << (8 * i)) >> 24);
	return (LPCSTR)s;
}
/* инициализация генератор СЧ*/
void Seed(UINT_32 dum)
{
	iran = dum;
}
/* Генератор СЧ  - 32 бита без знака --------------*/
UINT_32 _rand32(void)
{
	iran = 1664525L * iran + 1013904223L;
	return(iran);
}
/*  Сохранение данных в файле - бинарный режим*/
bool SaveDataFile(LPCSTR fname, void *data, size_t size)
{
	FILE * f;
	bool ret = false;
	if ((f = fopen(fname, "wb+")) == NULL)
	{
		return ret;
	}

	ret = fwrite(data, size, 1, f);
	return ret;
}
/*  Чтение данных из файла - бинарный режим*/
bool LoadDataFile(LPCSTR fname, void *data, size_t size)
{
	FILE * f;
	bool ret = false;
	if ((f = fopen(fname, "rb")) == NULL)
	{
		return ret;
	}

	ret = fread(data, size, 1, f);
	fclose(f);
	return ret;
}