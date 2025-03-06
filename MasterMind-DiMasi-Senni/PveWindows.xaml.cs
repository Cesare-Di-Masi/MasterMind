using MastermindLib;
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
using System.Windows.Shapes;

namespace MasterMind_DiMasi_Senni
{
    /// <summary>
    /// Logica di interazione per PveWindows.xaml
    /// </summary>
    public partial class PveWindows : Window
    {
        private bool _isColorBlind;
        GameManager game;

        public PveWindows(bool isColorBlind)
        {
            InitializeComponent();
            _isColorBlind = isColorBlind;
        }

        private void btnMenù_Click(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            a.Show();
            this.Close();
        }

        private void btnEasy_Click(object sender, RoutedEventArgs e)
        {
            game = new GameManager(_isColorBlind, 4, 4,5,1);
            Window Game = new GameWindow(game);
            Game.Show();
            this.Close();
        }

        private void btnDifficult_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
