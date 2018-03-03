using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permutator
{
    public class Permutator
    {
        List<bool[]> L = new List<bool[]>();
        int n, k, r;
        public Permutator(List<bool[]> l, int k, int r)
        {
            L = l;
            this.k = k;
            this.r = r;
            n = k + r;
        }

        public bool[] Permutate()
        {
            bool[] tmp = new bool[L.Count];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < L.Count; j++)
                {
                    tmp[(i*L.Count)+j] = L[j][i];
                }
            }
            return tmp;
        }

        public List<bool[]> Depermutate(bool[] input)
        {
            List<bool[]> res = new List<bool[]>();
            int b = input.Length / n;
            for (int i = 0; i < b; i++)
            {
                bool[] t = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    t[j] = input[(j*b)+i];
                }
                res.Add(t);
            }
            return res;
        }
    }
}
