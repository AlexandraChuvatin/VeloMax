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
    /// Logique d'interaction pour Modif_Client_Entreprise.xaml
    /// </summary>
    public partial class Modif_Client_Entreprise : Window
    {
        string access;
        public string Access { get { return access; } }

        List<Boutique> entreprises;
        bool creer;

        public List<Boutique> Entreprises { get { return entreprises; } set { entreprises = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="b">boutique à modifier</param>
        /// <param name="creer">creer boutique (true) modifier boutique (false)</param>
        /// <param name="access">root ou bozo</param>
        /// 
        public Modif_Client_Entreprise(Boutique b, bool creer, string access)
        {
            entreprises = new List<Boutique> { b };
            this.creer = creer;
            this.access = access;
            this.DataContext = this;
            InitializeComponent();
        }
        /// <summary>
        /// Valider la modification d'un client entreprise (boutique) en changeant son contenu dans la BDD via requêtes SQL
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
                requete = "UPDATE boutique SET adresse_boutique='" + (Entreprises[0].Adresse_boutique) + "' WHERE nom_boutique='" + Entreprises[0].Nom_boutique + "';" +
                    "UPDATE boutique SET tel_boutique='" + (Entreprises[0].Tel_boutique) + "' WHERE nom_boutique='" + Entreprises[0].Nom_boutique + "';" +
                    "UPDATE boutique SET mail_boutique ='" + (Entreprises[0].Mail_boutique) + "' WHERE nom_boutique='" + Entreprises[0].Nom_boutique + "';" +
                    "UPDATE boutique SET nom_contact='" + (Entreprises[0].Nom_contact) + "' WHERE nom_boutique='" + Entreprises[0].Nom_boutique + "';" +
                    "UPDATE boutique SET remise=" + (Entreprises[0].Remise) + " WHERE nom_boutique='" + Entreprises[0].Nom_boutique + "';";
            }
            else
            {
                requete = "INSERT INTO boutique VALUES ('" +
                    Entreprises[0].Nom_boutique + "', '" +
                    Entreprises[0].Adresse_boutique + "', '" +
                    Entreprises[0].Tel_boutique + "', '" +
                    Entreprises[0].Mail_boutique + "','" +
                    Entreprises[0].Nom_contact + "', " +
                    Entreprises[0].Remise + ");";
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

            Gestion_Client_Entreprise gestion = new Gestion_Client_Entreprise(access);
            gestion.Show();
            gestion.Liste1.Items.Refresh();
            this.Close();
        }
    }
}
