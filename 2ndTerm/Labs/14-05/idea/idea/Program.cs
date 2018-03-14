using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idea
{
  class global
    {
        public string keycode="";
        public string masskey = "";
        public string keydecode="";
        
        //сдвиг на 25 битов влево
        private static string ShiftByteLeft(string s)//вход - строка для набора ключей, длиной 128
//на выход - та же самая строка, сдвинутая на 25 символов влево
        {
            string s1 = "";
            for (int i = 25; i < 128; i++)
                s1 += s[i];
            for (int i = 0; i < 25; i++)
                s1 += s[i];
            return s1;
        }
    
    //сдвиг на 25 битов вправо
        private static string ShiftbyteRight(string s)
        {
            string s1 = "";
            for (int i = 103; i < 128; i++)
                s1 += s[i];
            for (int i = 0; i < 103; i++)
                s1 += s[i];
            return s1;
        }
 
        public int Numblocks = 0;
 
        private static string[] blockstext = new string[4];//текст: 4 блока по 16 бит
        private static string[] blockskey = new string[6];//ключ: 8 блоков по 16 бит
 
    //суммирование по модулю 2^16
        private static string Sum(string a1, string a2) //на вход - 2 блока string, на выход 1 string.
        {
            byte[] a10 = new byte[16];
            byte[] a20 = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                if (a1[i] == '0') a10[i] = 0;
                else a10[i] = 1;
                if (a2[i] == '0') a20[i] = 0;
                else a20[i] = 1;
            }
            for (int i = 15; i >= 1; i--)
            {
                a10[i] += a20[i];
                if (a10[i] > 1)
                {
                    a10[i] -= 2;
                    a10[i - 1]++;
                } 
            }
            if (a10[0] > 1) a10[0]-=2;
            a1="";
            for (int i = 0; i < 16; i++)
                if (a10[i] == 1) a1 += "1";
                else a1 += "0";
            return a1;
        }
    
    //умножение по модулю 2^16+1
        private static string Multi(string a1, string a2)
        {
            long sk1 = 0;
            long sk2 = 0;
            int[] a10 = new int[16];
            int[] a0 = new int[16];
            for (int i = 0; i < 16; i++)
            {
                if (a1[i]=='1')
                    sk1 += Convert.ToInt64(Math.Pow(2, 15 - i));
                if (a2[i]=='1')
                    sk2 += Convert.ToInt64(Math.Pow(2, 15 - i));
                if (a1[i] == '0') a10[i] = 0;
                else a10[i] = 1;
                a0[i] = 0;
            }
            sk1 = sk1 * sk2;
            sk2 = sk1 % (Convert.ToInt64(Math.Pow(2, 16) + 1));
            string s1 = "";
            for (int i = 15; i >= 0; i--)
            {
                if (sk2 >= Math.Pow(2, i))
                {
                    s1 += "1";
                    sk2 -= Convert.ToInt64(Math.Pow(2, i));
                }
                else s1 += "0";
            }
            return s1;
        }
 
    //исключающее ИЛИ
        private static string XOR(string a1, string a2)
        {
            string a0 = "";
            for ( int i = 0; i < 16; i++)
                if (a1[i] == a2[i]) a0 += "0";
                else a0 += "1";
            return a0;
        }
 
    //цикл, описанный по 1 ссылке
        private void cycle ()
        {
            string[] a = new string[14];
            a[0] = Multi(blockstext[0], blockskey[0]);
            a[1] = Sum(blockstext[1], blockskey[1]);
            a[2] = Sum(blockstext[2], blockskey[2]);
            a[3] = Multi(blockstext[3], blockskey[3]);
            a[4] = XOR(a[0], a[2]);
            a[5] = XOR(a[1], a[3]);
            a[6] = Multi(a[4], blockskey[4]);
            a[7] = Sum(a[5], a[6]);
            a[8] = Multi(a[7], blockskey[5]);
            a[9] = Sum(a[6], a[8]);
            a[10] = XOR(a[0], a[8]);
            a[11] = XOR(a[2], a[8]);
            a[12] = XOR(a[1], a[9]);
            a[13] = XOR(a[3], a[9]);
            blockstext[0] = a[10];
            blockstext[1] = a[11];
            blockstext[2] = a[12];
            blockstext[3] = a[13]; 
        }
 
        //алгоритм IDEA. Из главного окна обращаемся непосредственно к нему. Вход - 2 строки: 1) набор битов считанного блока (длина 64). 2) набор битов нашего ключа (длина 128). Выход - шифроблок (64 символа)
        public string Encryption (string st1, string st2)
        {
            string s = "";
            string[] blocksk = new string[8];//ключ: 8 блоков по 16 бит
            for (int i = 0; i < 4; i++)
                for (int i1 = 0; i1 < 16; i1++)
                    blockstext[i] += st1[i * 16 + i1]; //разбиваем текстовый блок (64 символа) на 4 блока (16 символов)
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];//разбиваем блок с ключами(128 символов) на 8 ключей (16 символов)
            }
            blockskey[0] = blocksk[0];//первые шесть ключей
            blockskey[1] = blocksk[1];// для использования в первом проходе цикла
            blockskey[2] = blocksk[2];
            blockskey[3] = blocksk[3];
            blockskey[4] = blocksk[4];
            blockskey[5] = blocksk[5];
            cycle();//1 цикл
            s = blockstext[2];// после цикла меняем местами 2 и 3 текстовые блоки
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[6];//оставшиеся блоки-ключей (7 и 8) 
            blockskey[1] = blocksk[7];//записываем как новые ключи для цикла (1 и 2 соответсвтенно)
            st2 = ShiftByteLeft(st2);//строка-ключ сдвигается на 25 символов влево
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];//задаются новые ключи 
            }
            blockskey[2] = blocksk[0];
            blockskey[3] = blocksk[1];
            blockskey[4] = blocksk[2];
            blockskey[5] = blocksk[3];
            cycle();//2 цикл
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[4];
            blockskey[1] = blocksk[5];
            blockskey[2] = blocksk[6];
            blockskey[3] = blocksk[7];
            st2 = ShiftByteLeft(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[4] = blocksk[0];
            blockskey[5] = blocksk[1];
            cycle();//3
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[2];
            blockskey[1] = blocksk[3];
            blockskey[2] = blocksk[4];
            blockskey[3] = blocksk[5];
            blockskey[4] = blocksk[6];
            blockskey[5] = blocksk[7];
            cycle();//4
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            st2 = ShiftByteLeft(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = blocksk[0];
            blockskey[1] = blocksk[1];
            blockskey[2] = blocksk[2];
            blockskey[3] = blocksk[3];
            blockskey[4] = blocksk[4];
            blockskey[5] = blocksk[5];
            cycle();//5
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[6];
            blockskey[1] = blocksk[7];
            st2 = ShiftByteLeft(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[2] = blocksk[0];
            blockskey[3] = blocksk[1];
            blockskey[4] = blocksk[2];
            blockskey[5] = blocksk[3];
            cycle();//6
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[4];
            blockskey[1] = blocksk[5];
            blockskey[2] = blocksk[6];
            blockskey[3] = blocksk[7];
            st2 = ShiftByteLeft(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[4] = blocksk[0];
            blockskey[5] = blocksk[1];
            cycle();//7
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = blocksk[2];
            blockskey[1] = blocksk[3];
            blockskey[2] = blocksk[4];
            blockskey[3] = blocksk[5];
            blockskey[4] = blocksk[6];
            blockskey[5] = blocksk[7];
            cycle();//8
            st2 = ShiftByteLeft(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = blocksk[0];
            blockskey[1] = blocksk[1];
            blockskey[2] = blocksk[2];
            blockskey[3] = blocksk[3];
            blockstext[0] = Multi(blockstext[0], blockskey[0]);
            blockstext[1] = Sum(blockstext[1], blockskey[1]);
            blockstext[2] = Sum(blockstext[2], blockskey[2]);
            blockstext[3] = Multi(blockstext[3], blockskey[3]);//последние преобразования шифроблоков
            s = blockstext[0] + blockstext[1] + blockstext[2] + blockstext[3];//конкатенация этих блоков
            masskey = st2;
            return s;            
        }
 
        //получение A=1\k , т.ч A*k=1 {mod 2^16+1}
        private static string Inverse (string s)
        {
            string s1 = "";
            int sk1 = 0;
            long sr = 0;
            for (int i = 0; i < 16; i++)
            {
                if (s[i] == '1')
                    sk1 += Convert.ToInt32(Math.Pow(2, 15 - i));
            }
            for (int i = 0; i < 65537; i++)
            {
                sr = (sk1 * i) % 65537;
                if (sr == 1)
                {
                    sk1 = i;
                    break;
                }
            }
            for (int i = 15; i >= 0; i--)
            {
                if (sk1 >= Math.Pow(2, i))
                {
                    s1 += "1";
                    sk1 -= Convert.ToInt32(Math.Pow(2, i));
                }
                else s1 += "0";
            }
            return s1;
        }
 
        //получение A=-k , т.ч. A+k=0
        private static string Negative (string s)
        {
            string s1 = "";
            byte[] a = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                if (s[i] == '1') a[i] = 0;
                else a[i] = 1;
            }
            a[15]++;
            for (int i = 15; i > 0; i--)
            {
                if (a[i] > 1)
                {
                    a[i] -= 2;
                    a[i - 1]++;
                }
            }
            if (a[0] == 2) a[0] = 0;
            for (int i = 0; i < 16; i++)
                if (a[i] == 1) s1 += "1";
                else s1 += "0";
            return s1;
        }
 
        //алгоритм де-IDEA
        public string Decryption (string st1, string st2)
        {
            string s = "";
            string[] a = new string[13];//каждые действия
            string[] blocksk = new string[8];//ключ: 8 блоков по 16 бит
 
            for (int i = 0; i < 4; i++)
            {
                blockstext[i]="";
                for (int i1 = 0; i1 < 16; i1++)
                    blockstext[i] += st1[i * 16 + i1];
            }
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = Inverse (blocksk[0]);
            blockskey[1] = Negative (blocksk[1]);
            blockskey[2] = Negative (blocksk[2]);
            blockskey[3] = Inverse (blocksk[3]);
            st2 = ShiftbyteRight(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[4] = blocksk[6];
            blockskey[5] = blocksk[7];
            cycle();//1
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = Inverse(blocksk[2]);
            blockskey[1] = Negative (blocksk[4]);
            blockskey[2] = Negative (blocksk[3]);
            blockskey[3] = Inverse (blocksk[5]);
            blockskey[4] = blocksk[0];
            blockskey[5] = blocksk[1];
            cycle();//2            
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            st2 = ShiftbyteRight(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            
            blockskey[0] = Inverse (blocksk[4]);
            blockskey[1] = Negative (blocksk[6]);
            blockskey[2] = Negative (blocksk[5]);
            blockskey[3] = Inverse (blocksk[7]);
            blockskey[4] = blocksk[2];
            blockskey[5] = blocksk[3];
            cycle();//3
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[1] = Negative(blocksk[0]);
            blockskey[3] = Inverse (blocksk[1]);
            st2 = ShiftbyteRight (st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = Inverse (blocksk[6]);
            blockskey[2] = Negative (blocksk[7]);
            blockskey[4] = blocksk[4];
            blockskey[5] = blocksk[5];
            cycle();//4
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = Inverse(blocksk[0]);
            blockskey[1] = Negative (blocksk[2]);
            blockskey[2] = Negative (blocksk[1]);
            blockskey[3] = Inverse (blocksk[3]);
            st2 = ShiftbyteRight(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[4] = blocksk[6];
            blockskey[5] = blocksk[7];
            cycle();//5
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[0] = Inverse(blocksk[2]);
            blockskey[1] = Negative (blocksk[4]);
            blockskey[2] = Negative (blocksk[3]);
            blockskey[3] = Inverse (blocksk[5]);
            blockskey[4] = blocksk[0];
            blockskey[5] = blocksk[1];
            cycle();//6
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            st2 = ShiftbyteRight(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = Inverse (blocksk[4]);
            blockskey[1] = Negative(blocksk[6]);
            blockskey[2] = Negative(blocksk[5]);
            blockskey[3] = Inverse (blocksk[7]);
            blockskey[4] = blocksk[2];
            blockskey[5] = blocksk[3];
            cycle();//7
            s = blockstext[2];
            blockstext[2] = blockstext[1];
            blockstext[1] = s;
            blockskey[1] = Negative(blocksk[0]);
            blockskey[3] = Inverse (blocksk[1]);
            st2 = ShiftbyteRight(st2);
            for (int i = 0; i < 8; i++)
            {
                blocksk[i] = "";
                for (int i1 = 0; i1 < 16; i1++)
                    blocksk[i] += st2[i * 16 + i1];
            }
            blockskey[0] = Negative(blocksk[6]);
            blockskey[2] = Inverse (blocksk[7]);
            blockskey[4] = blocksk[4];
            blockskey[5] = blocksk[5];
            cycle();//8
            blockskey[0] = Negative(blocksk[0]);
            blockskey[1] = Inverse (blocksk[1]);
            blockskey[2] = Inverse (blocksk[2]);
            blockskey[3] = Negative(blocksk[3]);
            blockstext[0] = Multi(blockstext[0], blockskey[0]);
            blockstext[1] = Sum(blockstext[1], blockskey[1]);
            blockstext[2] = Sum(blockstext[2], blockskey[2]);
            blockstext[3] = Multi(blockstext[3], blockskey[3]);
            s = blockstext[0] + blockstext[1] + blockstext[2] + blockstext[3];
            return s;
        }
        static void Main(string[] args)
        {

        }
    }


}

