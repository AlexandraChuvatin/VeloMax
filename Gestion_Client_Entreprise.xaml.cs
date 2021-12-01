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
    /// Logique d'interaction pour Gestion_Client_Entreprise.xaml
    /// </summary>
    public partial class Gestion_Client_Entreprise : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Boutique> entreprises = new List<Boutique>();
        public List<Boutique> Entreprises { get { return entreprises; } set { entreprises = value; OnPropertyChanged("Entreprises"); } }
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
        public Gestion_Client_Entreprise(string access)
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

            string requete = "SELECT * FROM boutique;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string nom_boutique;
                if (reader["nom_boutique"] == DBNull.Value) { nom_boutique = null; } else { nom_boutique = (string)reader["nom_boutique"]; }
                string adresse_boutique;
                if (reader["adresse_boutique"] == DBNull.Value) { adresse_boutique = null; } else { adresse_boutique = (string)reader["adresse_boutique"]; }
                string tel_boutique = (string)reader["tel_boutique"];
                string mail_boutique = (string)reader["mail_boutique"];
                string nom_contact = (string)reader["nom_contact"];
                float remise;
                if (reader["remise"] == DBNull.Value) { remise = 0; } else { remise = (float)reader["remise"]; }
                
                //Remplir la Liste
                Boutique b = new Boutique(nom_boutique, adresse_boutique, tel_boutique, mail_boutique, nom_contact, remise);
                entreprises.Add(b);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Retour à la page principale
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
        /// Supprimer un client entreprise de la table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach (Boutique b in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + b + " de la liste ?", null, MessageBoxButton.OKCancel);
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
                        "delete contient_piece from contient_piece natural join commande natural join boutique where nom_boutique='" + b.Nom_boutique + "';" +
                        "delete contient_modele from contient_modele natural join commande natural join boutique where nom_boutique = '" + b.Nom_boutique + "';" +
                        "delete commande from commande where nom_boutique = '" + b.Nom_boutique + "';" +
                        "delete from boutique where nom_boutique = '" + b.Nom_boutique + "'; ";

                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    try
                    {
                        command1.ExecuteNonQuery();
                        Entreprises.Remove(b);
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
        /// Accès à la page de modification d'un client entreprise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach (Boutique b in Liste1.SelectedItems)
            {
                Modif_Client_Entreprise fenetre = new Modif_Client_Entreprise(b, false, access);
                fenetre.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Accès à la page de création d'un client entreprise
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Boutique b = new Boutique();
            Modif_Client_Entreprise fenetre = new Modif_Client_Entreprise(b, false, access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Export de la table boutiques en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "entreprisesExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, entreprises);
            
            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        /// <summary>
        /// Export de la table boutiques en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Boutique>));
            StreamWriter wr = new StreamWriter("entreprisesXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, entreprises);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
