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

            // EN INSTANTES...
        }
    }
}
