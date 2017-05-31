using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDesktopApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dr = openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 3)
            {
                RidoCRC.cBinaryFileStream file = new RidoCRC.cBinaryFileStream();
                file.File = textBox1.Text;
                RidoCRC.cCRC32 crc = new RidoCRC.cCRC32();
                var res = crc.GetFileCrc32(file);
                label1.Text = res.ToString();

                DamienG.Security.Cryptography.Crc32 crc32 = new DamienG.Security.Cryptography.Crc32();
                var f = File.OpenRead(textBox1.Text);
                var res2 = crc32.ComputeHash(f);
                label1.Text += " " + GetBigEndianUInt32(res2);
            }
        }

        protected static UInt32 GetBigEndianUInt32(byte[] bytes)
        {
            if (bytes.Length != 4)
                throw new ArgumentOutOfRangeException("bytes", "Must be 4 bytes in length");

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
