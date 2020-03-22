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

namespace AhorcadoCSharp
{
    public partial class Form1 : Form
    {

        String palabraOculta = "CETYS";
        String descripcionOculta = "";
        bool partidaTerminada = false;
        int numeroFallos = 0;
        String goodEnd = "YOU WERE RIGTH";
        String badEnd = "REBEL SCUM!";

        private void dibujaPalabraConGuiones()
        {
            String auxiliar = "";
            for (int i = 0; i < palabraOculta.Length; i++)
            {
                if (palabraOculta[i] != ' ')
                {
                    auxiliar += "_ ";
                }
                else
                {
                    auxiliar += "  ";
                }
            }
            palabra_con_guiones.Text = auxiliar;
        }

        private void dibujaImagen()
        {
            switch (numeroFallos)
            {
                case 0: imagen_ahorcado.Image = Properties.Resources.ahorcado_0; break;
                case 1: imagen_ahorcado.Image = Properties.Resources.ahorcado_1; break;
                case 2: imagen_ahorcado.Image = Properties.Resources.ahorcado_2; break;
                case 3: imagen_ahorcado.Image = Properties.Resources.ahorcado_3; break;
                case 4: imagen_ahorcado.Image = Properties.Resources.ahorcado_4; break;
                case 5: imagen_ahorcado.Image = Properties.Resources.ahorcado_5; break;
                case 6: imagen_ahorcado.Image = Properties.Resources.ahorcado_fin;
                    mensaje_final.Text = badEnd; break;
                case -100: imagen_ahorcado.Image = Properties.Resources.acertastetodo;
                    mensaje_final.Text = goodEnd;
                    break;
                default: imagen_ahorcado.Image = Properties.Resources.ahorcado_fin; break;
            }
        }

        private Dictionary<string, string> eligePalabra()
        {
            Dictionary<string, string> listaPalabras = new Dictionary<string, string>();
            string filePathKeys = @"..\..\Resources\TextFiles\keys.txt";
            string filePathValues = @"..\..\Resources\TextFiles\values.txt";
            List<string> keyLines = new List<string>();
            try
            {
                keyLines = File.ReadAllLines(filePathKeys).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine("Archivo no encontrado");
            }
            List<string> valueLines = new List<string>();
            try
            {
                valueLines = File.ReadAllLines(filePathValues).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Archivo no encontrado");
            }
            for (int i = 0; i < keyLines.Count; i++)
            {
                listaPalabras.Add(keyLines[i].ToUpper(), valueLines[i]);
            }
            
            Random aleatorio = new Random(); //Variable aleatoria va a elegir una palabra al azahar
            int posicion = aleatorio.Next(keyLines.Count-1);
            Dictionary<string, string> palabraOculta = new Dictionary<string, string>()
            {
                {listaPalabras.ElementAt(posicion).Key, listaPalabras.ElementAt(posicion).Value }
            };
            return palabraOculta;
        }



        

       private void chequeaLetra(String letra)
        {
            letra = letra.ToUpper();

            if (palabraOculta.Contains(letra))
            {
                
                char letraPulsada = letra[0];

                for(int i=0; i<palabraOculta.Length; i++)
                {
                    if(palabraOculta[i] == letraPulsada)
                    {
                        palabra_con_guiones.Text = palabra_con_guiones.Text.Remove(2 * i, 1)
                    .Insert(2 * i, letra);
                    }
                }
                dibujaImagen();
            }
            else
            {
                numeroFallos++;
                if (numeroFallos == 6){
                    partidaTerminada = true;
                }
                dibujaImagen();
            }

            if (!palabra_con_guiones.Text.Contains('_'))
            {
                numeroFallos = -100;
                dibujaImagen();
            }
        }



        public Form1()
        {
            InitializeComponent();
            dibujaImagen();
            palabraOculta = eligePalabra().ElementAt(0).Key;
            descripcionOculta = eligePalabra().ElementAt(0).Value;
            dibujaPalabraConGuiones();
        }



        private void letraPulsada(object sender, EventArgs e)
        {

            Button miBoton = (Button)sender;
            String letra = miBoton.Text;
            letra = letra.ToUpper();

            if (!partidaTerminada)
            {
                chequeaLetra(letra);
            }

            miBoton.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(descripcionOculta);
        }
    }
}
