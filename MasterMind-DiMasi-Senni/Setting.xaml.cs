﻿using System;
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
