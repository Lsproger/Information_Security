using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Numerics;
using Org.BouncyCastle.Math;
using MD5;

namespace EGSA
{
    class Program
    {
        static void Main(string[] args)
        {


            ElGamalBean el = new ElGamalBean(new BigInteger(9473.ToString()),
                                             new BigInteger(3878.ToString()),
                                             new BigInteger(64.ToString()));



            /*Encryptor encryptor = new Encryptor(el, new BigInteger(9.ToString()));
            Console.WriteLine(encryptor.GetA());
            Console.WriteLine( encryptor.GetB());
           encryptor.Decrypt(encryptor.GetA(), encryptor.GetB());*/
            MessageEncryptor me = new MessageEncryptor();
            string message = "hello";
            List<string> encryptedSequence = me.EncryptMessage(message, el);


            string decryptedHash = me.MessageDecryptor(encryptedSequence,el);
            Console.WriteLine(decryptedHash);

            MD5.MD5 hash = new MD5.MD5();
            hash.Value = message;
            Console.WriteLine(hash.Value);

            if(decryptedHash == hash.Value)
            {
                Console.WriteLine("all right");
            }
            else
            {
                Console.WriteLine("error");
            }

            Encryptor e = new Encryptor(el);
            BigInteger a = e.GetA();
            BigInteger b = e.GetNDSAB(new BigInteger("104"));

            Encryptor encryptor = new Encryptor(el);
            BigInteger firstCheck = el.g.ModPow(new BigInteger("104"), el.p);
            BigInteger secondChech = encryptor.GetSecondCheck();



            
        }
    }
}
