using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ander
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            double n = Convert.ToDouble(textBoxN.Text);
            double s = Convert.ToDouble(textBoxS.Text);
            double sSi = Convert.ToDouble(textBoxSsi.Text); 
            double r = Convert.ToDouble(textBoxR.Text);
            double m = Convert.ToDouble(textBoxM.Text);
            double p = Convert.ToDouble(textBoxP.Text);
            double i = Math.Pow(n, s); 
            double E = (s+sSi)*8;
            double tn = (i * (E / (r * 1024))) / 2;
            double sr = (24*30*60*60 *r* 1024 * m) / (E * p);
            textBox2.Text = i.ToString(); // количесвто комбинаций пароля
            textBox3.Text = sr.ToString(); // формула андерсона
            if (p == 1)
            {
                tn = i * E / r / 1024; // безопасное время считается
            }
            TimeSpan t = TimeSpan.FromSeconds( tn );
            string answer = string.Format("{0:D2}d:{1:D2}h:{2:D2}m:{3:D2}s",
                t.Days,
                t.Hours, 
                t.Minutes, 
                t.Seconds
                );
            textBox1.Text = answer;           
            if (sr > i)
           { MessageBox.Show("Пароль будет взломан");
           }
           else {
               MessageBox.Show("Пароль не будет взломан");
           }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}