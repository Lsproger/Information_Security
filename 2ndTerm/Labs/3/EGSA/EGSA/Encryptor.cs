using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Numerics;
using Org.BouncyCastle.Math;

namespace EGSA
{
    class Encryptor
    {
        public ElGamalBean bean { get; set; }

        public Encryptor(ElGamalBean bean)
        {
            this.bean = bean;
        }

        public BigInteger GetA()
        {

            return bean.g.ModPow(bean.k, bean.p);        
            
        }

         public BigInteger GetB(BigInteger message)
        {
            /* BigInteger bi = message.Subtract(bean.x.Multiply(this.GetA())).Multiply(bean.k);

             return bi.Negate().ModInverse(bean.p.Subtract(BigInteger.One));  */
            return bean.y.Pow(Convert.ToInt32(bean.k.ToString())).Multiply(message).Mod(bean.p);
        }

        public BigInteger GetNDSAB(BigInteger message)
        {
            BigInteger bi = message.Subtract(bean.x.Multiply(GetA())).Negate();

            BigInteger modInverced = bean.k.ModInverse(bean.p.Subtract(BigInteger.One));

            return bi.Multiply(modInverced);
        }

       /* public void CheckSignature()
        {
            Console.WriteLine(bean.y.Pow(Convert.ToInt32(GetA().ToString())).Multiply(GetA().Pow(Convert.ToInt32(GetB().ToString()))).Mod(bean.p));
            
            Console.WriteLine(bean.g.ModPow(new BigInteger(message.ToString()),bean.p));
        }*/

        public int Decrypt(BigInteger a, BigInteger b)
        {
           return Convert.ToInt32(b.Multiply(a.Pow(Convert.ToInt32(bean.p.Subtract(BigInteger.One).Subtract(bean.x).ToString()))).Mod(bean.p).ToString());
        }

        public BigInteger GetSecondCheck()
        {
            return bean.y.Pow(Convert.ToInt32(GetA().ToString())).Multiply(GetA().Pow(Convert.ToInt32(GetB(new BigInteger("104")).ToString()))).Mod(bean.p);
        }
    }
}
