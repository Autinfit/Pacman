using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading; // PARA EL EVENTO DEL DESPACHADOR DE TIEMPO, IMPORTANTE IMPORTAR ESTE TIPO DE LIBRERÍA DE WINDOWS MEDIANTE PARALELISMO Y CONCURRENCIA.

namespace Comecocos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // CREAREMOS VARIABLES DENTRO DE ESTA CLASE (VENTANA PRINCIPAL DEL JUEGO CREADO).

        DispatcherTimer temporizador = new DispatcherTimer(); // AGREGAREMOS EL TEMPORIZADOR EN EL JUEGO.

        bool izquierdaTrue, derechaTrue, abajoTrue, arribaTrue; // VARIABLES POSITIVAS AL MOVERSE EN DISTINTAS DIRECCIONES UN PERSONAJE (PACMAN) DEL JUEGO.
        bool izquierdaFalse, derechaFalse, abajoFalse, arribaFalse; // VARIABLES NEGATIVAS (PAUSADAS) AL MOVERSE EN DISTINTAS DIRECCIONES UN PERSONAJE (PACMAN) DEL JUEGO.

        int velocidad = 8; // LA VELOCIDAD DEL PERSONAJE (PACMAN) SERÁ EN 8.

        Rect pacmanColision; // EL PERSONAJE SE DETECTARÁ MEDIANTE COLISIONES DE ALTO IMPACTO HACIA LAS PAREDES, FANTASMAS Y MONEDAS DEL JUEGO.

        int velocidadFantasma = 10; // VELOCIDAD DE CUALQUIER FANTASMA DEL JUEGO.
        int limiteMovimientosFantasma = 160; // LÍMITE DE MOVIMIENTOS DE UN FANTASMA DEL JUEGO.
        int movimientosActualesFantasma; // NÚMERO ACTUAL DEL LÍMITE DE MOVIMIENTOS DE UN FANTASMA DEL JUEGO.
        int puntuacion = 0; // PUNTUACIÓN INICIAL DEL JUEGO.

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            // ESTE ES UN EVENTO DEL WPF AL PRESIONAR UNA TECLA DEL TECLADO.

            // SI EL PERSONAJE VA HACIA LA IZQUIERDA EN EL JUEGO.

            if (e.Key == Key.Left && izquierdaFalse == false)
            {
                // SI SE PRESIONÓ LA TECLA DE IZQUIERDA Y LA VARIABLE "izquierdaFalse" ESTÁ EN FALSO.

                derechaTrue = arribaTrue = abajoTrue = false; // EL RESTO DE LAS DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.
                derechaFalse = arribaFalse = abajoFalse = false; // EL RESTO DE LAS RESTRICCIONES DE DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.

                izquierdaTrue = true; // EL PERSONAJE MOVERÁ HACIA LA IZQUIERDA.

                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2); // GIRA AL PERSONAJE MEDIANTE ESTE ALGORITMO HACIA LA IZQUIERDA.
            }

            // SI EL PERSONAJE VA HACIA LA DERECHA EN EL JUEGO.

            if (e.Key == Key.Right && derechaFalse == false)
            {
                // SI SE PRESIONÓ LA TECLA DE DERECHA Y LA VARIABLE "derechaFalse" ESTÁ EN FALSO.

                derechaTrue = arribaTrue = abajoTrue = false; // EL RESTO DE LAS DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.
                derechaFalse = arribaFalse = abajoFalse = false; // EL RESTO DE LAS RESTRICCIONES DE DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.

                derechaTrue = true; // EL PERSONAJE MOVERÁ HACIA LA DERECHA.

                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2); // GIRA AL PERSONAJE MEDIANTE ESTE ALGORITMO HACIA LA DERECHA.
            }

            // SI EL PERSONAJE VA HACIA ARRIBA EN EL JUEGO.

            if (e.Key == Key.Up && arribaFalse == false)
            {
                // SI SE PRESIONÓ LA TECLA DE ARRIBA Y LA VARIABLE "arribaFalse" ESTÁ EN FALSO.

                derechaTrue = abajoTrue = izquierdaTrue = false; // EL RESTO DE LAS DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.
                derechaFalse = abajoFalse = izquierdaFalse = false; // EL RESTO DE LAS RESTRICCIONES DE DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.

                arribaTrue = true; // EL PERSONAJE MOVERÁ HACIA ARRIBA.

                pacman.RenderTransform = new RotateTransform(-90, pacman.Width / 2, pacman.Height / 2); // GIRA AL PERSONAJE MEDIANTE ESTE ALGORITMO HACIA ARRIBA.
            }

            // SI EL PERSONAJE VA HACIA ABAJO EN EL JUEGO.

            if (e.Key == Key.Down && abajoFalse == false)
            {
                // SI SE PRESIONÓ LA TECLA DE ABAJO Y LA VARIABLE "abajoFalse" ESTÁ EN FALSO.

                arribaTrue = derechaTrue = izquierdaTrue = false; // EL RESTO DE LAS DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.
                arribaFalse = derechaFalse = izquierdaFalse = false; // EL RESTO DE LAS RESTRICCIONES DE DIRECCIONES BOOLEANAS VAN A ESTAR EN FALSO.

                abajoTrue = true; // EL PERSONAJE MOVERÁ HACIA ABAJO.

                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2); // GIRA AL PERSONAJE MEDIANTE ESTE ALGORITMO HACIA ABAJO.
            }

        }
    }
}
