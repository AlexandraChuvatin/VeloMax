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

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        string access;
        public string Access { get { return access; } set { access = value; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="a">root ou bozo</param>
        public Menu(string a)
        {
            access = a;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Accès à la fenêtre de gestion des pièces
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Piece_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Piece fenetre = new Gestion_Piece(access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Accès à la fenêtre de gestion des modèles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Modele_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Modele fenetre = new Gestion_Modele(access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Accès à la fenêtre de gestion des fournisseurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Fournisseur_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Fournisseur fenetre = new Gestion_Fournisseur(access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Accès à la fenêtre de gestion des clients particuliers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Particulier_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Client_Particulier fenetre = new Gestion_Client_Particulier(access);
            fenetre.Show();
            this.Close();
        }

        /// <summary>
        /// Accès à la fenêtre de gestion des clients entreprises
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Entreprise_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Client_Entreprise fenetre = new Gestion_Client_Entreprise(access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Accès à la fenêtre de gestion des commandes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Commandes_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Commande fenetre = new Gestion_Commande(access);
            fenetre.Show();
            this.Close();
        }

        /// <summary>
        /// Accès à la fenêtre de gestion des stocks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Gestion_Stock_Click(object sender, RoutedEventArgs e)
        {
            Gestion_Stock fenetre = new Gestion_Stock(access);
            fenetre.Show();
            this.Close();
        }

        //Accès à la fenêtre des statistiques de VeloMax
        private void Statistiques_Click(object sender, RoutedEventArgs e)
        {
            Statistiques fenetre = new Statistiques(access);
            fenetre.Show();
            this.Close();
        }

        /// <summary>
        /// Deconnexion = Retour à la MainWindow pour choisir l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deconnexion(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        #endregion
    }
}
