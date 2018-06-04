using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EllGamal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int fun(int p, int a, int b)
        {
            int s = 1;
            for (int i = 1; i <= b; i++)
            {

                s = (s * a) % p;
            }
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1, str2;
            Random myrandom = new Random();
            int p = 127, g = 3, da, ca, db, cb, k;
            ca = myrandom.Next(p);//A
            da = fun(p,g,ca);
            
            cb = myrandom.Next(p);//B
            db = fun(p, g, cb);

            str1 = textBox1.Text;
            textBox2.Text = "";
            textBox3.Text = "";
        
            for (int i = 0; i < str1.Length; i++)
            {
                k = (int)str1[i];
                //1kadam
                k = (k * fun(p, db, ca))%p;
                textBox2.Text = textBox2.Text + k.ToString()+".";
                //2kadam
                k = (k*fun(p, da, p-1-cb))%p;
                textBox3.Text = textBox3.Text + (char)k;

            }
            
 
        }
    }
}
