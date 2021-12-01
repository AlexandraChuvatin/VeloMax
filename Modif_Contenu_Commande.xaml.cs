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
    /// Logique d'interaction pour Modif_Contenu_Commande.xaml
    /// </summary>
    public partial class Modif_Contenu_Commande : Window
    {
        Commande c;
        public Commande C { get { return c; } }
        string access;
        public string Access { get { return access; } }
        List<Contient_modele> listeCModele = new List<Contient_modele>();
        public List<Contient_modele> ListeCModele { get { return listeCModele; } set { listeCModele = value; OnPropertyChanged("ListeCModele"); } }
        List<Contient_modele> ajoutModele = new List<Contient_modele>() { new Contient_modele(0, "", 0) };
        public List<Contient_modele> AjoutModele { get { return ajoutModele; } set { ajoutModele = value; OnPropertyChanged("AjoutModele"); } }
        List<Contient_piece> ajoutPiece = new List<Contient_piece>() { new Contient_piece(0, "", 0) };
        public List<Contient_piece> AjoutPiece { get { return ajoutPiece; } set { ajoutPiece = value; OnPropertyChanged("AjoutPiece"); } }
        List<Modele> listeModele = new List<Modele>();
        public List<Modele> ListeModele { get { return listeModele; } set { listeModele = value; OnPropertyChanged("ListeModele"); } }
        List<Piece> listePiece = new List<Piece>();
        public List<Piece> ListePiece { get { return listePiece; } set { listePiece = value; OnPropertyChanged("ListePiece"); } }

        List<Contient_piece> listeCPiece = new List<Contient_piece>();
        public List<Contient_piece> ListeCPiece { get { return listeCPiece; } set { listeCPiece = value; OnPropertyChanged("ListeCPiece"); } }

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
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="access">root ou bozo</param>
        /// <param name="c">Commande à modifier</param>
        public Modif_Contenu_Commande(string access, Commande c)
        {
            this.ajoutModele[0].Num_commande = c.Num_commande;
            this.ajoutPiece[0].Num_commande = c.Num_commande;
            this.c = c;
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

            string requeteModele = "select num_modele,prix_modele,ligne_produit,nom_modele from modele;";
            MySqlCommand command0 = maConnexion.CreateCommand();
            command0.CommandText = requeteModele;

            MySqlDataReader reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_modele = (string)reader0["num_modele"];
                float prix_modele = (float)reader0["prix_modele"];
                string ligne_produit = (string)reader0["ligne_produit"];
                string nom_modele = (string)reader0["nom_modele"];

                //Remplir la Liste
                Modele cm = new Modele();
                cm.Num_modele = num_modele; cm.Prix_modele = prix_modele; cm.Ligne_produit = ligne_produit; cm.Nom_modele = nom_modele;
                ListeModele.Add(cm);
            }
            reader0.Close();
            command0.Dispose();

            string requetePiece = "select num_piece, description, prix_piece from piece;";
            command0 = maConnexion.CreateCommand();
            command0.CommandText = requetePiece;

            reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string num_piece = (string)reader0["num_piece"];
                string description = (string)reader0["description"];
                float prix_piece = (float)reader0["prix_piece"];

                //Remplir la Liste
                Piece cp = new Piece();
                cp.Num_Piece = num_piece; cp.Description = description; cp.Prix_Piece = prix_piece;
                ListePiece.Add(cp);
            }
            reader0.Close();
            command0.Dispose();

            string requete1 = "SELECT * FROM contient_modele where num_commande = " + c.Num_commande+";";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete1;

            MySqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                int num_commande = (int)reader1["num_commande"];
                string modele = (string)reader1["num_modele"];
                int quantite = (int)reader1["quantite_modele"];

                //Remplir la Liste
                Contient_modele cm = new Contient_modele(num_commande, modele, quantite);
                ListeCModele.Add(cm);
            }
            reader1.Close();
            command1.Dispose();

            string requete2 = "SELECT * FROM contient_piece where num_commande = "+ c.Num_commande + ";";
            MySqlCommand command2 = maConnexion.CreateCommand();
            command2.CommandText = requete2;

            MySqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                int num_commande = (int)reader2["num_commande"];
                string piece = (string)reader2["num_piece"];
                int quantite = (int)reader2["quantite_piece"];

                //Remplir la Liste
                Contient_piece cp = new Contient_piece(num_commande, piece, quantite);
                ListeCPiece.Add(cp);
            }
            reader2.Close();
            command2.Dispose();

            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Insère ou supprime une pièce de la commande via requêtes SQL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            MySqlConnection maConnexion = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException er)
            {
                Console.WriteLine(" ErreurConnexion : " + er.ToString());
                return;
            }

            string requeteDelete = "delete from contient_modele where num_commande = "+this.c.Num_commande+ "; delete from contient_piece where num_commande = " + this.c.Num_commande + ";";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requeteDelete;
            try
            {
                command1.ExecuteNonQuery();
            }
            catch (MySqlException er)
            {
                Console.WriteLine("Erreur de la requête : " + er.ToString());
            }
            finally
            {
                command1.Dispose();            
            }

            foreach (Contient_modele cm in listeCModele)
            {
                string requete = "insert into contient_modele values(" + cm.Num_commande + ",'" + cm.Num_modele + "'," + cm.Quantite_modele + ");";
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException er)
                {
                    Console.WriteLine("Erreur de la requête : " + er.ToString());
                }
                finally
                {
                    command.Dispose();
                }
            }
            foreach (Contient_piece cp in listeCPiece)
            {
                string requete = "insert into contient_piece values(" + cp.Num_commande + ",'" + cp.Num_piece + "'," + cp.Quantite_piece + ");";
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException er)
                {
                    Console.WriteLine("Erreur de la requête : " + er.ToString());
                }
                finally
                {
                    command.Dispose();
                }
            }
            maConnexion.Close();


            Modif_Commande gestion = new Modif_Commande(c, false ,access);
            gestion.Show();
            this.Close();
        }

        /// <summary>
        /// Ajouter modèle à la liste des modèles que contient la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajout_Modele(object sender, RoutedEventArgs e)
        {
            this.ListeCModele.Add(new Contient_modele(AjoutModele[0].Num_commande, AjoutModele[0].Num_modele, AjoutModele[0].Quantite_modele));
            Liste1.Items.Refresh();
        }
        /// <summary>
        /// Ajouter pièce à la liste des pièces que contient la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajout_Piece(object sender, RoutedEventArgs e)
        {
            this.ListeCPiece.Add(new Contient_piece(AjoutPiece[0].Num_commande, AjoutPiece[0].Num_piece, AjoutPiece[0].Quantite_piece));
            Liste2.Items.Refresh();
        }
        /// <summary>
        /// Supprimer modèle de la liste des modèles que contient la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerModele(object sender, RoutedEventArgs e)
        {
            foreach(Contient_modele cm in Liste1.SelectedItems)
            {
                listeCModele.Remove(cm);
            }
            Liste1.Items.Refresh();
        }

        /// <summary>
        /// Supprimer pièce de la liste de pièces que contient la commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupprimerPiece(object sender, RoutedEventArgs e)
        {
            foreach (Contient_piece cp in Liste2.SelectedItems)
            {
                listeCPiece.Remove(cp);
            }
            Liste2.Items.Refresh();
        }
    }
}
