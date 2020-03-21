using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AhorcadoCSharp
{
    public partial class Form1 : Form
    {

        String palabraOculta = "CETYS";
        bool partidaTerminada = false;
        int numeroFallos = 0;

        private void dibujaPalabraConGuiones()
        {
            String auxiliar = "";
            for (int i = 0; i < palabraOculta.Length; i++)
            {
                auxiliar += "_ ";
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
                case 6: imagen_ahorcado.Image = Properties.Resources.ahorcado_fin; break;
                case -100: imagen_ahorcado.Image = Properties.Resources.acertastetodo; break;
                default: imagen_ahorcado.Image = Properties.Resources.ahorcado_fin; break;
            }
        }

        private String eligePalabra()
        {
            String[] listaPalabras = { "HOLA", "CETYS", "BORREGUITO", "BABYYODA", "MANDO" };
            Random aleatorio = new Random(); //Variable aleatoria va a elegir una palabra al azahar
            int posicion = aleatorio.Next(listaPalabras.Length);
            return listaPalabras[posicion].ToUpper();
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
            palabraOculta = eligePalabra();
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
    }
}
