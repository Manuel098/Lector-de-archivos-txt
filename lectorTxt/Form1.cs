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
            string estados = "initial", way = "";

            foreach (char caracter in text.Text)
            {
                int i;
                if (int.TryParse(caracter.ToString(), out i))
                {
                    switch (estados)
                    {
                        case "initial":
                            way = way + "0d: "+caracter;
                            estados = "Efinal1";
                            break;
                        case "Efinal1":
                            way = way + ", 1d: " + caracter;
                            break;
                        case "e2":
                            way = way + ", 2d: " + caracter;
                            temp = false;
                            estados = "Efinal2";
                            break;
                        case "Efinal2":
                            way = way + ", 3d: " + caracter;
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
                                way = way + ", 1p: punto";
                                estados = "e2";
                                temp = true;
                                valid = false;
                                break;
                            case 'e':
                                way = way + ", 1E: " + caracter;
                                valid = false;
                                temp = true;
                                estados = "e2";
                                break;
                            case 'E':
                                way = way + ", 1E: " + caracter;
                                valid = false;
                                temp = true;
                                estados = "e2";
                                break;
                            default:
                                way = way + ", Input incorrecto: " + caracter;
                                complete = false;
                                break;
                        }
                    }
                    else
                    {
                        way = way + ", Input incorrecto: " + caracter;
                        complete = false;
                    }
                }
            }
            if (complete && temp == false)
            {
                escribir.WriteLine(way);
                escribir.WriteLine("Completado es un digito :D");
                escribir.Close();
            }
            else
            {
                escribir.Close();
                ruleText();
            }
        }

        private void ruleText()
        {
            StreamWriter escribir = new StreamWriter(ruta, true);
            bool complete = true, valid = true, temp = false;
            string estados = "initial", way = "";

            foreach (char caracter in text.Text)
            {
                int i;
                if (char.IsLetter(caracter))
                {
                    switch (estados)
                    {
                        case "initial":
                            way = way + "0w: " + caracter;
                            estados = "Efinal1";
                            break;
                        case "Efinal1":
                            way = way + ", 5w: " + caracter;
                            break;
                        case "e6":
                            way = way + ", 6w: " + caracter;
                            temp = false;
                            estados = "Efinal2";
                            break;
                        case "Efinal2":
                            way = way + ", 7w: " + caracter;
                            break;
                    }
                }
                else
                {
                    if (int.TryParse(caracter.ToString(), out i))
                    {
                        switch (estados)
                        {
                            case "initial":
                                way = way + "0d: " + caracter;
                                estados = "Efinal1";
                                break;
                            case "Efinal1":
                                way = way + ", 5d: " + caracter;
                                break;
                            case "e6":
                                way = way + ", 6d: " + caracter;
                                temp = false;
                                estados = "Efinal2";
                                break;
                            case "Efinal2":
                                way = way + ", 7d: " + caracter;
                                break;
                        }
                    }
                    else
                    {
                        if (valid)
                        {
                            switch (caracter)
                            {
                                case '_':
                                    if (estados == "initial")
                                    {
                                        complete = false;
                                        break;
                                    }
                                    else
                                    {
                                        way = way + ", 5g: _";
                                        estados = "e6";
                                        temp = true;
                                        valid = false;
                                        break;
                                    }
                                default:
                                    way = way + ", Input incorrecto: " + caracter;
                                    complete = false;
                                    break;
                            }
                        }
                        else
                        {
                            way = way + ", Input incorrecto: " + caracter;
                            complete = false;
                        }
                    }
                }
            }
            if (complete && temp == false)
            {
                escribir.WriteLine(way);
                escribir.WriteLine("Completado es un texto :D");
            }
            else
            {
                escribir.WriteLine("La cadena es erronea");
            }
            escribir.Close();
        }
    }
}
