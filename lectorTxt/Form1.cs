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


// Autor: José Manuel Valdez González
namespace lectorTxt
{
    public partial class Form1 : Form
    {
        string ruta;
        public Form1()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            StreamWriter escribir = new StreamWriter(ruta, true);
            try
            {
                escribir.WriteLine("Nombre: "+ name.Text);
                escribir.WriteLine("Apellido: " + lastname.Text);
                escribir.WriteLine("\n");
                name.Text = "";
                lastname.Text = "";
            }
            catch
            {
                MessageBox.Show("error");
            }
            escribir.Close();
        }

        private void execute_Click(object sender, EventArgs e)
        {
            StreamReader leer = new StreamReader(ruta);
            string linea;
            inputMessage.Clear();
            try
            {
                linea = leer.ReadLine();
                while(linea != null){
                    inputMessage.AppendText(linea+"\n");
                    linea = leer.ReadLine();
                }
            }
            catch{
                MessageBox.Show("error");
            }
            leer.Close();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Archivos txt(*.txt)|*.txt";
            open.Title = "Archivos txt";
            if (open.ShowDialog() == DialogResult.OK)
            {
                ruta = open.FileName;
                inputMessage.AppendText(ruta);
            }
            open.Dispose();
        }
    }
}
