using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace EGSA
{
    class MessageEncryptor
    {
        public List<string> EncryptMessage(string message, ElGamalBean el)
        {
            Encryptor encryptor = new Encryptor(el);
            MD5.MD5 hash = new MD5.MD5();
            hash.Value = message;
            List<string> encryptedSequence = new List<string>();
            foreach (char item in hash.Value)
            {
                int messageSymbolAtInt = (int)item;

                StringBuilder aAndB = new StringBuilder();
                aAndB.Append(encryptor.GetA());
                aAndB.Append('|');
                aAndB.Append(encryptor.GetB(new BigInteger(messageSymbolAtInt.ToString())));
                encryptedSequence.Add(aAndB.ToString());
            }

            return encryptedSequence;
        }

        public String MessageDecryptor(List<string> encryptedSequence, ElGamalBean el)
        {
            Encryptor encryptor = new Encryptor(el);
            StringBuilder sb = new StringBuilder();
            foreach (var item in encryptedSequence)
            {
                string[] aAndB = item.Split('|');
                BigInteger a = new BigInteger(aAndB[0]);
                BigInteger b = new BigInteger(aAndB[1]);

                sb.Append(Convert.ToChar(encryptor.Decrypt(a, b)));
            }
            return sb.ToString();
        }
    }
}
