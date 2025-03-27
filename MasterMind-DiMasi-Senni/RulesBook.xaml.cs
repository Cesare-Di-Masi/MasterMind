<<<<<<< HEAD
﻿using System.Windows;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
>>>>>>> 710fda8c57e82b405f87cdc0a8f9ebed8bb48474

namespace MasterMind_DiMasi_Senni
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class RulesBook : Window
    {
        public RulesBook()
        {
            InitializeComponent();
        }

        private void btnMenù_Click(object sender, RoutedEventArgs e)
        {
            var a = new MainWindow();
            a.Show();
            this.Close();
        }
    }
}