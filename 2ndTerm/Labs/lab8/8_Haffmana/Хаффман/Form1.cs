using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Хаффман
{
    public partial class Form1 : Form
    {
        public Dictionary<String, String> res = new Dictionary<String, String>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            res.Clear();
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            string Message = string.Empty;
            if (!(textBox1.Text == string.Empty))
                Message = textBox1.Text;
            else
                MessageBox.Show("Please, input text!");
            Dictionary<String, String> ret = new Dictionary<String, String>();
            char[] chars = Message.ToCharArray().Distinct().ToArray();
            Dictionary<String, double> count = chars.ToDictionary(item => item.ToString(), item => 0d);
            foreach (var item in Message)
            {
                count[item.ToString()] += 1d / Message.Length;
            }
            var sorted = (from pair in count
                          orderby pair.Value descending, pair.Key ascending
                          select pair)
                         .ToDictionary(item => item.Key, item => item.Value);
            foreach (var elem in sorted)
            {
                richTextBox1.Text += "p(" + elem.Key + ")"+ " = " + Math.Round(elem.Value, 3) + "\n";
            }
            System.Console.WriteLine();

            //построение дерева по вероятностям
            //var res = new Dictionary<String, String>();

            if (res.Count == 0)
                res = sorted.ToDictionary(elem => elem.Key, elem => "");

            var temp = sorted.ToDictionary(elem => elem.Key, elem => elem.Value);

            while (temp.Count > 1)
            {
                var res1 = new Dictionary<string, double>();
                var xxx = new Dictionary<string, double>();
                var tempres = new Dictionary<string, double>();

                var temp1 = (from pair in temp
                             orderby pair.Value descending
                             select pair);

                if (temp1.Count() > 1)
                {
                    foreach (var elem in temp1.Take(temp1.Count() - 2))
                    {
                        tempres.Add(elem.Key, elem.Value);
                    }
                    var arr = temp1.Skip(temp1.Count() - 2).ToArray();
                    xxx.Add(arr[0].Key, arr[0].Value);
                    xxx.Add(arr[1].Key, arr[1].Value);

                    var temp_xxx = (from pair in xxx
                                    orderby pair.Value descending
                                    select pair);
                    var arr_xxx = temp_xxx.ToArray();
                    //объединение наименьших вероятностей
                    tempres.Add(arr_xxx[0].Key + arr_xxx[1].Key, arr_xxx[0].Value + arr_xxx[1].Value);
                    foreach (var chr in arr_xxx[0].Key)
                    {
                        //верхняя ветка
                        res[chr.ToString()] = "1" + res[chr.ToString()];
                    }

                    foreach (var chr in arr_xxx[1].Key)
                    {
                        //нижняя ветка
                        res[chr.ToString()] = "0" + res[chr.ToString()];
                    }
                }
                else
                    foreach (var elem in temp)
                    {
                        tempres.Add(elem.Key, elem.Value);
                    }

                res1 = (from pair in tempres
                        orderby pair.Value descending
                        select pair)
                            .ToDictionary(item => item.Key, item => item.Value);

                richTextBox1.Text += " " + "\n";

                foreach (var elem in res1)
                {
                    richTextBox1.Text += "p(" + elem.Key + ")"+ " = " + Math.Round(elem.Value, 3) + "\n";

                   
                }
                temp = res1.ToDictionary(elem => elem.Key, elem => elem.Value);
            }
            //вывод результата
            richTextBox2.Text = "";
            foreach (var elem in res)
            richTextBox2.Text += "p(" + elem.Key +")" + " = " + elem.Value + "\n";
            //кодовая последовательность
            var sb = new System.Text.StringBuilder();
            foreach (var elem in Message)
                sb.Append(res[elem.ToString()]);

            var enc = sb.ToString();
            var sb1 = new System.Text.StringBuilder();
            var revdict = res.ToDictionary(elem => elem.Value, elem => elem.Key);

            var current = "";

            foreach (var elem in enc)
            {
                current += elem;
                if (revdict.ContainsKey(current))
                {
                    sb1.Append(revdict[current]);
                    current = "";
                }
            }
            textBox2.Text = sb.ToString();
            textBox5.Text = sb.ToString();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool flag = false;
            var enc = textBox2.Text;
            var sb1 = new System.Text.StringBuilder();
            var revdict = res.ToDictionary(elem => elem.Value, elem => elem.Key);

            var current = "";

            foreach (var elem in enc)
            {
                flag = false;
                current += elem;
                if (revdict.ContainsKey(current))
                {
                    flag = true;
                    sb1.Append(revdict[current]);
                    current = "";
                }
            }
            if (flag == false)
            {
                MessageBox.Show("Архив поврежден", "Опачки", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            textBox3.Text = sb1.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
