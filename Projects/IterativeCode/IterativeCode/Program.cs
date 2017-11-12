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
                IterativeCode ic = new IterativeCode();
                ic.EnterWord(true);
                //   Console.WriteLine(ic.Xk.ToStr());
                ic.CalcRKN();
                ic.GenerateMatrixView();
                //    ic.CalcHxy();
                Console.WriteLine("k = {0}, x = {1}, y = {2}", ic.k, ic.hx, ic.hy);
                ic.printM(ic.H);
            }
        }
    }
}
