using MastermindLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MasterMind_DiMasi_Senni
{
    /// <summary>
    /// Logica di interazione per GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        GameManager game;

        double recHeight = 0, recWidth = 0;

        public GameWindow(GameManager _game)
        {
            InitializeComponent();
            game = _game;
            
        }

        //Prima cosa : determinare la dimensione e spaziatura di ogni rettangolo in maniera tale da definire la generazione del rettangolo (1 volta per partita)
        //Seconda cosa: generare il rettangolo (penso chiamato dalla funzione di generazione dei bottoni)
        //terza cosa: determinare dimensione e spaziatura dei bottoni per definire la generazione del rettangolo (1 volta deve essere richiamata come funzione per partita)
        //quarta cosa: generare i bottoni con nmi che abbiano ovviamente un senso con anche dei valori di default
        //quinta cosa: creare una funzione che richiami il passo 2 e 4
        //sesta cosa: spostare poi il tentativo in avanti nello spazio (possibile idea di generare tutto quanto prima che inizi la partita e poi spostare i valori)
        //giocare

    }




    //primo tentativo (probabilmente fallimentare) per la generazione del turno


    /*private void generateRectangles()
        {
            double maxHeight = 600; // Get current height of the window
            double totalAvailableHeight = maxHeight - (game.NAttempts - 1) * 1.0; // Space left after spacing
            double rectHeight = Math.Max(10, totalAvailableHeight / game.NAttempts); // Ensure a minimum height
            double rectWidth = rectHeight * 1.5; // Aspect ratio 1.5:1 (adjust as needed)

            for (int i = 0; i < game.NAttempts; i++)
            {
                Rectangle rect;
       
                rect = new Rectangle
                {
                    Fill = Brushes.Crimson,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };               

                rect.Width = rectWidth;
                rect.Height = rectHeight;

                double y = i * (rectHeight + 1.0); // Adjust position

                Canvas.SetLeft(rect, 50); // Keep a fixed X position
                Canvas.SetTop(rect, y);
            }
        }

        private void generateButtons()
        {
            // Determine the number of buttons that will fit in the available width
            int n_x = game.CodeLength;  // Number of buttons in a single row

            // Calculate the size of each button
            double maxSize = Math.Min(recWidth, recHeight) / (2 * n_x);  // Adjusted based on the width
            maxSize = Math.Max(10, maxSize);  // Ensure the size is not smaller than 10

            // Calculate the spacing between buttons
            double spacing_x = (recWidth - n_x * maxSize) / (n_x - 1);

            // Generate buttons and position them in a single row
            for (int i = 0; i < game.CodeLength; i++)
            {
                double posX = i * (maxSize + spacing_x);  // Calculate X position in the row
                Button button = createButton(i, maxSize);
                button.Margin = new Thickness(posX, 0, 0, 0);  // Position the button in a single line
            }
        }

        private Button createButton(int name, double size)
        {
            return new Button
            {
                Name = $"btnTurn{game.NAttempts}pos{name}",
                Width = size,
                Height = size,
                Background = Brushes.Crimson,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Content = 1,
                FontWeight = FontWeights.Bold,
                FontSize = size / 3,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Style = FindResource("RoundButtonStyle") as Style
            };
        }*/



}
