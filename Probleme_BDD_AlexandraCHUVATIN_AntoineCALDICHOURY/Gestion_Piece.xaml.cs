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
    /// Logique d'interaction pour Gestion_Piece.xaml
    /// </summary>
    public partial class Gestion_Piece : Window
    {
        List<Piece> listePiece = new List<Piece>();
        public List<Piece> ListePiece { get { return listePiece; } set { listePiece = value; OnPropertyChanged("ListePiece"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Indique quand une propriété d'une instance est modifiée
        /// </summary>
        /// <param name="name"></param>
        void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public Gestion_Piece()
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
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }

            string requete = "Select * from piece;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string num_piece;
                if (reader["num_piece"] == DBNull.Value) { num_piece = null; } else { num_piece = (string)reader["num_piece"]; }
                string description;
                if (reader["description"] == DBNull.Value) { description = null; } else { description = (string)reader["description"]; }
                
                string nom_fournisseur = (string)reader["nom_fournisseur"];
                string num_produit_catalogue = (string)reader["num_produit_catalogue"];
                float prix_piece = (float)reader["prix_piece"];
                DateTime date_intro_piece = (DateTime)reader["date_intro_piece"];
                DateTime date_disc_piece = (DateTime)reader["date_disc_piece"];
                string delai = (string)reader["delai"];
                int stock_piece = (int)reader["stock_piece"];

                //Remplir la Liste
                Piece p = new Piece(num_piece, description, nom_fournisseur, num_produit_catalogue, prix_piece, date_intro_piece, date_disc_piece, delai, stock_piece);
                listePiece.Add(p);
                //Console.WriteLine(p);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach(Piece p in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + p + " de la liste ?", null, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    p.Stock -= 1;
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

                    string requete = "UPDATE piece SET stock_piece="+ (p.Stock) +" WHERE num_piece='"+ p.Num_Piece +"';";
                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    command1.ExecuteNonQuery();

                    command1.Dispose();
                    maConnexion.Close();
                }
            }
            Liste1.Items.Refresh();
        }

        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach(Piece p in Liste1.SelectedItems)
            {
                Modif_Piece modif = new Modif_Piece(p, true);
                modif.Show();
                this.Close();
            }
        }

        private void Creer(object sender, RoutedEventArgs e)
        {
            Piece p = new Piece();
            Modif_Piece modif = new Modif_Piece(p, false);
            modif.Show();
            this.Close();
        }
        #endregion

        private void Retour(object sender, RoutedEventArgs e)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();
            this.Close();
        }
    }
}
