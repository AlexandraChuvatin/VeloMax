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
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    /// <summary>
    /// Logique d'interaction pour Gestion_Modele.xaml
    /// </summary>
    public partial class Gestion_Modele : Window
    {
        string access;
        public string Access { get { return access; } }
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

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="access">root ou bozo</param>
        public Gestion_Modele(string access)
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
                string pedalier;
                if (reader["pedalier"] == DBNull.Value) { pedalier = null; } else { pedalier = (string)reader["pedalier"]; }
                string ordinateur;
                if (reader["ordinateur"] == DBNull.Value) { ordinateur = null; } else { ordinateur = (string)reader["ordinateur"]; }
                string panier;
                if (reader["panier"] == DBNull.Value) { panier = null; } else { panier = (string)reader["panier"]; }
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
                Modif_Modele modif = new Modif_Modele(m, false, access);
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
                    if (access == "root")
                    {
                        if (m.Stock_modele > 0) { m.Stock_modele -= 1; } else { return; }
                    }
                    
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
            Modif_Modele modif = new Modif_Modele(m, true, access);
            modif.Show();
            this.Close();
        }
        /// <summary>
        /// Retour au menu principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Retour(object sender, RoutedEventArgs e)
        {
            Menu fenetre = new Menu(access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Export de la table de modèles de vélo en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "modelesExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, listeModele);

            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }

        /// <summary>
        /// Export de la table de modèles de vélo en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Modele>));
            StreamWriter wr = new StreamWriter("modelesXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, listeModele);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
