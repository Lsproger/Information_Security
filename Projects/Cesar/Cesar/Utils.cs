using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cesar
{
    public static class Utils
    {
        static char[] word;
        static int step, n, f, a, b;
        public static void InputWord()
        {
            Console.WriteLine("Input text");
            word = Console.ReadLine().ToCharArray();
            Console.WriteLine("Input a");
            a = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input b");
            b = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Input first & last symbols of alphabet");
            f = Console.ReadKey(false).KeyChar;
            n = Console.ReadKey(false).KeyChar - f;
        }

        public static void EncryptWord()
        {
            for (int i = 0; i < word.Length; i++)
            {
                step = ((word[i] - f) * a) + b;
                word[i] = (char)(((word[i] - f + step)%(n+1))+f);
            }
        }

        public static void PrintWord()
        {
            string s = new string(word);
            Console.WriteLine("\nEncrypted word:\n{0}", s);
        }

        public static void DecryptWord()
        {
            for (int i = 0; i < word.Length; i++)
            {
                word[i] = (char)((((((word[i] - f) - b) / a) + n ) %n ) + f);
            }
        }
    }
}
