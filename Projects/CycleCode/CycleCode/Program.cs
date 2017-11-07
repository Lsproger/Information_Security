using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCode
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                try
                {
                    CycleCode cc = new CycleCode();
                    Console.WriteLine("Input  Xk:");
                    cc.EnterWord(true);
                    if (!cc.isCode) Console.WriteLine("In binary: {0}", cc.Xk.ToStr());
                    cc.CalcRKN();
                    cc.CalcXn();
                    Console.WriteLine("n = {0}; k = {1}; r = {2}\nXn: = {3}", cc.n, cc.k, cc.r, cc.Xn.ToStr());
                    Console.WriteLine("Check: {0}", cc.XnDivGx().ToStr());
                    Console.WriteLine("Input Yn:");
                    cc.EnterWord(false);
                    cc.CalcSindrom();
                    Console.WriteLine("Sindrom: {0}", cc.Sr.ToStr());

                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
        }
    }
}
