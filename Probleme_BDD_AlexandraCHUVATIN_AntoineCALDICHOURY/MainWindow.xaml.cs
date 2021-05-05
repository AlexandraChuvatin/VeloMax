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

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Gestion_Piece_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Piece fenetre = new Gestion_Piece();
            fenetre.Show();
            this.Close();
        }

        private void Gestion_Modele_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Modele fenetre = new Gestion_Modele();
            fenetre.Show();
            this.Close();
        }
    }
}
