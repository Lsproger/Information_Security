#pragma once
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <string.h>
#include <limits.h>
#include <ctype.h>
#ifndef _RWSTD_NO_NAMESPACE
using namespace std;
#endif


#pragma once
/*=========================================================*/
/*                       ТИПЫ, ИСПОЛЬЗУЕМЫЕ В АЛГОРИТМЕ   */
/* некоторые типы, соответсвуют стандартным Windows, это переопределение
связано с тем, что, класс должен быть переносимым м/у платформами
в UNIX/Linux придётся кое что подправить, но это скорее относится к
синтаксису, после повторного теста в UNIX, сее дело придет в законченный вид,
пока, это работает устойчиво под Win32. Именно поэтому, основным компилятором
выбран rомпилятор от Borland. И, использовались "чистые" конструкции C/C++,
не пребегая к услугам конкретной платформы, или, среды разработки.
В данный момент, среда - C++Builder 6 : Просто удобно...
Несмотря на большое нагромождение класса второстипенными функциями,
основной алгоритм отмечен коментариями, и, можно выделить его в отдельную функциональную
группу. Алгоритм соответствует стандарту, взятых автором из источников:
1. электронное изданние: http://www.rsdn.ru
Автор материала, описывающего алгоритм: Андрей Винокуров mailto:avin@chat.ru
Источник: http://www.enlight.ru/crypto
2. Широкие просторы internet
Алгоритм имеет аккадемический характер, в плане построения, и не является оптимальным
с точки зрения практической реализации. Но, такая организация, позволяет не только
понять смысл, но и легко трассируется, что не маловажно для оптимизации.
Константы генератора случайного числа _rand32(),
основана на исследованиях D. Knuth и H.W. Lewis.
Алгоритм полностью прокомментирован. В остальных случаях, если нет комментариев,
логика очевидна, т.к. автор писал из принципа,
:"если это что-то делает в нескольких местах - значит, это отдельная функция".
Названия не всегда соответсвуют "стандартам", но, логике всегда. В основном,
используется Венгерская натация,по мнению автора, это удобочитаемо и понятно.
При компиляции, есть предупреждения, они не существенны, т.к. это предупреждения о потере
значимости при присваевании / копировании данных с большим разрядом в данные с меньшим разрядом.
Эти вопросы решаются приведением типов, но, в данном случае, это оставленно на CPU,
работа в основном идёт с битами (группами битов),и значимыми являются только опирируемые биты.
----------------------------------------------------------------------------------------------
Автор данной реализации: Пырлик Виктор  vic-ivdel@mail.ru
Агуст 2005 г.
----------------------------------------------------------------------------------------------
Сделано как лабораторная работа.
УГТУ-УПИ   РТФ   Кафедра АСУ
надеюсь, будет зачтена.
----------------------------------------------------------------------------------------------
Если Вы решили работать с этим классом /файлом - пожалуйста, указывайте в этом комментарии,
что именно Вы изменили.                                                                      */
typedef unsigned __int64 UINT_64;
typedef unsigned long   UINT_32;
typedef unsigned long   Key32[8];
typedef unsigned char   BYTE;
typedef unsigned char*  LPBYTE;
typedef const unsigned char*  LPCBYTE;
typedef const char*     LPCSTR;
typedef char*           LPSTR;
typedef unsigned char   BLOCK[16][8];
typedef enum TYPEKOD { fbase, fgame, fgame_os }TTYPEKOD;
typedef enum MODEKOD { b64, b256 }TMODEKOD;
/*=========================================================*/
/*        ВСПОМОГАТЕЛЬНЫЕ ФУНКЦИИ                          */
extern "C" {
	LPSTR Int32ToBin(UINT_32 X);
	LPSTR Int64ToBin(UINT_64 X);
	LPSTR Int32ToHex(LPSTR s, UINT_32 Value);
	LPSTR Int64ToHex(LPSTR s, UINT_64 Value);
	LPCSTR Int32ToStr(LPSTR s, UINT_32 x);
	LPCSTR Int64ToStr(UINT_64 N);
	UINT_32 GetSizeFile(const char* fname);
	UINT_64 StrToInt64(LPCBYTE text);
	void Seed(UINT_32 dum);
	UINT_32 _rand32(void);
	bool SaveDataFile(LPCSTR fname, void *data, size_t size);
	bool LoadDataFile(LPCSTR fname, void *data, size_t size);
};
/*=========================================================*/
/*                 СТРУКТУРА ПАРАМЕТРОВ АЛГОРИТМОА         */
typedef struct {
	Key32   K;    // 256 битный ключ (8 Х 32  = 256  бит = 32 байта)
	UINT_64 SP;   // синхропосылка
	BLOCK   BS;   // таблица замен   (16 Х 8  = 1024 бит = 128 байт)
	TYPEKOD type; // тип кодирования fbase = замена, fgame = гаммирование, fgame_os = гамми. с ОС
	MODEKOD mode; // режим кодирования b64 = сокращенный, b256 = полный
	void SetTable(BLOCK bl)
	{
		int i, j;
		for (i = 0; i<16; i++)
			for (j = 0; j<8; j++)
				BS[i][j] = bl[i][j];
	};
	void GetTable(BLOCK &bl)
	{
		int i, j;
		for (i = 0; i<16; i++)
			for (j = 0; j<8; j++)
				bl[i][j] = BS[i][j];
	};
	void SetKey(Key32 key)
	{
		int i;
		for (i = 0; i<8; i++)
			K[i] = key[i];
	};
	void GetKey(Key32 &key)
	{
		int i;
		for (i = 0; i<8; i++)
			key[i] = K[i];
	};
}GOSTPAR, *LPGOSTPAR;
/*=========================================================*/
/*                 КЛАСС АЛГОРИТМА ГОСТ 28147-89           */
class TGost
{
public:
	TGost(void);
	~TGost(void);
private:
	int icount_iter;
	UINT_64 Kode(UINT_64 N, UINT_64 &X, bool kript = true);
	UINT_64 SK;
	Key32 K;
	UINT_32 N1;
	UINT_32 N2;
	BLOCK   BS;
	TYPEKOD type;
	MODEKOD mode;
	GOSTPAR param;
	struct {
		unsigned ui4_0 : 4;
	};
	/*-----------------------------------------------*/
	UINT_32 ROL(UINT_32 X, BYTE n);
	UINT_64 SWAP64(UINT_64 X);
	UINT_64 SWAP32(UINT_32 N1, UINT_32 N2);
	UINT_64 RPGCH(UINT_64 N);
	UINT_32 ReplaceBlock(UINT_32 x);
	UINT_64 BaseKod(UINT_64 N, UINT_32 X);
	UINT_64 CodeGost(UINT_64 N);
	UINT_64 DeCodeGost(UINT_64 N);
	bool IsExistsBlock(BYTE* b, BYTE ch);
public:
	void InitGost(UINT_64 S, UINT_32 k32[], BLOCK tbl, TYPEKOD tk = fgame_os, MODEKOD mk = b256);
	void InitGost(GOSTPAR gp);
	void InitGost();
	UINT_32 KodeFile(const char *fname_in, const char* fname_out, bool kript = true);

	bool SaveParametr(LPCSTR fname, void *data = NULL);
	bool LoadParametr(LPCSTR fname, void *data = NULL);

	LPGOSTPAR GetParamter();
	void    SetParamter(LPGOSTPAR par);

	void InitTable(BLOCK & bl);
	void InitKey(Key32 & k);
	bool SetKey(char* KeyCh);
	void SetKey(const UINT_32 *a);
	UINT_32* GetKey(Key32 &key);
	UINT_64 GetSinhro();
	UINT_64 SetSinhro(UINT_64 X);

	TYPEKOD GetType();
	MODEKOD GetMode();
	void SetType(TYPEKOD tk);
	void SetMode(MODEKOD mk);
	void SetBlock(const BLOCK b);

	UINT_64 GostGUM(UINT_64 N, UINT_64 &X);
	UINT_64 GostGamOS(UINT_64 N, UINT_64 &X, bool kode = true);
	UINT_64 GetImito(UINT_64 X);

	UINT_64 KriptData(UINT_64 N, bool kode = true);

	void Clear(void);

	UINT_32 GetRand32();
	UINT_64 GetRand64();
	UINT_32 Rand32Char();
	BLOCK* GetTable(BLOCK & bl);
};






