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
using System.Runtime.InteropServices;

namespace EDITOR_DE_TEXTO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

        }     

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
                MessageBox.Show(saveFileDialog1.FileName.ToString());
                richTextBox1.Text = "";
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ubicacion = openFileDialog1.FileName;
                string leer = File.ReadAllText(ubicacion);
                richTextBox1.Text = leer;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog1.FileName))
                {

                    StreamWriter textonew = File.CreateText(saveFileDialog1.FileName);
                    textonew.Write(richTextBox1.Text);
                    textonew.Flush();
                    textonew.Close();

                }
                else
                {
                   StreamWriter textonew = File.CreateText(saveFileDialog1.FileName);
                    textonew.Write(richTextBox1.Text);
                    textonew.Flush();
                    textonew.Close();


                }
                
            }
            
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        public  int[] SearchString(string str, string pat, int cadenasize)
        {
            List<int> retVal = new List<int>();
            int m = pat.Length;
            int n = str.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pat, m, ref badChar);

            int s = 0;
            while (s <= (n - m))
            {
                int j = m - 1;

                while (j >= 0 && pat[j] == str[s + j])
                    --j;

                if (j < 0)
                {
                    retVal.Add(s);
                    s += (s + m < n) ? m - badChar[str[s + m]] : 1;
                }
                else
                {
                    s += Math.Max(1, j - badChar[str[s + j]]);
                }
            }
            if (retVal.Count() >= 1 )
            {
                MessageBox.Show("Se encontraron:  " +retVal.Count() +"  de coincidencias");
                for (int x = 0; x < retVal.Count(); ++x)
                {
                    richTextBox1.Select(retVal[x], cadenasize);
                    richTextBox1.SelectionColor = Color.Aqua;

                }
                panel2.Visible = false;

            }
            else
            {
                MessageBox.Show("No se encontraron resultados");
            }
            
            
           
            
            return retVal.ToArray();

        }

        private static void BadCharHeuristic(string str, int size, ref int[] badChar)
        {
            int i;

            for (i = 0; i < 256; i++)
                badChar[i] = -1;

            for (i = 0; i < size; i++)
                badChar[(int)str[i]] = i;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(Search.Text != "")
            {
                string cadena = Search.Text;
                int cadenasize = cadena.Length;
                int n = richTextBox1.Text.Length;
                string texto = richTextBox1.Text;
                char[] txt = texto.ToCharArray();
                char[] cad = cadena.ToCharArray();

                int [] value = SearchString(richTextBox1.Text, cadena, cadenasize);
                

            }
            else
            {
                MessageBox.Show("Escribe algo");
            }
            
        }
    }
}