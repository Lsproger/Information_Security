using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace HemmingCode_3_
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] Xk, Yk, Xr, Xn, Yn, Yr_, Yr, S, YnDecrypted;
            bool[][] H;
            int r = 0, k = 0;
            bool isCode, isModifyied = false;
            try
            {
                for (;;)
                {
                    Console.WriteLine("Vvedi slovo");
                    Xk = EnterWord(true, out isCode);
                    r = CalcR(Xk);
                    k = Xk.Count();
                    Console.WriteLine("Would u like  2 use modifyied Hemming code?(y/n)");
                    if (Console.ReadKey(true).KeyChar == 'y')
                    {
                        isModifyied = true;
                        r += 1;
                    }
                    Encrypt(r, k, Xk, isModifyied, out H, out Xr, out Xn);
                    Console.WriteLine("H:");
                    printM(H);
                    PrintAnswer(Xn, Xk, Xr, 'X');
                    Console.WriteLine("Input Yn");
                    YnDecrypted = Decrypt(r, k, isCode, isModifyied, Xn, Xk, Xr, out Yn, out Yk, out Yr, out Yr_, out S);
                    PrintAnswer(Yn, Yk, Yr, 'Y');
                    Console.WriteLine("Yr':");
                    printM(Yr_);
                    Console.WriteLine("S:");
                    printM(S);
                   // isCode = true;
                    if (isCode)
                    {
                        Console.WriteLine("Corrected Yn:");
                        printM(YnDecrypted);
                    }
                    else
                    {
                        bool[] _Yk = new bool[k];
                        for (int i = 0; i < k; i++)
                        {
                            _Yk[i] = YnDecrypted[i];
                        }
                        Console.WriteLine("Corrected Yk:" + BinToWord(_Yk));
                    }
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message + '\n' + e.Source + '\n' + e.StackTrace);
                Program.Main(new string[] { });
            }
        }

        private static void PrintAnswer(bool[] n, bool[] k, bool[] r, char xy)
        {
            Console.WriteLine(xy + "n:");
            printM(n);
            Console.WriteLine(xy + "k:");
            printM(k);
            Console.WriteLine(xy + "r:");
            printM(r);
        }

        private static bool[] Encrypt(int r, int k, bool[] Xk, bool isModifyied, out bool[][] H, out bool[] Xr, out bool[] Xn)
        {
            H = GetHelpMatrix(r, k);
            if (isModifyied)
                ModifyHMatrix(ref H);
            Xr = FindInfWord_r(H, Xk);

            Xn = new bool[k + r];
            for (int i = 0; i < k + r; i++)
            {
                if (i < k) Xn[i] = Xk[i];
                else Xn[i] = Xr[i - k];
            }
            return Xn;
        }

        private static void ModifyHMatrix(ref bool[][] H)
        {
            for (int i = 0; i < H[0].Count(); i++)
            {
                H[H.Count() - 1][i] = true;
            }

            for (int i = 0; i < H[0].Count(); i++)
            {
                for (int j = H.Count() - 2; j >= 0; j--)
                {
                    H[H.Count() - 1][i] = H[H.Count() - 1][i] ^ H[j][i];
                }
            }
        }

        private static bool[] Decrypt(int r, int k, bool isCode, bool isModifyied, 
            bool[] Xn, bool[] Xk, bool[] Xr, out bool[] Yn, out bool[] Yk, 
            out bool[] Yr, out bool[] Yr_, out bool[] S)
        {
            bool[][] H = GetHelpMatrix(r, k);
            if (isModifyied) ModifyHMatrix(ref H);
            bool[] tmpYn = EnterWord(false, out isCode);
            if (!isCode)
            {
                Yn = new bool[k + r];
                for (int i = 0; i < r + k; i++)
                {
                    if (i < k) Yn[i] = tmpYn[i];
                    else Yn[i] = Xr[i - k];
                }
            }
            else Yn = tmpYn;
            Yk = new bool[k];
            Yr = new bool[r];
            for (int i = 0; i < Yn.Length; i++)
            {
                if (i < k) Yk[i] = Yn[i];
                else Yr[i - k] = Yn[i];
            }
            Yr_ = FindInfWord_r(H, Yk);
            S = XOR(Yr, Yr_);
            if (isModifyied)
            {
                if (ErrCount(S) == 1)
                {
                    Console.WriteLine("Odna oshibka");
                    bool[] E = GetEVector(H, S);
                    return XOR(Yn, E);
                }
                else if (ErrCount(S) == -1) Console.WriteLine("Oshibki 2");
                else if (ErrCount(S) == 0) Console.WriteLine("Oshibok net");
                else Console.WriteLine("Chto-to esche");
            }
            else return XOR(Yn, GetEVector(H, S));
            return Yn;
        }

        private static int? ErrCount(bool[] S)
        {
            int num = 0;
            for (int i = 0; i < S.Length; i++) if (S[i]) num++;
            if (num == 0) return 0;
            else if (num % 2 == 0) return -1;
            else if (num%2 ==1) return 1;
            return null;
        }

        private static bool[] GetEVector(bool[][] H, bool[] S)
        {
            int n = H[0].Length;
            bool[] E = new bool[n];
            if (IsSZero(S)) return E;
            for (int i = 0; i < H[0].Length; i++)
            {
                if (IsEquals(S, TransMatr(H)[i]))
                {
                    E[i] = true;
                    return E;
                }
            }
            return E;
        }

        private static bool IsSZero(bool[] S)
        {
            for (int i = 0; i < S.Length; i++)
            {
                if (S[i]) return false;
            }
            return true;
        }

        private static bool[] FindInfWord_r(bool[][] _H, bool[] _Wk)
        {
            int r = _H.Length;
            int k = _H[0].Length - r;
            bool[] Wr = new bool[r];
            for (int i = 0; i < r; i++)
            {
                bool[] Wrjx = new bool[k];
                for (int j = 0; j < k; j++)
                {
                    Wrjx[j] = _H[i][j] & _Wk[j];
                    if (j == 0) Wr[i] = Wrjx[j];
                    else Wr[i] = Wr[i] ^ Wrjx[j];
                }
            }
            return Wr;
        }

        private static bool IsEquals(bool[] s1, bool[] s2)
        {
            if (s1.Length != s2.Length) return false;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] != s2[i]) return false;
            return true;
        }

        private static bool[] XOR(bool[] A, bool[] B)
        {
            bool[] res = new bool[A.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = A[i] ^ B[i];
            }
            return res;
        }

        private static bool[][] GetHelpMatrix(int _r, int _k)
        {
            bool[][] result = TransMatr(Enumerable.Range(0, 1 << (_r * 2))
                .Select(i => new BitArray(new int[] { i }).Cast<bool>().Take(_r).ToArray())
                .Where(k =>
                {
                    int n = 0;
                    for (int j = 0; j < _r; j++)
                    {
                        if (k[j]) n++;
                    }
                    if (n >= 2) return true;
                    else return false;
                }
                ).Take(_k + _r) //Добавление r лишних столбцов для создания единичной подматрицы
                .ToArray());
            FillIMatrix(ref result, _k, _r);
            return result;
        }

        private static string BinToWord(bool[] Yk)
        {
            string w;
            StringBuilder sb = new StringBuilder();
            foreach (var s in Yk)
            {
                sb.Append(Convert.ToInt32(s));
            }
            string res = "";
            for (int i = 0; i < sb.Length; i += 7)
            {
                res += (char)Convert.ToInt32(sb.ToString().Substring(i, 7), 2);
            }

            return res;
            
           
            //w = binaryStr
              //  .ToCharArray().Select(x => x - '0').ToArray().
                //Select(i => Convert.ToBoolean(i)).ToArray();


        }

        private static bool[] EnterWord(bool isX /* is entering Xk*/, out bool _isCode)
        {
            bool[] w;
            string input;
            _isCode = false;
            input = Console.ReadLine();
            int[] arr = input
                .ToCharArray().Select(x => x - '0').ToArray();
            for (int i = 0; i < arr.Count(); i++)
            {
                if (arr[i] == 1 || arr[i] == 0) _isCode = true;
                else
                {
                    _isCode = false;
                    break;
                }
            }
            if (_isCode)
                w = arr.Select(i => Convert.ToBoolean(i)).ToArray();
            else
            {
                StringBuilder sb = new StringBuilder();
                byte[] b = System.Text.Encoding.ASCII.GetBytes(input);
                foreach (var e in b)
                {
                    if(Convert.ToString(e, 2).Length > 6)
                        sb.Append(Convert.ToString(e, 2));
                    else
                        sb.Append("0" + Convert.ToString(e, 2));
                }
                string binaryStr = sb.ToString();
                w = binaryStr
                    .ToCharArray().Select(x => x - '0').ToArray().
                    Select(i => Convert.ToBoolean(i)).ToArray();
            }
            return w;
        }

        private static int CalcR(bool[] _Xk)
        {
            return (int)Math.Ceiling(Math.Log(_Xk.Count(), 2) + 1);
        }

        private static bool[][] TransMatr(bool[][] matr)
        {
            int x, y;
            x = matr.Length;
            y = matr[0].Length;
            bool[][] newMatr = new bool[y][];
            for (int j = 0; j < y; j++)
            {
                newMatr[j] = new bool[x];
                for (int i = 0; i < x; i++)
                {
                    newMatr[j][i] = matr[i][j];
                }
            }
            return newMatr;
        }

        private static void printM(bool[][] matr)
        {
            for (int i = 0; i < matr.Length; i++)
            {
                for (int j = 0; j < matr[i].Length; j++)
                {
                    if (matr[i][j] == true) Console.Write('1' + " ");
                    else Console.Write('0' + " ");
                }
                Console.WriteLine();
            }
        }

        private static void printM(bool[] matr)
        {
            for (int j = 0; j < matr.Length; j++)
            {
                if (matr[j]) Console.Write('1' + " ");
                else Console.Write('0' + " ");
            }
            Console.WriteLine();
        }

        private static void FillIMatrix(ref bool[][] _M, int _k, int _r)
        {
            int n = _k + _r;
            for (int i = _r - 1; i > -1; i--)
            {
                for (int j = n - 1; j > _k - 1; j--)
                {
                    if (j == i + _k) _M[i][j] = true;
                    else _M[i][j] = false;
                }
            }
        }
    }
}