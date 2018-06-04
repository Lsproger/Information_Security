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
    public partial class Compress : Form
    {

        public Compress()
        {
            InitializeComponent();
        }
   

        private void compressButton_Click(object sender, EventArgs e)
        {            
            LZ77 lz = new LZ77();
            int result = 0;
            if (dictionarySizeBox.Text != String.Empty)
                if (Int32.TryParse(dictionarySizeBox.Text, out result))
                    lz.DictionarySize = result;
                else
                    MessageBox.Show("Ошибка в размере словаря");
            if (bufferSizeBox.Text != String.Empty)
                if (Int32.TryParse(bufferSizeBox.Text, out result))
                    lz.BufferSize = result;
                else
                    MessageBox.Show("Ошибка в размере буфера");
            dictionarySizeBox.Text = lz.DictionarySize.ToString();
            bufferSizeBox.Text = lz.BufferSize.ToString();
            string compressMessage = lz.Compression(messageBox.Text);
            informBox.Text = compressMessage;
            Decompress window = new Decompress(compressMessage, lz.DictionarySize);
            window.Show();

        }


        private void informBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dictionarySizeBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
