using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCode
{
    public class CycleCode
    {
        public  bool[] Gx, Xn, Xk, Xr, Rx, Yn, Sr, U, En;
        public int n, k, r;
        public  bool isCode;
        //public enum Gx;
        private static bool[][] H;
        private static bool[][] G =  {
            new bool[]{ true,true},
            new bool[]{ true,true,true},
            new bool[]{ true,true,false,true },
            new bool[]{ true,true,false,false,true},
            new bool[]{ true,false, true,false,false, true},
            new bool[]{ true, true, false, false, false, false, true},
            new bool[]{ true, false,false,false,true,false,false,true},
            new bool[]{ true, true, true,true, false,false, true,true,true} };



        //public bool CalcXn()
        //{
        //    bool[] tmpXn = new bool[n];
        //    Array.Copy(Xk, tmpXn, k);
        //}

        private static bool[] GetPolynom(int r)
        {
            if (r < G.Count() + 1)
                return G[r - 1];
            else return null;
        }

        public void CalcXn()
        {
            Xn = new bool[n];
            Gx = GetPolynom(r);
            Rx = Xk.Mul(r).Div(Gx);
            Array.Copy(Xk, Xn, k);
            Array.Copy(Rx, 0, Xn, n - Rx.Length, Rx.Length);
            
        }

        public bool[] XnDivGx()
        {
            return Xn.Div(Gx);
        }

        //public bool[] operator /(bool[] X, bool[] G)
        //{
        //    new bool
        //}

        public static bool[] GetEVector(bool[][] H, bool[] S)
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

        public static bool IsSZero(bool[] S)
        {
            for (int i = 0; i < S.Length; i++)
            {
                if (S[i]) return false;
            }
            return true;
        }

        public static bool[] FindInfWord_r(bool[][] _H, bool[] _Wk)
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

        public static bool IsEquals(bool[] s1, bool[] s2)
        {
            if (s1.Length != s2.Length) return false;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] != s2[i]) return false;
            return true;
        }

        public bool[][] GetHelpMatrix(int _r, int _k)
        {
            bool[][] result = new bool[r][];
            FillIMatrix(ref result, _k, _r);
            return result;
        }

        public static string BinToWord(bool[] Yk)
        {
            //string w;
            StringBuilder sb = new StringBuilder();
            MasToString(Yk, sb);
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

        private static void MasToString(bool[] Yk, StringBuilder sb)
        {
            foreach (var s in Yk)
            {
                sb.Append(Convert.ToInt32(s));
            }
        }

        private static bool IsWordBinary(int[] w)
        {
            for (int i = 0; i < w.Count(); i++)
            {
                if (w[i] != 1 && w[i] != 0) return false;
            }
            return true;
        }

        public void EnterWord(bool isX /* is entering Xk*/)
        {
            string input;
            isCode = false;
            input = Console.ReadLine();
            int[] arr = input
                .ToCharArray().Select(x => x - '0').ToArray();
            isCode = IsWordBinary(arr);
            if (isCode)
            {
                if (isX) Xk = arr.Select(i => Convert.ToBoolean(i)).ToArray();
                else Yn = arr.Select(i => Convert.ToBoolean(i)).ToArray();
            }
            else
            {
                if(isX)
                Xk = StringToBoolMas(input);
                else Yn = StringToBoolMas(input);
            }
        }

        public void CalcSindrom()
        {
            Sr = Yn.Div(Gx);
        }

        private static bool[] StringToBoolMas(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] b = System.Text.Encoding.ASCII.GetBytes(s);
            foreach (var e in b)
            {
                if (Convert.ToString(e, 2).Length > 6)
                    sb.Append(Convert.ToString(e, 2));
                else
                    sb.Append("0" + Convert.ToString(e, 2));
            }
            string binaryStr = sb.ToString();
            return binaryStr.ToCharArray().Select(x => x - '0').ToArray().
                Select(i => Convert.ToBoolean(i)).ToArray();
        }

        public void CalcRKN()
        {
            k = Xk.Count();
            r = (int)Math.Ceiling(Math.Log(k, 2) + 1);
            n = k + r;
        }

        public static bool[][] TransMatr(bool[][] matr)
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

        public void printM(bool[][] matr)
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

        public void printM(bool[] matr)
        {
            for (int j = 0; j < matr.Length; j++)
            {
                if (matr[j]) Console.Write('1' + " ");
                else Console.Write('0' + " ");
            }
            Console.WriteLine();
        }

        public static void FillIMatrix(ref bool[][] _M, int _k, int _r)
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
