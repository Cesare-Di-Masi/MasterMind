using System.Linq.Expressions;
using System.Windows;
using MastermindLib;

namespace MasterMind_DiMasi_Senni
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        bool isColorBlind = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPVP_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnPVE_Click(object sender, RoutedEventArgs e)
        {
            Window a = new PveWindows();
            a.Show();
            this.Close();
        }

        private void btnRule_Click(object sender, RoutedEventArgs e)
        {
            var a = new RulesBook ();
            a.Show();
            this.Close();
        }
    }
}