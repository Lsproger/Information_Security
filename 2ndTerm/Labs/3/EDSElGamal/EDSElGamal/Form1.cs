using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Text.RegularExpressions;

namespace EDSElGamal
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void generateEDS_Click(object sender, EventArgs e)
        {
            int P, G, X, K;
            BigInteger Y, A, B;

            do
            {
                P = rand.Next(0, 1000);
            } while (!IsSimple(P));

            G = rand.Next(0, P);
            X = rand.Next(0, P);

            Y = BigInteger.ModPow(G, X, P);

            outM.Text = inputMsg.Text;
            inP.Text = P.ToString();
            inG.Text = G.ToString();
            inX.Text = X.ToString();
            inY.Text = Y.ToString();
            outP.Text = P.ToString();
            outG.Text = G.ToString();
            outY.Text = Y.ToString();

            outA.Text = "";
            outB.Text = "";
            foreach(char c in inputMsg.Text)
            {
                do
                {
                    K = rand.Next(0, P - 1);
                } while (Nod(K, P - 1) != 1);

                A = BigInteger.ModPow(G, K, P);

                B = 0;
                while (c != (X * A + K * B) % (P - 1)) B++;

                outA.Text += A + " ";
                outB.Text += B + " ";
            }
        }

        private void checkEDS_Click(object sender, EventArgs e)
        {
            int counter = outM.Text.Length;
            bool isTrue = true;

            int cnt1 = 0, cnt2 = 0;
            foreach (Match m in Regex.Matches(outA.Text, " "))
                cnt1++;
            foreach (Match m in Regex.Matches(outB.Text, " "))
                cnt2++;
            if (cnt1 != outM.Text.Length || cnt2 != outM.Text.Length) isTrue = false;


            try
            {
                for (int i = 0; i < counter; i++)
                {
                    int A = Convert.ToInt32(outA.Text.Split(' ')[i]);
                    int B = Convert.ToInt32(outB.Text.Split(' ')[i]);

                    BigInteger Y = BigInteger.Parse(outY.Text);
                    BigInteger P = BigInteger.Parse(outP.Text);
                    BigInteger G = BigInteger.Parse(outG.Text);

                    if ((BigInteger.Pow(Y, A) * BigInteger.Pow(A, B)) % P != BigInteger.ModPow(G, outM.Text[i], P))
                    {
                        isTrue = false;
                    }
                }
            }
            catch (Exception) { isTrue = false; }

            if (isTrue) MessageBox.Show("ok");
            else MessageBox.Show("Достоверность не подтверждена");
        }


        private bool IsSimple(int p)
        {
            if (p < 2) return false;
            if (p == 2) return true;
            if (p % 2 == 0) return false;

            double limit = Math.Sqrt(p);

            for (uint i = 3; i <= limit; i += 2)
            {
                if ((p % i) == 0) return false;
            }

            return true;
        }

        private int Nod(int a, int b)
        {
            if (b == 0) return a;
            else return Nod(b, a % b);
        }
    }
}
