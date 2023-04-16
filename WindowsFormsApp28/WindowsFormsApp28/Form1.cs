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
using System.Reflection.Emit;

namespace WindowsFormsApp28
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<int> znamky = new List<int>();
        List<int> vaha = new List<int>();
        List<long> pozice = new List<long>();
        List<string> jmeno = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {         
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {               
                FileStream datovytok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryReader ctenar = new BinaryReader(datovytok, Encoding.GetEncoding("windows-1250"));
                BinaryWriter zapisovac = new BinaryWriter(datovytok, Encoding.GetEncoding("windows-1250"));
                zapisovac.BaseStream.Position = zapisovac.BaseStream.Length;
                zapisovac.Write(znamky.Count);
                for (int i = 0; i < znamky.Count; i++)
                {
                    zapisovac.Write(znamky[i]);
                    zapisovac.Write(vaha[i]);
                }
                zapisovac.Write(textBox1.Text);
                zapisovac.Write(textBox2.Text);
                comboBox1.Items.Clear();                
                ctenar.BaseStream.Position = 0;
                while (ctenar.BaseStream.Position < ctenar.BaseStream.Length)
                {
                    pozice.Add(ctenar.BaseStream.Position);
                    int pomocna = ctenar.ReadInt32();
                    ctenar.BaseStream.Seek(pomocna * 8, SeekOrigin.Current);
                    string celejmeno = ctenar.ReadString() + " " + ctenar.ReadString();
                    jmeno.Add(celejmeno);
                    comboBox1.Items.Add(celejmeno);
                }
                ctenar.Close();
            }
            else
            {
                MessageBox.Show("Zadej jméno!", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int soucet = 0;
            int pocet = 0;
            double prumer;
            FileStream datovytok = new FileStream("seznam.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter zapisovac = new BinaryWriter(datovytok, Encoding.GetEncoding("windows-1250"));
            BinaryReader ctenar = new BinaryReader(datovytok, Encoding.GetEncoding("windows-1250"));
            ctenar.BaseStream.Position = pozice[jmeno.IndexOf(comboBox1.Text)];
            int pomocna = ctenar.ReadInt32();            
            for (int i = 0; i < pomocna; i++)
            {
                int znamka = ctenar.ReadInt32();
                int vaha = ctenar.ReadInt32();                
                soucet += znamka * vaha;
                pocet += vaha;
                listBox2.Items.Add("Známka: " + znamka + ", váha: " + vaha);
            }
            prumer = (double)soucet / (double)pocet; 
            label6.Text = "Průměr: " + prumer;
            if (prumer < 1.5)
            {
                MessageBox.Show("Vychází ti 1! Gratuluji!");
            }
            if (prumer >= 3.5 && prumer < 4.5)
            {
                MessageBox.Show("Vychází ti 4! Pozor!");
            }
            if (prumer >= 4.5)
            {                
                zapisovac.BaseStream.Position = ctenar.BaseStream.Position;
                zapisovac.Write("John Doe");
            }
            ctenar.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            znamky.Add((int)numericUpDown1.Value);
            vaha.Add((int)numericUpDown2.Value);
            listBox1.Items.Add("Známka: " + (int)numericUpDown1.Value + ", váha: " + (int)numericUpDown2.Value);
        }
    }
}
