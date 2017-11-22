using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IterativeCode
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
              //  try
                {
                    IterativeCode i = new IterativeCode();
                    Console.WriteLine("Enter Xk:");
                    i.EnterWord(true);
                    i.CalcRKN();
                    Console.WriteLine("\nr = {0}; k = {1}; n = {2};", i.r, i.k, i.n);
                    i.CalcXn();
                    Console.WriteLine("\nXn = {0}", i.Xn.ToStr());
                    Console.WriteLine("\nXk in matrix:");
                    i.printM(i.GenerateMatrixView(i.Xk));
                    Console.WriteLine("\nXn in matrix:");
                    i.printM(i.GenerateMatrixViewWithCheckSymbols(i.Xk, i.R));
                    i.GenerateHelpMatrix();
                    Console.WriteLine("\nHelp matrix: ");
                    i.printM(i.H);
                    Console.WriteLine("Enter Yn");
                    i.EnterWord(false);
                    i.GetYkYr();
                    Console.WriteLine("\nYn = {0}\nYk = {1}\nYr = {2}", i.Yn.ToStr(), i.Yk.ToStr(), i.Yr.ToStr());
                    i.CalcS();
                    Console.WriteLine("\nYr = {0}\n_Yr = {1}\nS = {2}", i.Yr.ToStr(), i._Yr.ToStr(), i.S.ToStr());
                    i.CorrectError();
                    Console.WriteLine("E = {0}\nCorrected Yn = {1}", i.E.ToStr(), i.Yn.ToStr());
                    // IterativeCode ic = new IterativeCode();
                    // Console.WriteLine("Enter Xk");
                    // ic.EnterWord(true);
                    // ic.CalcRKN();
                    // Console.WriteLine("k = {0}, x = {1}, y = {2}", ic.k, ic.rr, ic.rc);
                    // ic.GenerateMatrixView(true);
                    // ic.printM(ic.Hin);
                    // ic.GetHelpMatrix();
                    // ic.printM(ic.H);
                    // Console.WriteLine("rr = {0}, rc={1}", ic.H.Length, ic.H[0].Length);
                    // Console.WriteLine("Enter Yn");
                    // ic.EnterWord(false);
                    // ic.GenerateMatrixView(false);
                    // ic.printM(ic.Hout);
                    // ic.CalcS();
                    // Console.WriteLine("Yr = {0}; _Yr = {1}; Sindrom = {2}",ic.Yr.ToStr(), ic._Yr.ToStr(), ic.S.ToStr());
                    //// ic.CorrectError();
                    //Console.WriteLine("Corrected Matrix");
                    //ic.printM(ic.Hout);
                }
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //}

            }
        }
    }
}
