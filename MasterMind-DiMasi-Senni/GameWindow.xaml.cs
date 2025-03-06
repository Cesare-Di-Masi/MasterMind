using MastermindLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
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
using System.Xml.Linq;

namespace MasterMind_DiMasi_Senni
{
    //Prima cosa : determinare la dimensione e spaziatura di ogni rettangolo in maniera tale da definire la generazione del rettangolo (1 volta per partita)
    //Seconda cosa: generare il rettangolo (penso chiamato dalla funzione di generazione dei bottoni)
    //terza cosa: determinare dimensione e spaziatura dei bottoni per definire la generazione del rettangolo (1 volta deve essere richiamata come funzione per partita)
    //quarta cosa: generare i bottoni con nmi che abbiano ovviamente un senso con anche dei valori di default
    //quinta cosa: creare una funzione che richiami il passo 2 e 4
    //sesta cosa: spostare poi il tentativo in avanti nello spazio (possibile idea di generare tutto quanto prima che inizi la partita e poi spostare i valori)
    //giocare

    public partial class GameWindow : Window
    {

        int startVertical = 89;
        int startHorizontal = 445;

        GameManager currentGame;
        //codebreaker, il gioco al momento non richiede alcun dato, però ci servono le funzioni
        CodeBreaker currentCodeBreaker;

        //lista di bottoni per il gioco
        List<Button> selectColoursList;

        //tutti i tentativi precedenti
        List<List<Ellipse>> allAttempts = new List<List<Ellipse>> ();

        double recHeight = 0, recWidth = 0;
        //✔
        public GameWindow(GameManager game)
        {
            InitializeComponent();
            currentGame = game;
            calculateRectangle();
            calculateButton();
            selectColoursList = new List<Button>();
            //richiamo la funzione di generazione dei bottoni solo all'inizio della partita
            generateCodeSolver();
        }

        //dimensioni definite dal campo di gioco
        private int _maxWidth = 335;
        private int _maxHeight = 491;
        private int _tipWidth = 255;

        //counter per aiutarci a definire i nomi dei cerchi che devono comparire
        private int _currentAttempt = 0;

        //spaziatura e dimensioni dei rettangoli
        private int _rectangleHeight, _rectangleSpacing;
        //spaziatura e dimensione dei bottoni e cerchi 
        private int _buttonSize, _buttonSpacing;

        //✔
        //calcolo le dimensioni che ogni rettangolo deve avere e la spaziatura fra di loro
        public void calculateRectangle()
        {
            _rectangleHeight = _maxHeight/currentGame.NAttempts+1;
            _rectangleSpacing = _rectangleHeight / currentGame.NAttempts - 1;
        }

        //✔
        //calcolo le dimensioni di ogni bottone che deve essere all'interno del rettangolo e le spaziature, con i limiti dell'altezza del rettangolo
        public void calculateButton()
        {
            _buttonSize= _maxWidth/currentGame.CodeLength+1;
            _buttonSpacing = _buttonSize / currentGame.CodeLength - 1;

            if(_buttonSize > _rectangleHeight)
            {
                _buttonSize = _rectangleHeight;
                _buttonSpacing = _buttonSize / currentGame.CodeLength - 1;
            }

        }

        //✔
        //genero la "zona" di gioco dove creo il codice da provare
        public void generateCodeSolver()
        {
            for (int i = 0; i < currentGame.CodeLength; i++)
            {
                //fare il generatore di bottoni
                Button button = generateButton(i);
                selectColoursList.Add(button);
            }
        }

        //posizione di inizio per la generazione dei blocchi
        private int _currentPosVertical = 89;
        private int _currentPosHorizontal = 445;

        //✔
        //nuovo turno quindi vai a resettare il tutto e costruisci a schermo il tentativo precedente
        private void newTurn(object sender,EventArgs e)
        {
            generateRectangle(_currentAttempt);
            _currentPosVertical -= _rectangleHeight;

            for(int i=0; i<currentGame.CodeLength;i++)
            {
                //genero la nuova riga di cerchi (tentativo precedente)
                generateEllipse(_currentAttempt, i, selectColoursList[i]);
                //mi muovo a destra di tot spazio
                _currentPosHorizontal += _buttonSpacing;
            }
            _currentPosHorizontal = startHorizontal;
        }

        //✔
        private void resetButton()
        {
            for (int i = 0; i < currentGame.CodeLength; i++)
            {
                //resetto la board dei bottoni al colore rosso
                selectColoursList[i].Content = "1";
                ShowButtonColours(selectColoursList[i]);
            }
        }

        //✔
        private void nextColour(object sender, EventArgs e)
        {
            if (sender is System.Windows.Controls.Button) 
            {
                Button btn = sender as Button;

                if (btn == null)
                    btn.Content = 0;

                int curr = int.Parse((string)btn.Content);

                Colours cl = Colours.Red + curr;

                currentCodeBreaker.NextColour(ref cl);

                btn.Content = (int)cl;

                ShowButtonColours(btn);

            }
        }
        //✔
        private void previousColour(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = sender as Button;

                if (btn == null)
                    btn.Content = 0;

                int curr = int.Parse((string)btn.Content);

                Colours cl = Colours.Red+curr;

                currentCodeBreaker.PreviousColour(ref cl);

                btn.Content = (int)cl;

                ShowButtonColours(btn);

            }
        }

        //✔
        private void ShowButtonColours(Button btn)
        {
            int cur = int.Parse((string)btn.Content);
            switch (cur) 
            {
                case 0:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                    break;
                case 1:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255));
                    break;
                case 2:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 0));
                    break;
                case 3:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0));
                    break;
                case 4:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 0, 128));
                    break;
                case 5:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 165, 0));
                    break;
                case 6:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 192, 203));
                    break;
                case 7:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 255));
                    break;
                case 8:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
                    break;
                case 9:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 36, 0));
                    break;
                case 10:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 128));
                    break;
                case 11:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(75, 0, 130));
                    break;
                default:
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    break;

            }
            
        }

        private void ShowEllipseColour(Ellipse ell)
        {
            int cur = int.Parse((string)ell.);
            switch (cur)
            {
                case 0:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
                    break;
                case 1:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255));
                    break;
                case 2:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 0));
                    break;
                case 3:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0));
                    break;
                case 4:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 0, 128));
                    break;
                case 5:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 165, 0));
                    break;
                case 6:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 192, 203));
                    break;
                case 7:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 255));
                    break;
                case 8:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
                    break;
                case 9:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 36, 0));
                    break;
                case 10:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 128, 128));
                    break;
                case 11:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(75, 0, 130));
                    break;
                default:
                    ell.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                    break;

            }
        }

        //gestione delle coordinate da fare, non dovrebbe essere troppo complesso
        private System.Windows.Shapes.Rectangle generateRectangle(int verticalPos)
        {
            throw new NotImplementedException();
        }

        //questi sono i bottoni per la zona di gioco
        //gestione delle coordinate da fare, non dovrebbe essere troppo complesso
        private Button generateButton(int currentPos)
        {
           throw new NotImplementedException();
        }

        //btnToCopy mi dice i valori da copiare (colore principalmente)
        private Ellipse generateEllipse(int verticalPos, int horizontalPos,Button btnToCopy)
        {
            throw new NotImplementedException();
            //chiama la seguente funzione : ShowEllipseColour(ellipse ell)
            //in modo da dargli il colore
        }


        private void generateRectangles()
        {
           Button btn = new Button();
            btn.Content = "bottone generato da codice";
           
        }

    }




    //primo tentativo (probabilmente fallimentare) per la generazione del turno


   
    /*
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
