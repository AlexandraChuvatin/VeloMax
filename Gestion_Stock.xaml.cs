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
    /// Logique d'interaction pour Gestion_Stock.xaml
    /// </summary>
    public partial class Gestion_Stock : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Piece> listePiece = new List<Piece>();
        public List<Piece> ListePiece { get { return listePiece; } set { listePiece = value; OnPropertyChanged("ListePiece"); } }
        List<Modele> listeModele = new List<Modele>();
        public List<Modele> ListeModele { get { return listeModele; } set { listeModele = value; OnPropertyChanged("ListeModele"); } }
        List<string> listeFournisseur = new List<string>();
        public List<string> ListeFournisseur { get { return listeFournisseur; } set { listeFournisseur = value; OnPropertyChanged("ListeFournisseur"); } }
        List<string> listeProduit = new List<string>();
        public List<string> ListeProduit { get { return listeProduit; } set { listeProduit = value; OnPropertyChanged("ListeProduit"); } }


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


        public Gestion_Stock(string access)
        {
            this.access = access;
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }

            string requeteModele = "select num_modele, ligne_produit, nom_modele,stock_modele from modele;";
            MySqlCommand command0 = maConnexion.CreateCommand();
            command0.CommandText = requeteModele;

            MySqlDataReader reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_modele = (string)reader0["num_modele"];
                string ligne_produit = (string)reader0["ligne_produit"];
                string nom_modele = (string)reader0["nom_modele"];
                int stock_modele = (int)reader0["stock_modele"];

                //Remplir la Liste
                Modele cm = new Modele();
                cm.Num_modele = num_modele; cm.Ligne_produit = ligne_produit; cm.Nom_modele = nom_modele; cm.Stock_modele = stock_modele;
                ListeModele.Add(cm);
            }
            reader0.Close();
            command0.Dispose();

            string requetePiece = "select num_piece, nom_fournisseur, stock_piece from piece;";
            command0 = maConnexion.CreateCommand();
            command0.CommandText = requetePiece;

            reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_piece = (string)reader0["num_piece"];
                string nom_fournisseur = (string)reader0["nom_fournisseur"];
                int stock_piece = (int)reader0["stock_piece"];

                //Remplir la Liste
                Piece cp = new Piece();
                cp.Num_Piece = num_piece; cp.Nom_fournisseur = nom_fournisseur; cp.Stock = stock_piece;
                ListePiece.Add(cp);
            }
            reader0.Close();
            command0.Dispose();

            string requeteProduit = "select distinct(ligne_produit) from modele order by ligne_produit asc;";
            command0 = maConnexion.CreateCommand();
            command0.CommandText = requeteProduit;

            reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string nom_produit = (string)reader0["ligne_produit"];

                ListeProduit.Add(nom_produit);
            }
            reader0.Close();
            command0.Dispose();

            string requeteFournisseur = "select distinct(nom_fournisseur) from piece order by nom_fournisseur asc;";
            command0 = maConnexion.CreateCommand();
            command0.CommandText = requeteFournisseur;

            reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string nom_fournisseur = (string)reader0["nom_fournisseur"];

                ListeFournisseur.Add(nom_fournisseur);
            }
            reader0.Close();
            command0.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }
        private void Retour(object sender, RoutedEventArgs e)
        {
            Menu fenetre = new Menu(access);
            fenetre.Show();
            this.Close();
        }
        
        private void SelectionChangedFournisseur(object sender, SelectionChangedEventArgs args)
        {
            ListePiece.Clear();
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }

            string fournisseur = (string)Combobox.SelectedItem;
            string requetePiece = "select num_piece, nom_fournisseur, stock_piece from piece where nom_fournisseur='"+fournisseur+"';";
            MySqlCommand command0 = maConnexion.CreateCommand();
            command0.CommandText = requetePiece;

            MySqlDataReader reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_piece = (string)reader0["num_piece"];
                string nom_fournisseur = (string)reader0["nom_fournisseur"];
                int stock_piece = (int)reader0["stock_piece"];

                //Remplir la Liste
                Piece cp = new Piece();
                cp.Num_Piece = num_piece; cp.Nom_fournisseur = nom_fournisseur; cp.Stock = stock_piece;
                ListePiece.Add(cp);
            }
            reader0.Close();
            command0.Dispose();
            ListeStockPiece.Items.Refresh();
        }

        private void SelectionChangedProduit(object sender, SelectionChangedEventArgs args)
        {
            ListeModele.Clear();
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }

            string produit = (string)Combobox2.SelectedItem;
            string requeteProduit = "select num_modele, ligne_produit, nom_modele,stock_modele from modele where ligne_produit='"+produit+"';";
            MySqlCommand command0 = maConnexion.CreateCommand();
            command0.CommandText = requeteProduit;

            MySqlDataReader reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_modele = (string)reader0["num_modele"];
                string ligne_produit = (string)reader0["ligne_produit"];
                string nom_modele = (string)reader0["nom_modele"];
                int stock_modele = (int)reader0["stock_modele"];

                //Remplir la Liste
                Modele cm = new Modele();
                cm.Num_modele = num_modele; cm.Ligne_produit = ligne_produit; cm.Nom_modele = nom_modele; cm.Stock_modele = stock_modele;
                ListeModele.Add(cm);
            }
            reader0.Close();
            command0.Dispose();
            ListeStockModele.Items.Refresh();
        }
    }
}
