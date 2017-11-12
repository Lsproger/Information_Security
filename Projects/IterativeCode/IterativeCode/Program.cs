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
                try
                {
                    IterativeCode ic = new IterativeCode();
                    Console.WriteLine("Enter Xk");
                    ic.EnterWord(true);
                    ic.CalcRKN();
                    Console.WriteLine("k = {0}, x = {1}, y = {2}", ic.k, ic.hx, ic.hy);
                    ic.GenerateMatrixView(true);
                    ic.printM(ic.Hin);
                    Console.WriteLine("Enter Yn");
                    ic.EnterWord(false);
                    ic.GenerateMatrixView(false);
                    ic.printM(ic.Hout);
                    ic.CorrectError();
                    Console.WriteLine("Corrected Matrix");
                    ic.printM(ic.Hout);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
