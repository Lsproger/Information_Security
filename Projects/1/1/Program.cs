using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> txt = new Dictionary<int, int>();
            string path;
            int vsego = 0,I;
            for (;;)
            {
                Console.WriteLine("Vvedite put' k failu s rasshirenuem *.txt");
                path = Console.ReadLine();
                txt = SortText(path, ref vsego);
                foreach (var i in txt)
                {
                    Console.WriteLine((char)i.Key + "      " + i.Value);
                }
                Console.WriteLine("Vsego simvolov: " + vsego + "\nEntropiya alphavita = " + CalcEntr(txt, vsego) + 
                    "\nHosh uznat' skol'ko infi neset soobschenie s tvoei FIO?(y/n)");
                if (Console.ReadKey().KeyChar == 'y')
                {
                    Console.WriteLine("\nVvedite svoe FIO na English yazike:" );
                    string FIO = Console.ReadLine();
                    I =(int)(CalcEntr(txt, vsego) * FIO.Length);
                    Console.WriteLine("Ogo, vot eto da, vasha familiya soderzhit " + I + " bit's of  informaciya!\n And " + FIO.Length*8 + " bit's in ASCII" );
                }
            }
        }
        public static double CalcEntr(Dictionary<int, int> smbls, int vse)
        {
            double H = 0;
            double Pi;
            double sumi;
            foreach (var i in smbls)
            {
                Pi = (double)i.Value / (double)vse;
                sumi = Pi * Math.Log(Pi, 2);
                H -= sumi;
            }
            return H;
        }
        public static Dictionary<int, int> SortText(string path, ref int vse)
        {
            Dictionary<int, int> alph = new Dictionary<int, int>();
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int s_code;
                    while ((s_code = sr.Read()) != -1)
                    {
                        if (s_code > 64 && s_code < 91)
                        {
                            vse++;
                            if (alph.ContainsKey(s_code))
                                alph[s_code]++;
                            else alph.Add(s_code, 1);
                        }
                        if (s_code > 96 && s_code < 123)
                        {
                            vse++;
                            if (alph.ContainsKey(s_code - 32))
                                alph[s_code - 32]++;
                            else alph.Add(s_code - 32, 1);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Sore, net takogo puti ili nel'zya otkrit' fail\n");
            }
            return alph;
        }
    }
}
