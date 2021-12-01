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
    /// Logique d'interaction pour Modif_Client_Particulier.xaml
    /// </summary>
    public partial class Modif_Client_Particulier : Window
    {

        string access;
        public string Access { get { return access; } }

        List<Individu> individus;
        bool creer;

        public List<Individu> Individus { get { return individus; } set { individus = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="i">client à modifier</param>
        /// <param name="creer">creer individu (true) modifier individu (false)</param>
        /// <param name="access">root ou bozo</param>
        public Modif_Client_Particulier(Individu i, bool creer, string access)
        {
            individus = new List<Individu> { i };
            this.creer = creer;
            this.access = access;
            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Valider la modification d'un client particulier en changeant son contenu dans la BDD via requêtes SQL
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
            //Ajouter les autres attributs
            string requete;
            if (Creer == false)
            {
                requete = "UPDATE individu SET prenom_individu='" + (Individus[0].Prenom_individu) + "' WHERE nom_individu='" + Individus[0].Nom_individu + "';" +
                    "UPDATE individu SET adresse_individu='" + (Individus[0].Adresse_individu) + "' WHERE nom_individu='" + Individus[0].Nom_individu + "';" +
                    "UPDATE individu SET tel_individu ='" + (Individus[0].Tel_individu) + "' WHERE nom_individu='" + Individus[0].Nom_individu + "';" +
                    "UPDATE individu SET mail_individu='" + (Individus[0].Mail_individu) + "' WHERE nom_individu='" + Individus[0].Nom_individu + "';" +
                    "UPDATE individu SET date_adhesion='" + (Individus[0].Date_adhesion.ToString("yyyy-MM-dd")) + "' WHERE nom_individu='" + Individus[0].Nom_individu + "';" +
                    "UPDATE individu SET num_programme=" + (Individus[0].Num_programme) + " WHERE nom_individu='" + Individus[0].Nom_individu + "';";
            }
            else
            {
                requete = "INSERT INTO individu VALUES ('" +
                    Individus[0].Nom_individu + "', '" +
                    Individus[0].Prenom_individu + "', '" +
                    Individus[0].Adresse_individu + "', '" +
                    Individus[0].Tel_individu + "','" +
                    Individus[0].Mail_individu + "', '" +
                    Individus[0].Date_adhesion.ToString("yyyy-MM-dd") + "', " +
                    Individus[0].Num_programme + ");";
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

            Gestion_Client_Particulier gestion = new Gestion_Client_Particulier(access);
            gestion.Show();
            gestion.Liste1.Items.Refresh();
            this.Close();
        }
    }
}
