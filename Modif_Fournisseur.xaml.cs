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
    /// Logique d'interaction pour Modif_Fournisseur.xaml
    /// </summary>
    public partial class Modif_Fournisseur : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Fournisseur> fournisseur;
        bool creer;
        public List<Fournisseur> Fournisseur { get { return fournisseur; } set { fournisseur = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="f">fournisseur à modifier</param>
        /// <param name="creer">creer fournisseur (true) modifier fournisseur (false)</param>
        /// <param name="access">root ou bozo</param>
        public Modif_Fournisseur(Fournisseur f, bool creer, string access)
        {
            fournisseur = new List<Fournisseur> { f };
            this.creer = creer;
            this.access = access;
            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Valider la modification d'un fournisseur en changeant son contenu dans la BDD via requêtes SQL
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

            string requete;
            if (Creer == false)
            {
                requete = "UPDATE fournisseur SET nom_entreprise='" + (Fournisseur[0].Nom_entreprise) + "' WHERE siret='" + Fournisseur[0].Siret + "';" +
                    "UPDATE fournisseur SET contact_fournisseur='" + (Fournisseur[0].Contact_fournisseur) + "' WHERE siret='" + Fournisseur[0].Siret + "';" +
                    "UPDATE fournisseur SET adresse_fournisseur='" + (Fournisseur[0].Adresse_fournisseur) + "' WHERE siret='" + Fournisseur[0].Siret + "';" +
                    "UPDATE fournisseur SET libelle=" + (Fournisseur[0].Libelle) + " WHERE siret='" + Fournisseur[0].Siret + "';";
            }
            else
            {
                requete = "INSERT INTO fournisseur VALUES ('" +
                    Fournisseur[0].Siret + "', '" +
                    Fournisseur[0].Nom_entreprise + "', '" +
                    Fournisseur[0].Contact_fournisseur + "', '" +
                    Fournisseur[0].Adresse_fournisseur + "'," +
                    Fournisseur[0].Libelle + ") ;";
            }


            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;
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
                maConnexion.Close();
            }


            Gestion_Fournisseur gestion = new Gestion_Fournisseur(access);
            gestion.Show();
            this.Close();
        }
    }
}
