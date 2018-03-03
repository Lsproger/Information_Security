using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterativeCode
{
    public class IterativeCode
    {
        public bool[] Gx, Xn, Xk, tmpXk, Xr, Yn, cYn, Yk, Yr, _Yr, S, E, R, RrIn, RcIn, RrOut, RcOut, RrCheck, RcCheck;
        public string sYk;
        public int n, k, r, rc, rr;
        public bool isCode, RIn, ROut, RCheck;
        public bool[][] Hin, Hout, Hcheck, H;

        public void GenerateHelpMatrix()
        {
            H = new bool[r][];
            for (int i = 0; i < r; i++)
            {
                H[i] = new bool[n];
                if (i < rc)
                    for (int j = 0; j < rr; j++)
                        H[i][(i * rr) + j] = true;
                else
                    for(int j = 0; j < rc; j++)
                    {
                        H[i][(j * rr) + (i - rc)] = true;
                    }
            }
            FillIMatrix(ref H);
        }

        public void GetYkYr()
        {
            Yk = new bool[k];
            Yr = new bool[r];
            Array.Copy(Yn, 0, Yk, 0, Yk.Length);
            Array.Copy(Yn, k, Yr, 0, Yr.Length);
        }

        public void CalcS()
        {
            _Yr = CalcR(Yk);
            bool[] tmpYr = new bool[r];
            Array.Copy(Yr, tmpYr, Yr.Length);
            S = tmpYr.XOR(_Yr);
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

        public bool[][] GenerateMatrixView(bool[] A)
        {
            bool[][] M = new bool[rc][];
            for (int i = 0; i < rc; i++)
            {
                M[i] = new bool[rr];
                for (int j = 0; j < rr; j++)
                    M[i][j] = A[(rr * i) + j];
            }
            return M;

            //WriteHelpSymbols(isX);
            //}
            //else
            //{
            //    Hout = new bool[rc][];
            //    for (int i = 0; i < rc; i++)
            //    {
            //        Hout[i] = new bool[rr];
            //        for (int j = 0; j < rr; j++)
            //        {
            //            if ((rr * i) + j < rr*rc)
            //                Hout[i][j] = Yn[(rr * i) + j];
            //        }
            //    }
            //}
        }

        public bool[][] GenerateMatrixViewWithCheckSymbols(bool[] A, bool[] R)
        {
            bool[][] tmp = GenerateMatrixView(A);
            bool[][] res = new bool[tmp.Length + 1][];
            for (int i = 0; i < tmp.Length + 1; i++)
            {
                res[i] = new bool[tmp[0].Length + 1];
                if (i == tmp.Length) break;
                for (int j = 0; j < tmp[0].Length; j++)
                    res[i][j] = tmp[i][j];
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                res[i][res[0].Length - 1] = R[i];
            }
            for (int i = 0; i < tmp[0].Length; i++)
            {
                res[res.Length - 1][i] = R[rc + i];
            }
            return res;
        }

        private void GetEVector()
        {
            bool[][] tmpH = TransMatr(H);
            E = new bool[n];
            for (int i = 0; i < n; i++)
            {
                if (S.IsEquals(tmpH[i]))
                {
                    E[i] = true;
                    break;
                }
            }
        }

        public void CorrectError()
        {
            GetEVector();
            Yn.XOR(E);
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

        private bool IsCheckSymbolsGood(bool[][] H)
        {
            bool col = false;
            bool row = false;
            for (int i = 0; i < H.Length; i++)
                col ^= H[i][H[0].Length - 1];
            for (int i = 0; i < H[0].Length; i++)
                row ^= H[H.Length - 1][i];
            if ((col ^ row) == H[H.Length - 1][H[0].Length - 1]) return true;
            else return false;
        }

        private void CalcHelpSymbols(bool[][] H, ref bool[] Rc, ref bool[] Rr, ref bool R)
        {
            Rc = new bool[rc - 1];
            Rr = new bool[rr - 1];
            CalcRcRr(H, ref Rc, ref Rr);
            //   CalcR(Rc, Rr, out R);
        }

        private void CalcRcRr(bool[][] H, ref bool[] Rc, ref bool[] Rr)
        {

        }

        private bool[] CalcR(bool[] A)
        {
            R = new bool[r];

            for (int i = 0; i < rc; i++)
                for (int j = rr - 1; j >= 0; j--)
                    R[i] ^= A[(i * rr) + j];
            for (int j = 0; j < rr; j++)
                for (int i = 0; i < rc; i++)
                    R[j + rc] ^= A[(i * rr) + j];//cheknut'

            return R;

        }

        public void CalcXn()
        {
            Xn = new bool[n];
            Array.Copy(Xk, Xn, Xk.Length);
            Array.Copy(CalcR(Xk), 0, Xn, k, r);
        }

        public void WriteHelpSymbols(bool input)
        {
            if (input)
            {
                CalcHelpSymbols(Hin, ref RcIn, ref RrIn, ref RIn);
                WriteSymbolsToMatrix(ref Hin, ref RcIn, ref RrIn, ref RIn);
            }
            else
            {
                Hcheck = Hout;
                CalcHelpSymbols(Hcheck, ref RcOut, ref RrOut, ref ROut);
                WriteSymbolsToMatrix(ref Hcheck, ref RcOut, ref RrOut, ref ROut);
            }

        }

        private void WriteSymbolsToMatrix(ref bool[][] H, ref bool[] Rc, ref bool[] Rr, ref bool R)
        {
            for (int i = 0; i < rc - 1; i++)
                H[i][rr - 1] = Rc[i];
            for (int i = 0; i < rr - 1; i++)
                H[rc - 1][i] = Rr[i];
            H[rc - 1][rr - 1] = R;
        }

        private void CalcRcRr()
        {
            if (Math.Sqrt(k) == Math.Truncate(Math.Sqrt(k)))
            {
                rc = (int)Math.Sqrt(k);
                rr = (int)Math.Sqrt(k);
            }
            else
            {
                rr = (int)Math.Ceiling(Math.Sqrt(k));
                int i = 1;
                while (true)
                {
                    double a = Math.Truncate((double)((k + i) / rr));
                    double b = ((double)(k + i) / rr);

                    if (a == b)
                    {
                        rc = (int)(a);
                        if (rc * rr < k) rc++;
                        break;
                    }
                    i++;
                }
            }
        }

        public void EnterWord(bool isX /* is entering Xk*/)
        {
            string input;
            input = Console.ReadLine();
            int[] arr = input
                .ToCharArray().Select(x => x - '0').ToArray();
            if (isX) tmpXk = arr.Select(i => Convert.ToBoolean(i)).ToArray();
            else Yn = arr.Select(i => Convert.ToBoolean(i)).ToArray();
        }

        public void CalcRKN()
        {
            k = tmpXk.Count();
            CalcRcRr();
            k = rr * rc;
            r = rr + rc;
            n = k + r;
            Xk = new bool[k];
            Array.Copy(tmpXk, Xk, tmpXk.Length);
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

        private static void FillIMatrix(ref bool[][] _M)
        {
            int n = _M[0].Length;
            int _r = _M.Length;
            int _k = n - _r;
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
