using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Permutator;

namespace IterativeCode
{
    public static class MyUt
    {
        public static IEnumerable<string> SplitS(this string text, int size)
        {
            string h = "00000000000000000000";
            for (var i = 0; i < text.Length; i += size)
            {
                if (Math.Min(size, text.Length - i) == size)
                yield return text.Substring(i, size);
                else
                yield return text.Substring(i, text.Length - i) + h.Substring(0, size - (text.Length - i));
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            for (;;)
            {
                //  try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Enter your word, davai");
                    string word = Console.ReadLine();
                    Console.WriteLine("Enter block size:");
                    int blockSize = Int32.Parse(Console.ReadLine());
                    List<string> subWords = word.SplitS(blockSize).ToList<string>();
                    List<bool[]> EncWords = new List<bool[]>();
                    List<IterativeCode> Codes = new List<IterativeCode>();
                    IterativeCode tmp;
                    foreach (var e in subWords)
                    {
                        tmp = new IterativeCode(true, e);
                        Codes.Add(tmp);
                        Codes.Last().Encode();
                        Console.WriteLine(Codes.Last().Xn.ToStr());
                        EncWords.Add(Codes.Last().Xn);
                    }
                    int n = EncWords[0].Length;
                    Permutator.Permutator p = new Permutator.Permutator(EncWords, n);
                    bool[] permutatedWord = p.Permutate();
                    Console.WriteLine("Permutated word: \n{0}", permutatedWord.ToStr());
                    Console.WriteLine("Input indexes to reverce bits");
                    int[] ind = Console.ReadLine().Split(' ').Select(i=> Int32.Parse(i)).ToArray();
                    for (int i = 0; i < ind.Count(); i++)
                    {
                        permutatedWord[ind[i]] = !permutatedWord[ind[i]];
                    }
                    Console.WriteLine("Permutated word with errors:\n{0}", permutatedWord.ToStr());
                    Console.WriteLine("Depermutated word:");
                    List<bool[]> depWords = p.Depermutate(permutatedWord);
                    foreach (var e in depWords) Console.WriteLine(e.ToStr());
                    Console.WriteLine("Corrected vsya eta hueta");
                    for (int i = 0; i < depWords.Count;i++)
                    {
                        Codes[i].EnterWord(depWords[i]);
                        Codes[i].Decode();
                        Console.WriteLine(Codes[i].Yn.ToStr());
                    }




                    //    List<bool[]> l = new List<bool[]>();
                    //    //IterativeCode ic = new IterativeCode();
                    //    Console.WriteLine("Enter Xk:");
                    //    // i.EnterWord(true);
                    //    ic.CalcRKN();
                    //    Console.WriteLine("\nr = {0}; k = {1}; n = {2};", ic.r, ic.k, ic.n);
                    //    ic.CalcXn();

                        //    Permutator.Permutator p = new Permutator.Permutator(l, ic.k, ic.r);
                        //    bool[] r = p.Permutate();
                        //    Console.WriteLine("Permutated res:\n{0}", r.ToStr());
                        //    Console.WriteLine("\nXn = {0}", ic.Xn.ToStr());
                        //    Console.WriteLine("\nXk in matrix:");
                        //    ic.printM(ic.GenerateMatrixView(ic.Xk));
                        //    Console.WriteLine("\nXn in matrix:");
                        //    ic.printM(ic.GenerateMatrixViewWithCheckSymbols(ic.Xk, ic.R));
                        //    ic.GenerateHelpMatrix();
                        //    Console.WriteLine("\nHelp matrix: ");
                        //    ic.printM(ic.H);
                        //    Console.WriteLine("Enter Yn");
                        //    ic.EnterWord(false);
                        //    ic.GetYkYr();
                        //    Console.WriteLine("\nYn = {0}\nYk = {1}\nYr = {2}", ic.Yn.ToStr(), ic.Yk.ToStr(), ic.Yr.ToStr());
                        //    ic.CalcS();
                        //    Console.WriteLine("\nYr = {0}\n_Yr = {1}\nS = {2}", ic.Yr.ToStr(), ic._Yr.ToStr(), ic.S.ToStr());
                        //    ic.CorrectError();
                        //    Console.WriteLine("E = {0}\nCorrected Yn = {1}", ic.E.ToStr(), ic.Yn.ToStr());
                        //}
                        //catch (Exception e)
                        //{
                        //    Console.WriteLine(e.Message);
                        //}

                }
            }

        }
    }
}
