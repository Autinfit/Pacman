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

            SetUp(); // LLAMADO DEL MÉTODO PARA IMPORTAR TODO DESDE UN ALGORITMO YA REALIZADO.
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

        // EN EL SIGUIENTE MÉTODO QUE SE MOSTRARÁ A CONTINUACIÓN SE REALIZARÁN LAS CONFIGURACIONES A LA INTERFAZ DEL JUEGO EN EL WPF.

        private void SetUp()
        {
            MyCanvas.Focus(); // LA ITERACIÓN "MyCanvas" HACE REFERENCIA A LA FUNCIÓN PRINCIPAL PARA LA VISUALIZACIÓN DE LA INTERFAZ DEL JUEGO EN WPF.

            // LUEGO, SE REALIZARÁN LAS CONFIGURACIONES DEL TEMPORIZADOR HACIA ESTA INTERFAZ.

            temporizador.Tick += GameLoop; // SE ENLAZA EN CONJUNTO CON EL MÉTODO DECLARADO PARA HACERLO FUNCIONAR EL TEMPORIZADOR EN UN INSTANTE.
            temporizador.Interval = TimeSpan.FromMilliseconds(20); // EL TIEMPO INSTANTÁNEO SE CONFIGURÓ EN 20 MILISEGUNDOS.
            temporizador.Start(); // INICIALIZA EL TEMPORIZADOR.
            movimientosActualesFantasma = limiteMovimientosFantasma; // AJUSTE DE MOVIMIENTOS PARA LOS FANTASMAS.

            // FINALMENTE, REALIZAREMOS LAS ÚLTIMAS CONFIGURACIONES A LOS PERSONAJES DEL JUEGO.

            ImageBrush pacmanImage = new ImageBrush(); // EDITAREMOS AL PACMAN.
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO AMARILLO A ESTE PERSONAJE.
            pacman.Fill = pacmanImage; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush blinky = new ImageBrush(); // EDITAREMOS A BLINKY, EL FANTASMA ROJO.
            blinky.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO ROJO A ESTE PERSONAJE.
            redGuy.Fill = blinky; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush clyde = new ImageBrush(); // EDITAREMOS A CLYDE, EL FANTASMA NARANJA.
            clyde.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/orange.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO NARANJO A ESTE PERSONAJE.
            orangeGuy.Fill = clyde; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush pinky = new ImageBrush(); // EDITAREMOS A PINKY EL FANTASMA ROSADO.
            pinky.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pink.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO AMARILLO A ESTE PERSONAJE.
            pinkGuy.Fill = pinky; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // MÉTODO DE COMPILACIÓN DE LA INTERFAZ DEL JUEGO EN WPF.

            txtScore.Content = "Puntuación: " + puntuacion; // EL CONTENIDO DEL TEXTO SE MOSTRARÁ EN LA VENTANA DE JUEGO EN WPF.

            // SE INICIALIZA EL PROGRAMA MOVIENDO AL PERSONAJE EN DISTINTAS DIRECCIONES.

            if (derechaTrue) // SI EL PERSONAJE VA HACIA LA DERECHA.
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + velocidad); // AÑADE SU VELOCIDAD YA DECLARADO EN UNA VARIABLE ANTERIORMENTE.
            }

            if (izquierdaTrue) // SI EL PERSONAJE VA HACIA LA IZQUIERDA.
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - velocidad); // AÑADE SU VELOCIDAD YA DECLARADO EN UNA VARIABLE ANTERIORMENTE PERO DE LO CONTRARIO HACIA AL OTRO LADO.
            }

            if (arribaTrue) // SI EL PERSONAJE VA HACIA ARRIBA.
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - velocidad); // AÑADE SU VELOCIDAD YA DECLARADO EN UNA VARIABLE ANTERIORMENTE PERO DE LO CONTRARIO HACIA AL OTRO LADO.
            }

            if (abajoTrue) // SI EL PERSONAJE VA HACIA ABAJO.
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + velocidad); // AÑADE SU VELOCIDAD YA DECLARADO EN UNA VARIABLE ANTERIORMENTE.
            }

            // FIN DE LOS MOVIMIENTOS DEL PERSONAJE.

            // INICIALMENTE, ESTE PERSONAJE VA A QUEDAR PARADO, PERO SIN MOVERSE POR NINGÚN MOTIVO.

            if (abajoTrue && Canvas.GetTop(pacman) + 80 > Application.Current.MainWindow.Height)
            {
                // Si este personaje se está moviendo hacia abajo, la posición de éste es mayor que la altura de la ventana principal, entonces detén el movimiento hacia abajo.

                abajoFalse = true;
                abajoTrue = false;
            }

            if (arribaTrue && Canvas.GetTop(pacman) < 1)
            {
                // Si este personaje se está moviendo y la posición de éste es menor que 1 entonces se detendrá el movimiento hacia arriba.

                arribaFalse = true;
                arribaTrue = false;
            }

            if (izquierdaTrue && Canvas.GetLeft(pacman) - 10 < 1)
            {
                // Si este personaje se está moviendo hacia la izquierda y la posición de éste es menor que 1 entonces se detendrá el movimiento hacia la izquierda.

                arribaFalse = true;
                arribaTrue = false;
            }

            if (derechaTrue && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {
                // Si este personaje se está moviendo hacia la derecha, la posición de éste es mayor que el ancho de la ventana principal, entonces detén el movimiento hacia la derecha.

                derechaFalse = true;
                derechaTrue = false;
            }

            // EN INSTANTES...
        }
    }
}
