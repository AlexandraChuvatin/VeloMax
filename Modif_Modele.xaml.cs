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
    /// Logique d'interaction pour Modif_Modele.xaml
    /// </summary>
    public partial class Modif_Modele : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Modele> modele;
        bool creer;
        public List<Modele> Modele { get { return modele; } set { modele = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="m">modèle à modifier</param>
        /// <param name="creer">creer modèle (true) modifier modèle (false)</param>
        /// <param name="access">root ou bozo</param>
        public Modif_Modele(Modele m, bool creer, string access)
        {
            modele = new List<Modele> { m };
            this.creer = creer;
            this.access = access;
            this.DataContext = this;
            InitializeComponent();
        }
        /// <summary>
        /// Valider la modification d'un modèle en changeant son contenu dans la BDD via requêtes SQL
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
                requete = "UPDATE modele SET prix_modele=" + (Modele[0].Prix_modele) + " WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET ligne_produit='" + (Modele[0].Ligne_produit) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET nom_modele ='" + (Modele[0].Nom_modele) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET grandeur='" + (Modele[0].Grandeur) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET date_intro_modele='" + (Modele[0].Date_intro_modele.ToString("yyyy-MM-dd")) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET date_disc_modele='" + (Modele[0].Date_disc_modele.ToString("yyyy-MM-dd")) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET cadre='" + (Modele[0].Cadre) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET guidon='" + (Modele[0].Guidon) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET freins='" + (Modele[0].Freins) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET selle='" + (Modele[0].Selle) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET derailleur_avant='" + (Modele[0].Derailleur_avant) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET derailleur_arriere='" + (Modele[0].Derailleur_arriere) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET roue_avant='" + (Modele[0].Roue_avant) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET roue_arriere='" + (Modele[0].Roue_arriere) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET reflecteurs='" + (Modele[0].Reflecteurs) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET pedalier='" + (Modele[0].Pedalier) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET ordinateur='" + (Modele[0].Ordinateur) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET panier='" + (Modele[0].Panier) + "' WHERE num_modele='" + Modele[0].Num_modele + "';" +
                    "UPDATE modele SET stock_modele=" + (Modele[0].Stock_modele) + " WHERE num_modele='" + Modele[0].Num_modele + "';";
            }
            else
            {
                requete = "INSERT INTO modele VALUES ('" + 
                    Modele[0].Num_modele + "', '" + 
                    Modele[0].Prix_modele + "', '" + 
                    Modele[0].Ligne_produit + "', '" + 
                    Modele[0].Nom_modele + "'," + 
                    Modele[0].Grandeur + ", '" + 
                    Modele[0].Date_intro_modele.ToString("yyyy-MM-dd") + "', '" + 
                    Modele[0].Date_disc_modele.ToString("yyyy-MM-dd") + "', '" + 
                    Modele[0].Cadre + "','" + 
                    Modele[0].Guidon + "', '" + 
                    Modele[0].Freins + "', '" + 
                    Modele[0].Selle + "', '" + 
                    Modele[0].Derailleur_avant + "', '" + 
                    Modele[0].Derailleur_arriere + "', '" + 
                    Modele[0].Roue_avant + "', '" + 
                    Modele[0].Roue_arriere + "', '" + 
                    Modele[0].Reflecteurs + "', '" + 
                    Modele[0].Pedalier + "', '" + 
                    Modele[0].Ordinateur + "', '" + 
                    Modele[0].Panier + "', '" + 
                    Modele[0].Stock_modele + ") ;";
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

            Gestion_Modele gestion = new Gestion_Modele(access);
            gestion.Show();
            this.Close();
        }
    }
}
