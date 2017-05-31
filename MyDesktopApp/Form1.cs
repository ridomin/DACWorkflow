using Interop.RidoCRC;
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
                label1.Text = CalculateCRCWithCOM(textBox1.Text).ToString();

                label2.Text = CalculateCRCWithDotNet(textBox1.Text).ToString();
            }
        }

        private uint CalculateCRCWithDotNet(string fileName)
        {
            uint res = 0;
            try
            {
                DamienG.Security.Cryptography.Crc32 crc32 = new DamienG.Security.Cryptography.Crc32();
                var f = File.OpenRead(fileName);
                var res2 = crc32.ComputeHash(f);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(res2);

                res = BitConverter.ToUInt32(res2, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
        }

        private Int32 CalculateCRCWithCOM(string fileName)
        {
            int res = 0;
            try
            {
                cBinaryFileStream file = new cBinaryFileStream();
                file.File = fileName;
                cCRC32 crc = new cCRC32();
                res =  crc.GetFileCrc32(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return res;
            
        }
    }
}
