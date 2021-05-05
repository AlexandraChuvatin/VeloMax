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
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    /// <summary>
    /// Logique d'interaction pour Modif_Piece.xaml
    /// </summary>
    public partial class Modif_Piece : Window
    {
        List<Piece> piece;
        bool creer;
        public List<Piece> Piece { get { return piece; } set { piece = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }
        
        public Modif_Piece(Piece p, bool creer)
        {
            piece = new List<Piece> { p };
            this.creer = creer;
            this.DataContext = this;
            InitializeComponent();
        }

        private void Valider(object sender, RoutedEventArgs e)
        {
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=root;PASSWORD=root";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException er)
            {
                Console.WriteLine(" ErreurConnexion : " + er.ToString());
                return;
            }
            //Ajouter les autres attributs
            string requete;
            if(Creer==true)
            {
                requete = "UPDATE piece SET description=" + (Piece[0].Description) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET nom_fournisseur=" + (Piece[0].Nom_fournisseur) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET num_produit_catalogue v=" + (Piece[0].Num_produit_catalogue) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET prix_piece=" + (Piece[0].Prix_Piece) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET date_intro_piece=" + (Piece[0].Date_intro_piece) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET date_disc_piece=" + (Piece[0].Date_disc_piece) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET delai=" + (Piece[0].Delai) + " WHERE num_piece='" + Piece[0].Num_Piece + "';" +
                    "UPDATE piece SET stock_piece=" + (Piece[0].Stock) + " WHERE num_piece='" + Piece[0].Num_Piece + "';";
            }
            else
            {
                requete = "INSERT INTO piece VALUES ('" + 
                    Piece[0].Num_Piece + "', '" + 
                    Piece[0].Description + "', '" + 
                    Piece[0].Nom_fournisseur + "', '" + 
                    Piece[0].Num_produit_catalogue + "'," + 
                    Piece[0].Prix_Piece + ", '" + 
                    Piece[0].Date_intro_piece.ToString("yyyy-MM-dd") + "', '" + 
                    Piece[0].Date_disc_piece.ToString("yyyy-MM-dd") + "', '" + 
                    Piece[0].Delai + "', " + Piece[0].Stock + ") ;";
            }
            

            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;
            command1.ExecuteNonQuery();//y'a un pb avec cette commande quand même...

            command1.Dispose();
            maConnexion.Close();


            Gestion_Piece gestion = new Gestion_Piece();
            gestion.Show();
            this.Close();
        }
    }
}
