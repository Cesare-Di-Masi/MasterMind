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
        public PveWindows(bool isColorBlind)
        {
            _isColorBlind = isColorBlind;
            InitializeComponent();
        }

        private void btnPVE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnMedium_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
