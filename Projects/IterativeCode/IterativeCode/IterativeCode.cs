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
        public bool[] Gx, Xn, Xk, Xr, Yn, Yk, Sr, E, RrIn, RcIn, RrOut, RcOut, RrCheck, RcCheck;
        public string sYk;
        public int n, k, r, hx, hy, x, y;
        public bool isCode, RIn, ROut, RCheck;
        public bool[][] Hin, Hout, Hcheck;

        public void GenerateMatrixView(bool isX)
        {
            if (isX)
            {
                Hin = new bool[hy][];
                for (int i = 0; i < hy; i++)
                {
                    Hin[i] = new bool[hx];
                    if (i < hy - 1)
                        for (int j = 0; j < hx - 1; j++)
                        {
                            if (((hx - 1) * i) + j < Xk.Length)
                                Hin[i][j] = Xk[((hx - 1) * i) + j];
                            else break;
                        }
                }
                WriteHelpSymbols(isX);
            }
            else
            {
                Hout = new bool[hy][];
                for (int i = 0; i < hy; i++)
                {
                    Hout[i] = new bool[hx];
                    for (int j = 0; j < hx; j++)
                    {
                        if ((hx * i) + j < hx*hy)
                            Hout[i][j] = Yn[(hx * i) + j];
                    }
                }
            }
        }

        public void CorrectError()
        {
            if (IsCheckSymbolsGood(Hout))
            {
                CalcHelpSymbols(Hout,ref RcCheck,ref RrCheck,ref RCheck);
                for (int i = 0; i < RrCheck.Length; i++)
                    if (RrCheck[i] != Hout[Hout.Length - 1][i]) x = i;
                for (int i = 0; i < RcCheck.Length; i++)
                    if (RcCheck[i] != Hout[i][Hout[0].Length - 1]) y = i;
                Hout[y][x] = !Hout[y][x];
            }
            else
            {
                WriteHelpSymbols(false);
            }
        }

        private bool IsCheckSymbolsGood(bool[][] H)
        {
            bool col = false;
            bool row = false;
            for (int i = 0; i < H.Length; i++)
                col ^= H[i][H[0].Length - 1];
            for (int i = 0; i < H[0].Length; i++)
                row ^= H[H.Length-1][i];
            if ((col ^ row) == H[H.Length - 1][H[0].Length - 1]) return true;
            else return false;
        }

        private void CalcHelpSymbols(bool[][] H, ref bool[] Rc, ref bool[] Rr, ref bool R)
        {
            Rc = new bool[hy - 1];
            Rr = new bool[hx - 1];
            CalcRcRr(H, ref Rc, ref Rr);
            CalcR(Rc, Rr, out R);
        }

        private void CalcRcRr(bool[][] H, ref bool[] Rc, ref bool[] Rr)
        {
            for (int i = 0; i < H.Length - 1; i++)
                for (int j = H[0].Length - 2; j >= 0; j--)
                    Rc[i] ^= H[i][j];
            for (int j = 0; j < Hin[0].Length - 1; j++)
                for (int i = H.Length - 2; i >= 0; i--)
                    Rr[j] ^= H[i][j];
        }

        private void CalcR(bool[] Rc, bool[] Rr, out bool R)
        {
            R = false;
            for (int i = Rc.Length-1; i >= 0; i--)
                R ^= Rc[i];
            for (int i = Rr.Length-1; i >= 0; i--)
                R ^= Rr[i];
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
            for (int i = 0; i < hy - 1; i++)
                H[i][hx - 1] = Rc[i];
            for (int i = 0; i < hx - 1; i++)
                H[hy - 1][i] = Rr[i];
            H[hy - 1][hx - 1] = R;
        }

        private void CalcHxy()
        {
            if (Math.Sqrt(k) == Math.Truncate(Math.Sqrt(k)))
            {
                hy = (int)Math.Sqrt(k) + 1;
                hx = (int)Math.Sqrt(k) + 1;
            }
            else
            {
                bool isI = Math.Truncate((double)k) == k ? true : false;
                hx = (int)Math.Truncate(Math.Sqrt(k)) + 2;
                int i = 1;
                while (true)
                {
                    double a = Math.Truncate((double)((k + i) / hx));
                    double b = ((double)(k + i) / hx);

                    if (a == b)
                    {
                        hy = (int)(a + 1);
                        if ((hy - 1) * (hx - 1) < k) hy++;
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
            if (isX) Xk = arr.Select(i => Convert.ToBoolean(i)).ToArray();
            else Yn = arr.Select(i => Convert.ToBoolean(i)).ToArray();
        }
        
        public void CalcRKN()
        {
            k = Xk.Count();
            r = (int)Math.Ceiling(Math.Log(k, 2) + 1);
            n = k + r;
            CalcHxy();
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
    }
}
