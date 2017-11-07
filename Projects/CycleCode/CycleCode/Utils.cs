using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCode
{
    public static class Utils
    {
        public static bool[] Mul(this bool[] A, int n) //Сдвиг влево на n символов
        {
            bool[] res = new bool[A.Count() + n];
            Array.Copy(A, res, A.Count());
            return res;
        }

        public static bool[] Div(this bool[] A, bool[] B)
        {
            if (B == null) throw new Exception("Too many symbols in the input word!");
            if (A.Count() - B.Count() >= 0)
                A = A.XOR(B).Reduce().Div(B);
            return A;
        }


        public static bool[] XOR(this bool[] A, bool[] B)
        {
            for (int i = 0; i < B.Length; i++)
                A[i] = A[i] ^ B[i];
            return A;
        }

        private static bool[] Reduce(this bool[] A)
        {
            bool[] res;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i])
                {
                    res = new bool[A.Length - i];
                    Array.Copy(A, i, res, 0, res.Length);
                    return res;
                }
            }
            return new bool[A.Length-1];
        }

        public static string ToStr(this bool[] A)
        {
            string s = "";
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i]) s += "1";
                else s += "0";
            }
            return s;
        }
    }
}
