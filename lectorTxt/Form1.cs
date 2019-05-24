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
                escribir.WriteLine(text.Text);
                escribir.Close();
                rule();
                text.Text = "";
            }
            catch
            {
                MessageBox.Show("error");
            }
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

        private void rule()
        {
            StreamWriter escribir = new StreamWriter(ruta, true);
            bool complete = true, valid = true, temp = false;
            string estados = "initial";
            foreach (char caracter in text.Text)
            {
                int i;
                if (int.TryParse(caracter.ToString(), out i))
                {
                    switch (estados)
                    {
                        case "initial":
                            escribir.WriteLine("0d: " + caracter);
                            estados = "Efinal1";
                            break;
                        case "Efinal1":
                            escribir.WriteLine("1d: " + caracter);
                            break;
                        case "e2":
                            escribir.WriteLine("2d: " + caracter);
                            temp = false;
                            estados = "Efinal2";
                            break;
                        case "Efinal2":
                            escribir.WriteLine("3d: " + caracter);
                            break;
                    }
                }
                else
                {
                    if (valid)
                    {
                        switch (caracter)
                        {
                            case '.':
                                escribir.WriteLine("1 . : " + caracter);
                                estados = "e2";
                                temp = true;
                                valid = false;
                                break;
                            case 'e':
                                escribir.WriteLine("1E: " + caracter);
                                valid = false;
                                temp = true;
                                estados = "e2";
                                break;
                            case 'E':
                                escribir.WriteLine("1E: " + caracter);
                                valid = false;
                                temp = true;
                                estados = "e2";
                                break;
                            default:
                                escribir.WriteLine("Input incorrecto.");
                                complete = false;
                                break;
                        }
                    }
                    else
                    {
                        escribir.WriteLine("Input incorrecto.");
                        complete = false;
                    }
                }
            }
            if (complete && temp == false)
            {
                escribir.WriteLine("Completado :D");
            }
            else
            {
                escribir.WriteLine("FALLO :c");
            }
            escribir.Close();
        }
    }
}
