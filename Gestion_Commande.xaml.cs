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
    /// Logique d'interaction pour Gestion_Commande.xaml
    /// </summary>
    public partial class Gestion_Commande : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Commande> listeCommande = new List<Commande>();
        public List<Commande> ListeCommande { get { return listeCommande; } set { listeCommande = value; OnPropertyChanged("ListeCommande"); } }

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
        public Gestion_Commande(string access)
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

            string requete = "SELECT * FROM commande;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                int num_commande = (int)reader["num_commande"];
                DateTime date_commande = (DateTime)reader["date_commande"];
                string adresse_livraison = (string)reader["adresse_livraison"];
                DateTime date_livraison = (DateTime)reader["date_livraison"];
                string nom_individu;
                if (reader["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader["nom_individu"]; }
                string nom_boutique;
                if (reader["nom_boutique"] == DBNull.Value) { nom_boutique = null; } else { nom_boutique = (string)reader["nom_boutique"]; }

                //Remplir la Liste
                Commande c = new Commande(num_commande, date_commande, adresse_livraison, date_livraison, nom_individu, nom_boutique);
                listeCommande.Add(c);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Supprimer une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach (Commande c in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + c + " de la liste ?", null, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
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

                    string requete =
                        "delete contient_piece from contient_piece natural join commande where num_commande=" + c.Num_commande + ";" +
                        "delete contient_modele from contient_modele natural join commande where num_commande=" + c.Num_commande + ";" +
                        "delete commande from commande where num_commande = " + c.Num_commande + ";";

                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    try
                    {
                        command1.ExecuteNonQuery();
                        ListeCommande.Remove(c);
                    }
                    catch (MySqlException er)
                    {
                        Console.WriteLine("Erreur de la requête : " + er.ToString());
                    }
                    finally
                    {
                        command1.Dispose();
                        maConnexion.Close();
                    }
                }
            }
            Liste1.Items.Refresh();
        }
        /// <summary>
        /// Accès à la page de modification d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach (Commande c in Liste1.SelectedItems)
            {
                Modif_Commande modif = new Modif_Commande(c, false, access);
                modif.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Accès à la page de création d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Commande c = new Commande();
            Modif_Commande modif = new Modif_Commande(c, true, access);
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
        /// Export de la table commandes en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "commandesExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, listeCommande);

            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        /// <summary>
        /// Export de la table commandes en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Commande>));
            StreamWriter wr = new StreamWriter("entreprisesXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, listeCommande);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
