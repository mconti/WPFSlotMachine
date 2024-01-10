using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
using System.Windows.Threading;

using SlotMachineLib;

namespace conti.maurizio.WPFSlotMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SlotMachine s = new SlotMachine("Mauri");

        private List<string> carteDaGioco = new List<string>
        {
            "AssoCuori", "DueCuori", "TreCuori", "QuattroCuori", "CinqueCuori",
            "SeiCuori", "SetteCuori", "OttoCuori", "NoveCuori", "DieciCuori",
            "JackCuori", "DonnaCuori", "ReCuori"
            // Aggiungi altre carte e suite a seconda delle tue esigenze
        };

        // Dimensioni di ciascuna carta nell'immagine complessiva
        private const int larghezzaCarta = 78;
        private const int altezzaCarta = 78;

        private int indiceCartaCorrente = 0;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InizializzaTimer();
        }

        private void InizializzaTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1); // Imposta l'intervallo a 1 secondo
            timer.Tick += TimerTick;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            MostraProssimaCarta();
        }

        private void MostraProssimaCarta()
        {
            if (indiceCartaCorrente < carteDaGioco.Count)
            { 
                string nomeCarta = carteDaGioco[indiceCartaCorrente];
                lettera.Text = nomeCarta[0].ToString();

                MostraImmagineCarta(nomeCarta);
                indiceCartaCorrente++;
                timer.Stop();
            }
            else
            {
                // Fine delle carte, puoi fermare il timer o eseguire un'altra azione
                timer.Stop();
            }
        }

        private void MostraImmagineCarta(string nomeCarta)
        {
            string percorsoImmagine = $"./Carte/Carte.png"; // Assicurati di avere un percorso corretto
            BitmapImage bitmapImmagineComplessiva = new BitmapImage(new Uri(percorsoImmagine, UriKind.Relative));

            // Calcola le coordinate di inizio per lo slicing
            int colonna = indiceCartaCorrente % (bitmapImmagineComplessiva.PixelWidth / larghezzaCarta);
            int riga = indiceCartaCorrente / (bitmapImmagineComplessiva.PixelHeight / altezzaCarta);

            int X = colonna * larghezzaCarta;
            int Y = riga * altezzaCarta;

            stato.Text = $"" +
                $"Indice: {indiceCartaCorrente}\n" +
                $"Colonna: {colonna}\n" +
                $"Riga: {riga}\n" +
                $"X: {X}\n" +
                $"Y: {Y}\n" +
                $"Width: {bitmapImmagineComplessiva.PixelWidth}"+
                $"Height: {bitmapImmagineComplessiva.PixelHeight}";

            // Esegui lo slicing dell'immagine complessiva
            /*CroppedBitmap cartaCroppata = new CroppedBitmap(

                bitmapImmagineComplessiva,
                // Colonna  Larghezza  X
                //    0        78      0
                //    1        78      78
                //    2        78      156
                //    3        78      234
                //    4        78      312

                new Int32Rect(
                    X,
                    Y,
                    larghezzaCarta,           // Width
                    altezzaCarta              // Height
                )
            );
            ;
            */

            // Vecchio sistema...
            //ImageBrush brush = new ImageBrush(new BitmapImage(new Uri(percorsoImmagine, UriKind.Relative)));

            //ImageBrush brush = new ImageBrush(cartaCroppata);
            //brush.Stretch = Stretch.None;
            //cartaCanvas.Background = brush;
        }

        private void AvviaAnimazioneButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}

