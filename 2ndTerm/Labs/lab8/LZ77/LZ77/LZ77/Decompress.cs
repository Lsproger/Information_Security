using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LZ77
{
    public partial class Decompress : Form
    {
        LZ77 lz;
        public Decompress(string compressMessage, int dictSize)
        {
            InitializeComponent();
            messageBox.Text = compressMessage;
            lz = new LZ77() { DictionarySize = dictSize };
        }

        private void decompressButton_Click(object sender, EventArgs e)
        {
            try
            {
                string result = lz.Decompression(messageBox.Text);
               // informBox.AppendText($"Восстановление:\n");
                informBox.Text=result;
            }
            catch (ArgumentException m)
            {
                MessageBox.Show("Ошибка \n{m.Message}");
            }
            catch(IndexOutOfRangeException m)
            {
                MessageBox.Show("Ошибка \n{m.Message}");
            }
        }

        private void messageBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void informBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Decompress_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
