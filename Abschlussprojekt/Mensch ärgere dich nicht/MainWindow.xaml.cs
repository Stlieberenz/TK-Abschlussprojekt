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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mensch_ärgere_dich_nicht;

namespace Mensch_ärgere_dich_nicht
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Hier wird nur das mainWindow und das Grudfenster (rootFrame) in Variablen Geschrieben, die von über all aus erreicht werden kann.
            InitializeComponent();
            Statische_Variablen.rootFrame = this.rootFrame;
            Statische_Variablen.mainWindow = this;
            rootFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden; //Versteckt die Navigationsleiste
            rootFrame.Content = new Seiten.Startseite(); // Öffnet die Startseite
        }
    }
}
