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
    /// Logique d'interaction pour Modif_Commande.xaml
    /// </summary>
    public partial class Modif_Commande : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Commande> commandes;
        bool creer;

        public List<Commande> Commandes { get { return commandes; } set { commandes = value; } }
        public bool Creer { get { return creer; } set { creer = value; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="c">Commande à modifier</param>
        /// <param name="creer">creer commande (true) modifier commande (false)</param>
        /// <param name="access">root ou bozo</param>
        public Modif_Commande(Commande c, bool creer, string access)
        {
            commandes = new List<Commande> { c };
            this.creer = creer;
            this.access = access;
            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Valider la modification d'une commande en changeant son contenu dans la BDD via requêtes SQL
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
            string requete = null;
            if (Creer == false)
            {
                requete = "UPDATE commande SET date_commande='" + (Commandes[0].Date_commande.ToString("yyyy-MM-dd")) + "' WHERE num_commande=" + Commandes[0].Num_commande + ";" +
                    "UPDATE commande SET adresse_livraison ='" + (Commandes[0].Adresse_livraison) + "' WHERE num_commande=" + Commandes[0].Num_commande + ";" +
                    "UPDATE commande SET date_livraison='" + (Commandes[0].Date_livraison.ToString("yyyy-MM-dd")) + "' WHERE num_commande=" + Commandes[0].Num_commande + ";" +
                    "UPDATE commande SET nom_individu='" + (Commandes[0].Nom_individu) + "' WHERE num_commande=" + Commandes[0].Num_commande + ";" +
                    "UPDATE commande SET nom_boutique='" + (Commandes[0].Nom_boutique) + "' WHERE num_commande=" + Commandes[0].Num_commande + ";";
            }
            else
            {
                if(Commandes[0].Nom_boutique == null)
                {
                    requete = "INSERT INTO commande VALUES (" +
                    Commandes[0].Num_commande + ", '" +
                    Commandes[0].Date_commande.ToString("yyyy-MM-dd") + "', '" +
                    Commandes[0].Adresse_livraison + "', '" +
                    Commandes[0].Date_livraison.ToString("yyyy-MM-dd") + "','" +
                    Commandes[0].Nom_individu + "', null);";
                }
                else if(Commandes[0].Nom_individu == null)
                {
                    requete = "INSERT INTO commande VALUES (" +
                    Commandes[0].Num_commande + ", '" +
                    Commandes[0].Date_commande.ToString("yyyy-MM-dd") + "', '" +
                    Commandes[0].Adresse_livraison + "', '" +
                    Commandes[0].Date_livraison.ToString("yyyy-MM-dd") + "',null, '" +
                    Commandes[0].Nom_boutique + "');";
                }
                
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

            Gestion_Commande gestion = new Gestion_Commande(access);
            gestion.Show();
            gestion.Liste1.Items.Refresh();
            this.Close();
        }

        /// <summary>
        /// Accède à la fenêtre qui permet de modifier le contenu d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modif_Contenu(object sender, RoutedEventArgs e)
        {
            Modif_Contenu_Commande modif = new Modif_Contenu_Commande(access, Commandes[0]);
            modif.Show();
            this.Close();
        }
    }
}
