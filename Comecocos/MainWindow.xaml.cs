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

using System.Windows.Threading; // PARA EL EVENTO DEL DESPACHADOR DE TIEMPO, ES MUY IMPORTANTE IMPORTAR ESTE TIPO DE LIBRERÍA DE WINDOWS MEDIANTE PARALELISMO Y CONCURRENCIA.

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

        int velocidad = 5; // PARA PODER TENER MAYOR FACILIDAD AL JUEGO, LA VELOCIDAD DEL PERSONAJE (PACMAN) SERÁ EN 5.

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
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Imagenes/pacman.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO AMARILLO A ESTE PERSONAJE.
            pacman.Fill = pacmanImage; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush blinky = new ImageBrush(); // EDITAREMOS A BLINKY, EL FANTASMA ROJO.
            blinky.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Imagenes/red.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO ROJO A ESTE PERSONAJE.
            redGuy.Fill = blinky; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush clyde = new ImageBrush(); // EDITAREMOS A CLYDE, EL FANTASMA NARANJA.
            clyde.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Imagenes/orange.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO NARANJO A ESTE PERSONAJE.
            orangeGuy.Fill = clyde; // LO COLOREAMOS A ESTE PERSONAJE EN EL RECTÁNGULO CORRESPONDIENTE TAL CUÁL COMO SE HABÍA DECLARADO EN ESTA SECCIÓN.

            ImageBrush pinky = new ImageBrush(); // EDITAREMOS A PINKY EL FANTASMA ROSADO.
            pinky.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Imagenes/pink.jpg")); // LO VAMOS A IMPORTAR EN EL RECTÁNGULO AMARILLO A ESTE PERSONAJE.
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
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) + velocidad); // AÑADE SU VELOCIDAD YA DECLARADO EN UNA VARIABLE ANTERIORMENTE.
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

                izquierdaFalse = true;
                izquierdaTrue = false;
            }

            if (derechaTrue && Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {
                // Si este personaje se está moviendo hacia la derecha, la posición de éste es mayor que el ancho de la ventana principal, entonces detén el movimiento hacia la derecha.

                derechaFalse = true;
                derechaTrue = false;
            }

            // SI ES QUE EL PERSONAJE DETECTA COLISIÓN CON CUALQUIER OBSTÁCULO DEL JUEGO...

            pacmanColision = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);

            // ALGORITMO SENCILLO MEDIANTE LA ITERACIÓN "foreach" PARA DETECTAR COLISIONES CON EL RECTÁNGULO YA CREADO ANTERIORMENTE.

            foreach(var x in MyCanvas.Children.OfType<Rectangle>())
            {
                // CREAREMOS OTROS RECTÁNGULOS DISTINTOS DEL PERSONAJE PARA DETECTAR COLISIONES CON EL MISMO MENCIONADO ANTERIORMENTE.

                Rect colisiones = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                // SI ES QUE ENCUENTRA ALGUNA PARED DEL JUEGO MEDIANTE UNA ETIQUETA YA MENCIONADA EN EL WPF SE DETECTARÁ AHÍ MISMO CON EL PERSONAJE.

                if ((string)x.Tag == "wall")
                {
                    // EL PERSONAJE QUE MOVERÁ HACIA LA IZQUIERDA DETECTARÁ COLISIONES CON LA PARED.

                    if (izquierdaTrue == true && pacmanColision.IntersectsWith(colisiones))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        izquierdaFalse = true;
                        izquierdaTrue = false;
                    }

                    // EL PERSONAJE QUE MOVERÁ HACIA LA DERECHA DETECTARÁ COLISIONES CON LA PARED.

                    if (derechaTrue == true && pacmanColision.IntersectsWith(colisiones))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        derechaFalse = true;
                        derechaTrue = false;
                    }

                    // EL PERSONAJE QUE MOVERÁ HACIA ABAJO DETECTARÁ COLISIONES CON LA PARED.

                    if (abajoTrue == true && pacmanColision.IntersectsWith(colisiones))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        abajoFalse = true;
                        abajoTrue = false;
                    }

                    // EL PERSONAJE QUE MOVERÁ HACIA LA ARRIBA DETECTARÁ COLISIONES CON LA PARED.

                    if (arribaTrue == true && pacmanColision.IntersectsWith(colisiones))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) + 10);
                        arribaFalse = true;
                        arribaTrue = false;
                    }
                }
                
                // ANALIZA SI ES QUE HAY RECTÁNGULOS QUE TIENEN ETIQUETADOS COMO MONEDAS DEL JUEGO AL DETECTAR COLISIONES.

                if ((string) x.Tag == "coin")
                {
                      
                    // SI DENTRO DE ESTA CONDICIÓN COLISIONA CON UNA DE LAS MONEDAS DEL JUEGO, ENTONCES HABRÍA VISIBILIDAD.

                    if (pacmanColision.IntersectsWith(colisiones) && x.Visibility == Visibility.Visible)
                    {
                            
                        // CAMBIARÁ A CADA MONEDA COMO OCULTA.

                        x.Visibility = Visibility.Hidden;

                        // AÑADE A 1 LA PUNTUACIÓN.

                        puntuacion++;
                    }
                }

                // SI ES UN FANTASMA SEGÚN LA ETIQUETA DEL WPF...

                if ((string) x.Tag == "ghost")
                {
                    
                    // DETECTA COLISIONES DEL PERSONAJE CON EL FANTASMA SI ES QUE SE CUMPLE CON ESTA CONDICIÓN.

                    if (pacmanColision.IntersectsWith(colisiones))
                    {
                            
                        // EL PERSONAJE COLISIONÓ CON CUALQUIER FANTASMA DEL JUEGO LLAMANDO A LA FUNCIÓN DE FINALIZAR UNA PARTIDA.

                        GameOver("BASTA PIRAÑA, RENUNCIA AHORA YA!!!!!!!!");
                    }

                    // SUPONGA A CLYDE COMO UN FANTASMA DE PRUEBA.

                    if (x.Name.ToString() == "redGuy")
                    {
                            
                         // MUEVE UN RECTÁNGULO DE MANERA HORIZONTAL HACIA LA IZQUIERDA EN LA PANTALLA DEL JUEGO.

                         Canvas.SetLeft(x, Canvas.GetLeft(x) - velocidadFantasma);
                    }

                    else
                    {
                         // EN CASO CONTRARIO LO HACE AL REVÉS (HACIA LA DERECHA).

                         Canvas.SetLeft(x, Canvas.GetLeft(x) + velocidadFantasma);
                    }

                    // REDUCE A 1 EL NÚMERO DE PASOS DEL FANTASMA.

                    movimientosActualesFantasma--;

                    // SI EL LÍMITE DE MOVIMIENTOS ACTUALES DEL FANTASMA ES MENOR QUE 1...

                    if (movimientosActualesFantasma < 1)
                    {
                            
                        // REINICIA EL NÚMERO ACTUAL DE MOVIMIENTOS DEL FANTASMA

                        movimientosActualesFantasma = limiteMovimientosFantasma;

                        // INVIERTE LA VELOCIDAD DE MOVIMIENTOS DEL FANTASMA.

                        velocidadFantasma = -velocidadFantasma;
                    }
                }

                // SI EL JUGADOR OBTUVO TODAS LAS MONEDAS EN TOTAL DEL JUEGO...

                if (puntuacion == 85)
                {
                    // MUESTRA UN MENSAJE DE ÉXITO MEDIANTE MÉTODO DE FINALIZACIÓN DE LA PARTIDA.

                    GameOver("HAS GANADO LA PARTIDA, PRESIONA 'OK' PARA CONTINUAR!!!!!");
                }
            }
        }

        // MÉTODO PARA FINALIZAR LA PARTIDA.

        private void GameOver(string mensaje)
        {
            // DENTRO DE ESTA FUNCIÓN, SE DESPLEGARÁ UN MENSAJE DICIENDO QUE FINALIZA LA PARTIDA.

            temporizador.Stop(); // PARALIZA EL TEMPORIZADOR.
            MessageBox.Show(mensaje, "PACMAN"); // MENSAJE AL FINALIZAR LA PARTIDA.

            // CUANDO EL JUGADOR CLIQUEA EL BOTÓN "ok" EN LA CAJA DE MENSAJE.

            // REINICIA LA APLICACIÓN.

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); // REALIZA EL PROCESO DE CERRAR LA APLICACIÓN.
            Application.Current.Shutdown(); // CIERRA EL PROGRAMA O LA APLICACIÓN.
        }
    }
}
