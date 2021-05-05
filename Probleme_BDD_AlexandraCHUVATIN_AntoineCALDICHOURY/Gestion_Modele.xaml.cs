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
    /// Logique d'interaction pour Gestion_Modele.xaml
    /// </summary>
    public partial class Gestion_Modele : Window
    {
        List<Modele> listeModele = new List<Modele>();
        public List<Modele> ListeModele { get { return listeModele; } set { listeModele = value; OnPropertyChanged("ListePiece"); } }

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

        public Gestion_Modele()
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

            string requete = "SELECT * FROM modele;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string num_modele;
                if (reader["num_modele"] == DBNull.Value) { num_modele = null; } else { num_modele = (string)reader["num_modele"]; }
                float prix_modele = (float)reader["prix_modele"];
                string ligne_produit =(string)reader["ligne_produit"];
                string nom_modele = (string)reader["nom_modele"];
                string grandeur = (string)reader["grandeur"];
                DateTime date_intro_modele = (DateTime)reader["date_intro_modele"];
                DateTime date_disc_modele = (DateTime)reader["date_disc_modele"];
                string cadre = (string)reader["cadre"];
                string guidon = (string)reader["guidon"];
                string freins = (string)reader["freins"];
                string selle = (string)reader["selle"];
                string derailleur_avant = (string)reader["derailleur_avant"];
                string derailleur_arriere = (string)reader["derailleur_arriere"];
                string roue_avant = (string)reader["roue_avant"];
                string roue_arriere = (string)reader["roue_arriere"];
                string reflecteurs = (string)reader["reflecteurs"];
                string pedalier = (string)reader["pedalier"];
                string ordinateur = (string)reader["ordinateur"];
                string panier = (string)reader["panier"];
                int stock_modele = (int)reader["stock_modele"];

                //Remplir la Liste
                Modele m = new Modele(num_modele, prix_modele, ligne_produit, nom_modele, grandeur, date_intro_modele, date_disc_modele, cadre, guidon, freins, selle, derailleur_avant, derailleur_arriere, roue_avant, roue_arriere, reflecteurs, pedalier, ordinateur, panier, stock_modele);
                listeModele.Add(m);
                //Console.WriteLine(m);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Modification d'un modèle de vélo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach (Modele m in Liste1.SelectedItems)
            {
                Modif_Modele modif = new Modif_Modele(m, true);
                modif.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Supprime une quantité de la pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach (Modele m in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + m + " de la liste ?", null, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    m.Stock_modele -= 1;
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

                    string requete = "UPDATE modele SET stock_modele=" + (m.Stock_modele) + " WHERE num_modele='" + m.Num_modele + "';";
                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    command1.ExecuteNonQuery();

                    command1.Dispose();
                    maConnexion.Close();
                }
            }
            Liste1.Items.Refresh();
        }

        /// <summary>
        /// Création d'un nouveau modèle de vélo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Modele m = new Modele();
            Modif_Modele modif = new Modif_Modele(m, false);
            modif.Show();
            this.Close();
        }

        private void Retour(object sender, RoutedEventArgs e)
        {
            MainWindow fenetre = new MainWindow();
            fenetre.Show();
            this.Close();
        }
        #endregion
    }
}
