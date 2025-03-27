using System.Windows;

namespace MasterMind_DiMasi_Senni
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class GameSettings : Window
    {
        private bool _isColorBlind;

        public GameSettings(bool isColorBlind)
        {
            _isColorBlind = isColorBlind;
            InitializeComponent();
        }
    }
}