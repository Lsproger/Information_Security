using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWT
{
    class Program
    {
        static void Main(string[] args)
        {

            BWT bwt = new BWT();
            bwt.Message = "ABACABA";

            string lastMatrixCollumn =  bwt.GetTransformString();
            Console.WriteLine("Tranansformed string "+lastMatrixCollumn);

            string restoredString = bwt.restoreOriginalString(lastMatrixCollumn);
            Console.WriteLine("restored message  " + restoredString);
        }
    }
}
