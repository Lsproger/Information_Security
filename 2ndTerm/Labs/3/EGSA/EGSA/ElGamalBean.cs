using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Numerics;
using Org.BouncyCastle.Math;

namespace EGSA
{
    class ElGamalBean
    {
        public BigInteger p { get; set; }
        public BigInteger g { get; set; }
        public BigInteger x { get; set; }
        public BigInteger y
        {
            get
            {
                return g.ModPow(x, p);
            }
        }
        public BigInteger k
        {
            get
            {
                return new BigInteger(GetSimpleNumber(Convert.ToInt32(this.p.ToString())).ToString());
            }
        }

        public ElGamalBean(BigInteger p, BigInteger g, BigInteger x)
        {
            this.p = p;
            this.g = g;
            this.x = x;
        }

        public void GetPublicKey(ref BigInteger p,
                                 ref BigInteger g,
                                 ref BigInteger y)
        {
            p = this.p;
            g = this.g;
            y = this.y;
        }

        public void GetX(ref BigInteger x)
        {
            x = this.x;
        }

        public static int NOD(int a, int b)
        {
            if (a == b)
                return a;
            else
                if (a > b)
                return NOD(a - b, b);
            else
                return NOD(b - a, a);
        }
        static int GetSimpleNumber(int p)
        {
            Random rand = new Random();
            for (;;)
            {
                int a = rand.Next(1, p - 1);
                if (NOD(a, p - 1) == 1)
                    return a;                    
            }      
        }

    }
}
