using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElGamal
{
    class Program
    {

        static int FindQ(int p)
        {
            int q = 5;

            while(Math.Pow(q, (p-1))%p != 1)
            {
                q++;
            }
            return q;
        }

        static void Main(string[] args)
        {

            Console.WriteLine(FindQ(71));

            Random rand = new Random();
            for(int i = 0; i<34; i++)
            {
                Console.WriteLine(rand.Next(1, 70));
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine((Math.Pow(21, 69)) % 71);
            Console.WriteLine(Math.Pow(21, 69));
            //string Alphabet = " АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            //Console.WriteLine("N = " + Alphabet.Length);
            //Console.WriteLine("Input simple p: ");
            //int p = Convert.ToInt32(Console.ReadLine());
            //int q = FindQ(p);

            //Random gen = new Random();
            //int x = gen.Next(1, p);

            //int y = (int)(Math.Pow(q, x) % p);

            //Console.WriteLine("Open key: (" + y + ", " + q + ", " + p + ")");
            //Console.WriteLine("Secret key: " + x);

            //Console.WriteLine("Input message: ");
            //string msg = Console.ReadLine();

            //#region EncryptionDecryption

            //Console.WriteLine("Исходный символ: Шифрование: Дешифрование:");
            //for(int j = 0; j<msg.Length; j++)
            //{
            //    int k = gen.Next(1, p - 1);
            //    int M = 0;
            //    for(int i = 0; i<Alphabet.Length; i++)
            //    {
            //        if (msg[j] == Alphabet[i]) M = i;
            //    }

            //    int a = (int)(Math.Pow(q, k) % p);
            //    int b = (int)((Math.Pow(y, k) * M) % p);


            //    int E = (int)((b * Math.Pow(a, p - 1 - x)) % p);

            //    Console.WriteLine(msg[j] + "\t" + "(" + a + ", " + b + ")\t" + msg[E]);
            //}

            //#endregion
        }
    }
}
