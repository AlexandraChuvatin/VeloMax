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
    /// Logique d'interaction pour Gestion_Client_Particulier.xaml
    /// </summary>
    public partial class Gestion_Client_Particulier : Window
    {
        string access;
        public string Access { get { return access; } }

        List<Individu> particuliers = new List<Individu>();
        public List<Individu> Particuliers { get { return particuliers; } set { particuliers = value; OnPropertyChanged("Particuliers"); } }
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
        public Gestion_Client_Particulier(string access)
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

            string requete = "SELECT * FROM individu;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string nom_individu;
                if (reader["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader["nom_individu"]; }
                string prenom_individu;
                if (reader["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader["prenom_individu"]; }
                string adresse_individu = (string)reader["adresse_individu"];
                string tel_individu = (string)reader["tel_individu"];
                string mail_individu = (string)reader["mail_individu"];
                DateTime date_adhesion;
                if (reader["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader["date_adhesion"]; }
                int num_programme;
                if (reader["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                particuliers.Add(i);
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
        /// Supprimer un client particulier de la table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach (Individu i in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + i + " de la liste ?", null, MessageBoxButton.OKCancel);
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

                    //PROBLEME : il faut aussi supprimer de contient_modele et contient_piece les commandes faites par cet individu
                    string requete =
                        "delete contient_piece from contient_piece natural join commande natural join individu where nom_individu='" + i.Nom_individu + "';" +
                        "delete contient_modele from contient_modele natural join commande natural join individu where nom_individu = '" + i.Nom_individu + "';" +
                        "delete commande from commande where nom_individu = '" + i.Nom_individu + "';" +
                        "delete from individu where nom_individu = '" + i.Nom_individu + "'; ";

                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    try
                    {
                        command1.ExecuteNonQuery();
                        Particuliers.Remove(i);
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
        /// Accès à la page de modificationd d'un client particulier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach (Individu i in Liste1.SelectedItems)
            {
                Modif_Client_Particulier fenetre = new Modif_Client_Particulier(i, false, access);
                fenetre.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Accès à la page de création d'un client particulier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Individu i = new Individu();
            Modif_Client_Particulier fenetre = new Modif_Client_Particulier(i, true, access);
            fenetre.Show();
            this.Close();
        }
        /// <summary>
        /// Export de la table individus en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "particuliersExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, particuliers);

            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        /// <summary>
        /// Export de la table individus en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Individu>));
            StreamWriter wr = new StreamWriter("particuliersXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, particuliers);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
