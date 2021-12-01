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
    /// Logique d'interaction pour Gestion_Fournisseur.xaml
    /// </summary>
    public partial class Gestion_Fournisseur : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Fournisseur> listeFournisseur = new List<Fournisseur>();
        public List<Fournisseur> ListeFournisseur { get { return listeFournisseur; } set { listeFournisseur = value; OnPropertyChanged("ListeFournisseur"); } }

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
        public Gestion_Fournisseur(string access)
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

            string requete = "SELECT * FROM fournisseur;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string siret = (string)reader["siret"];
                string nom_entreprise = (string)reader["nom_entreprise"];
                string contact_fournisseur = (string)reader["contact_fournisseur"];
                string adresse_fournisseur = (string)reader["adresse_fournisseur"];
                int libelle = (int)reader["libelle"];

                //Remplir la Liste
                Fournisseur f = new Fournisseur(siret, nom_entreprise, contact_fournisseur, adresse_fournisseur, libelle);
                listeFournisseur.Add(f);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Supprimer un fournisseur de la table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach (Fournisseur f in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + f + " de la liste ?", null, MessageBoxButton.OKCancel);
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

                    string requete = "DELETE FROM approvision WHERE siret = '" + f.Siret + "'; DELETE FROM fournisseur WHERE siret = '" + f.Siret + "'; ";
                    MySqlCommand command1 = maConnexion.CreateCommand();
                    command1.CommandText = requete;
                    try
                    {
                        command1.ExecuteNonQuery();
                        ListeFournisseur.Remove(f);
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
        /// Accès à la fenêtre de modification d'un fournisseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach (Fournisseur f in Liste1.SelectedItems)
            {
                Modif_Fournisseur modif = new Modif_Fournisseur(f, false, access);
                modif.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Accès à la fenêtre de création d'un fournisseur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Fournisseur f = new Fournisseur();
            Modif_Fournisseur modif = new Modif_Fournisseur(f, true, access);
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
        /// Export de la table fournisseurs en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "fournisseurssExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, listeFournisseur);

            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        /// <summary>
        /// Export de la table fournisseurs en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Fournisseur>));
            StreamWriter wr = new StreamWriter("fournisseursXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, listeFournisseur);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
