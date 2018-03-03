using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesar
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                try
                {
                    Utils.InputWord();
                    Utils.EncryptWord();
                    Utils.PrintWord();
                    Utils.DecryptWord();
                    Utils.PrintWord();
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                }
            }
        }
    }
}
