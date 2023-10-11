using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Formulario_Csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.TextChanged += ValidarNombre;
            textBox2.TextChanged += ValidarApellidos;
            textBox3.TextChanged += ValidarTelefono;
            textBox4.TextChanged += ValidarEstatura;
            textBox5.TextChanged += ValidarEdad;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombres = textBox1.Text;
            string apellidos = textBox2.Text;
            string telefono = textBox3.Text;
            string estatura = textBox4.Text;
            string edad = textBox5.Text;

            string genero = "";
            if (radioButton1.Checked)
            {
                genero = "Hombre";
            }
            else if (radioButton2.Checked)
            {
                genero = "Mujer";
            }

            if (esenterovalido(edad) && esdecimalvalido(estatura) && esenterode10digitos(telefono) && estextovalido(nombres) && estextovalido(apellidos))
            {
                string datos = $"Nombre:{nombres}\r\nApellidos:{apellidos}\r\nTelefonos: {telefono}\r\nEstatura {estatura}:\r\nEdad: {edad}\r\nGenero: {genero}";

                string rutaArchivo = ("C:/Users/re543/Videos/Rt/formulario.txt");

                bool archivoExiste = File.Exists(rutaArchivo);
                if (archivoExiste == false)
                {
                    File.WriteAllText(rutaArchivo, datos);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(rutaArchivo))
                    {
                        if (archivoExiste)
                        {
                            writer.WriteLine();
                        }
                        writer.WriteLine(datos);
                    }
                }
                MessageBox.Show("Datos Guardados con éxito: \n\n" + datos, "información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, ingrese datos validos en los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool esenterovalido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }

        private bool esdecimalvalido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out resultado);
        }


        private bool esenterode10digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado);
        }

        private bool estextovalido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");
        }
        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!esenterovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una edad válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!esdecimalvalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una estatura válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;
            if (input.Length < 10)
            {
                if (!esenterode10digitos(input))
                {
                    MessageBox.Show("Por favor, ingrese una estatura válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
            else if (!esenterode10digitos(input))
            {
                MessageBox.Show("Por favor, ingrese una estatura válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!estextovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido (solo letras y espacios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }
        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!estextovalido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese apellidos válidos (solo letras y espacios)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox4.Clear();
            textBox3.Clear();
            textBox5.Clear();
        }
    }
}
