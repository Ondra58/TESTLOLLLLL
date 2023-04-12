using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp28
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int soucet = 0;
        int pocet = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream datovytok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter zapisovac = new BinaryWriter(datovytok, Encoding.GetEncoding("windows-1250"));
            BinaryReader ctenar = new BinaryReader(datovytok, Encoding.GetEncoding("windows-1250"));
            int pocetznamek = (int)numericUpDown3.Value;
            
            
            comboBox1.Items.Add(textBox1.Text + " " + textBox2.Text);
            for (int i = 0; i < pocetznamek; i++)
            {
                zapisovac.Write(numericUpDown1.Value);
                zapisovac.Write(numericUpDown2.Value);
                zapisovac.Write(textBox1.Text);
                zapisovac.Write(textBox2.Text);
                listBox1.Items.Add(numericUpDown2.Value + " s váhou " + numericUpDown1.Value);
                soucet += (int)numericUpDown2.Value;
                pocet++;
            }
            double prumer = (double)soucet / (double)pocet;
            label6.Text = prumer.ToString();
            datovytok.Close();
            /*while (ctenar.BaseStream.Position < ctenar.BaseStream.Length)
            {
                ctenar.BaseStream.Seek();
                comboBox1.Items.Add(ctenar.ReadString() + " " + ctenar.ReadString());
            }*/
            
            //BinaryReader ctenar = new BinaryReader(datovytok, Encoding.GetEncoding("windows-1250"));            
            //ctenar.BaseStream.Position = 0;

        }

    }
}
