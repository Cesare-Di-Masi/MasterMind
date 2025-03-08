using MastermindLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        int _tipsSize = 86;

        GameManager currentGame;
        //codebreaker, il gioco al momento non richiede alcun dato, però ci servono le funzioni
        CodeBreaker currentCodeBreaker;

        //lista di bottoni per il gioco
        List<Button> selectColoursList;

        //tutti i tentativi precedenti
        List<List<Ellipse>> allAttempts;

        List<List<Button>> allTips = new List<List<Button>> ();

        double recHeight = 0, recWidth = 0;
        //✔
        public GameWindow(GameManager game)
        {
            InitializeComponent();
            currentGame = game;
            calculateRectangle();
            calculateButton();
            selectColoursList = new List<Button>();

            allAttempts = new List<List<Ellipse>>(game.NAttempts);
            allTips = new List<List<Button>>(game.NAttempts);

            //richiamo la funzione di generazione dei bottoni solo all'inizio della partita
            generateCodeSolver();
        }

        //dimensioni definite dal campo di gioco
        private int _maxWidth = 335;
        private int _maxHeight = 491;

        //counter per aiutarci a definire i nomi dei cerchi che devono comparire
        private int _currentAttempt = 0;

        //spaziatura e dimensioni dei rettangoli
        private int _rectangleHeight, _rectangleSpacing;
        //spaziatura e dimensione dei bottoni e cerchi 
        private int _buttonSize, _buttonSpacing;

        private int _currentPosVertical = 89;
        private int _currentPosHorizontal = 445;

        //dati per mostrare la soluzione
        private int solveCodeVerticalPosition=20;
        private int solveCodeHorizontalPosition=445;
        private int solveCodeSize = 20;


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
            System.Windows.Point solverPoint = new System.Windows.Point(_currentPosHorizontal,_currentPosVertical);
            for (int i = 0; i < currentGame.CodeLength; i++)
            {
                //fare il generatore di bottoni
                Button button = generateButton(i,solverPoint);
                selectColoursList.Add(button);
            }
        }

        private void btnMandaCombinazione_Click(object sender, RoutedEventArgs e)
        {
            newTurn();
        }

        private Colours[] buttonToCode()
        {
            Colours[] code = new Colours[currentGame.CodeLength];

            for(int i = 0;i < currentGame.CodeLength;i++)
            {
                code[i] = Colours.Red +int.Parse((string)selectColoursList[i].Content);
            }
            return code;
        }


        //✔
        //nuovo turno quindi vai a resettare il tutto e costruisci a schermo il tentativo precedente
        private void newTurn()
        {
            GameStatus status = currentGame.EndOfTheTurn(buttonToCode());

            if (status == GameStatus.Playing)
            {
                System.Windows.Point ellypsePoint = new System.Windows.Point(_currentPosHorizontal, _currentPosVertical);
                System.Windows.Point tipsPoint = new System.Windows.Point(30, _currentPosVertical);

                //allineamento a sinistra del punto
                generateRectangle(ellypsePoint);

                for (int i = 0; i < currentGame.CodeLength; i++)
                {
                    //genero la nuova riga di cerchi (tentativo precedente)
                    allAttempts[_currentAttempt].Add(generateEllipse(ellypsePoint, selectColoursList[i]));
                    //mi muovo a destra di tot spazio
                    ellypsePoint.X += _buttonSpacing;
                }

                for (int i = 0; i < 3; i++)
                {
                    tipsPoint.X += 86 * i;
                    generateTips(i, tipsPoint);
                }

                _currentPosVertical -= _rectangleSpacing;
            }else 
            {
                for(int i = 0;i < currentGame.CodeLength;i++)
                {
                    selectColoursList[i].IsEnabled = false;
                }

                System.Windows.Point solutionPoint = new System.Windows.Point(solveCodeHorizontalPosition,solveCodeVerticalPosition);

                Colours[] solution = currentGame.GiveColourCode();

                for(int i = 0;i<currentGame.CodeLength ; i++)
                {
                    generateEllipse(solutionPoint, solution[i]);
                }

            }

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
                    btn.Background = Brushes.Red;
                    break;
                case 1:
                    btn.Background = Brushes.Blue;
                    break;
                case 2:
                    btn.Background = Brushes.Green;
                    break;
                case 3:
                    btn.Background = Brushes.Yellow;
                    break;
                case 4:
                    btn.Background = Brushes.Purple;
                    break;
                case 5:
                    btn.Background = Brushes.Orange;
                    break;
                case 6:
                    btn.Background = Brushes.Pink;
                    break;
                case 7:
                    btn.Background = Brushes.Cyan;
                    break;
                case 8:
                    btn.Background = Brushes.Cyan;
                    break;
                case 9://scarletto
                    btn.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 36, 0));
                    break;
                case 10:
                    btn.Background = Brushes.Teal;
                    break;
                case 11:
                    btn.Background = Brushes.Indigo;
                    break;
                default:
                    btn.Background = Brushes.Black;
                    break;

            }
            
        }

        private void ShowEllipseColour(Ellipse ell,Button btn)
        {
            int cur = int.Parse((string)btn.Content);
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
        private System.Windows.Shapes.Rectangle generateRectangle(System.Windows.Point currPoint)
        {
            System.Windows.Shapes.Rectangle rec = new System.Windows.Shapes.Rectangle();
            
            rec.Name = $"{_currentAttempt}Rectangle";
            rec.Width = recWidth;
            rec.Height = recHeight;
            rec.Fill = Brushes.Black;
            rec.PointFromScreen(currPoint);
            rec.Visibility = System.Windows.Visibility.Visible;
            
            return rec;

        }

        

        //questi sono i bottoni per la zona di gioco
        //gestione delle coordinate da fare, non dovrebbe essere troppo complesso
        private Button generateButton(int currentPos,System.Windows.Point currPoint)
        {
           Button btn = new Button();

            btn.Name = $"{currentPos}SolverButton";
            btn.Width = _buttonSize;
            btn.Height = _buttonSize;
            btn.Background = Brushes.Red;
            btn.PointFromScreen(currPoint);
            btn.BorderThickness = new Thickness(2);
            btn.Content = "1";
            btn.FontWeight = FontWeights.Bold;
            btn.FontSize = _buttonSize / 3;
            btn.Foreground = Brushes.White;
            btn.Visibility = System.Windows.Visibility.Visible;

            return btn; 

        }


        //btnToCopy mi dice i valori da copiare (colore principalmente)
        private Ellipse generateEllipse(System.Windows.Point currPoint,Button btnToCopy)
        {
            throw new NotImplementedException();
        }

        //per generare la soluzione in cima alla pagina
        private Ellipse generateEllipse(System.Windows.Point currPoint, Colours colourToCopy)
        {
            throw new NotImplementedException();
        }

        //genera ad ogni turno i suggerimenti del gioco mastermind
        private Button generateTips(int currentTip,System.Windows.Point currentPoint)
        {
            Button tipButton = new Button();

            tipButton.Name = $"{currentTip}Tip";
            tipButton.Width = _tipsSize;
            tipButton.Height = _rectangleHeight;
            tipButton.Background = Brushes.LightGray;
            tipButton.Foreground = Brushes.Black;
            tipButton.PointFromScreen(currentPoint);
            tipButton.Visibility = System.Windows.Visibility.Visible;

            if (currentTip == 0)
                tipButton.Content = currentGame.RightPosition;
            else if (currentTip == 1)
                tipButton.Content = currentGame.WrongPosition;
            else
                tipButton.Content = currentGame.IsAllWrong;

            return tipButton;

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
