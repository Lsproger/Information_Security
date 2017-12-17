using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DES
{
    class Program
    {
        static void Main(string[] args)
        {
            DES des = new DES();
            des.InputData();
            des.Encrypt();
            des.DisplayResult(true);
            des.Decrypt();
            des.DisplayResult(false);
        }
    }
}
