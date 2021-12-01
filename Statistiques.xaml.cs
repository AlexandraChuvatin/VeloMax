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
    /// Logique d'interaction pour Statistiques.xaml
    /// </summary>
    public partial class Statistiques : Window
    {
        #region attributs
        //utilisateur console (root ou bozo)
        string access;
        //Individus par abonnement
        List<Individu> individus0 = new List<Individu>();
        List<Individu> individus1 = new List<Individu>();
        List<Individu> individus2 = new List<Individu>();
        List<Individu> individus3 = new List<Individu>();
        List<Individu> individus4 = new List<Individu>();
        //boutiques
        List<Boutique> boutiques = new List<Boutique>();
        //contenu des commandes
        List<Contient_modele> commande_modele = new List<Contient_modele>();
        List<Contient_piece> commande_piece = new List<Contient_piece>();
        //nbr total de quantités vendues
        int total_quantite = 0;
        //listes avec les quantites de pieces/modeles par commande (utilisé pour moyenne)
        List<int> quantite_m = new List<int>();
        List<int> quantite_p = new List<int>();
        //moyenne de pièces et modèles par commande
        double moyenne_modele = 0;
        double moyenne_piece = 0;
        //total argent pour toutes les ventes
        float total_vente = 0;
        //somme des quantités pour trouver le meilleur client
        int somme_quantites = 0;
        //nombre de clients chez VeloMax
        int nb_clients;
        #endregion

        #region propriétés
        public List<Individu> Individus0 { get { return individus0; } set { individus0 = value; } }
        public List<Individu> Individus1 { get { return individus1; } set { individus1 = value; } }
        public List<Individu> Individus2 { get { return individus2; } set { individus2 = value; } }
        public List<Individu> Individus3 { get { return individus3; } set { individus3 = value; } }
        public List<Individu> Individus4 { get { return individus4; } set { individus4 = value; } }
        public List<Boutique> Boutiques { get { return boutiques; } set { boutiques = value; } }


        public List<Contient_modele> Commande_modele { get { return commande_modele; } set { commande_modele = value; } }
        public List<Contient_piece> Commande_piece { get { return commande_piece; } set { commande_piece = value; } }

        public List<int> Quantite_m { get { return quantite_m; } set { quantite_m = value; } }
        public List<int> Quantite_p { get { return quantite_p; } set { quantite_p = value; } }

        public int Total_quantite { get { return total_quantite; } set { total_quantite = value; } }
        public float Total_vente { get { return total_vente; } set { total_vente = value; } }
        public string Access { get { return access; } }
        public int Nb_clients { get { return nb_clients; } }

        public double Moyenne_modele { get { return moyenne_modele; } set { moyenne_modele = value; } }
        public double Moyenne_piece { get { return moyenne_piece; } set { moyenne_piece = value; } }
        public int Somme_quantites { get { return somme_quantites; } set { somme_quantites = value; } }
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="access">bozo ou root</param>
        public Statistiques(string access)
        {
            this.access = access;
            #region requetes
            string requete0 = "SELECT * FROM individu WHERE num_programme is null;";
            string requete1 = "SELECT * FROM velomax.individu WHERE num_programme=1;";
            string requete2 = "SELECT * FROM velomax.individu WHERE num_programme=2;";
            string requete3 = "SELECT * FROM velomax.individu WHERE num_programme=3;";
            string requete4 = "SELECT * FROM velomax.individu WHERE num_programme=4;";

            string requeteB = "SELECT * FROM velomax.boutique;";

            string requete5 = "SELECT cm.num_commande, cm.num_modele, cm.quantite_modele, m.prix_modele FROM contient_modele cm JOIN modele m ON m.num_modele=cm.num_modele;";
            string requete6 = "SELECT cp.num_commande, cp.num_piece, cp.quantite_piece, p.prix_piece FROM contient_piece cp JOIN piece p ON p.num_piece=cp.num_piece;";
            #endregion

            #region individus sans programme 
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
            MySqlCommand command0 = maConnexion.CreateCommand();
            command0.CommandText = requete0;

            MySqlDataReader reader0 = command0.ExecuteReader();

            while (reader0.Read())
            {
                string nom_individu;
                if (reader0["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader0["nom_individu"]; }
                string prenom_individu;
                if (reader0["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader0["prenom_individu"]; }
                string adresse_individu = (string)reader0["adresse_individu"];
                string tel_individu = (string)reader0["tel_individu"];
                string mail_individu = (string)reader0["mail_individu"];
                DateTime date_adhesion;
                if (reader0["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader0["date_adhesion"]; }
                int num_programme;
                if (reader0["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader0["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                individus0.Add(i);
            }
            reader0.Close();
            command0.Dispose();
            maConnexion.Close();
            #endregion

            #region individus programme 1
            MySqlConnection maConnexion1 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion1 = new MySqlConnection(connexionString);
                maConnexion1.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command1 = maConnexion1.CreateCommand();
            command1.CommandText = requete1;

            MySqlDataReader reader1 = command1.ExecuteReader();

            while (reader1.Read())
            {
                string nom_individu;
                if (reader1["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader1["nom_individu"]; }
                string prenom_individu;
                if (reader1["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader1["prenom_individu"]; }
                string adresse_individu = (string)reader1["adresse_individu"];
                string tel_individu = (string)reader1["tel_individu"];
                string mail_individu = (string)reader1["mail_individu"];
                DateTime date_adhesion;
                if (reader1["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader1["date_adhesion"]; }
                int num_programme;
                if (reader1["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader1["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                individus1.Add(i);
            }
            reader1.Close();
            command1.Dispose();
            maConnexion1.Close();
            #endregion

            #region individus programme 2
            MySqlConnection maConnexion2 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion2 = new MySqlConnection(connexionString);
                maConnexion2.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command2 = maConnexion2.CreateCommand();
            command2.CommandText = requete2;

            MySqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                string nom_individu;
                if (reader2["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader2["nom_individu"]; }
                string prenom_individu;
                if (reader2["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader2["prenom_individu"]; }
                string adresse_individu = (string)reader2["adresse_individu"];
                string tel_individu = (string)reader2["tel_individu"];
                string mail_individu = (string)reader2["mail_individu"];
                DateTime date_adhesion;
                if (reader2["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader2["date_adhesion"]; }
                int num_programme;
                if (reader2["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader2["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                individus2.Add(i);
            }
            reader2.Close();
            command2.Dispose();
            maConnexion2.Close();
            #endregion

            #region individus programme 3
            MySqlConnection maConnexion3 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion3 = new MySqlConnection(connexionString);
                maConnexion3.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command3 = maConnexion3.CreateCommand();
            command3.CommandText = requete3;

            MySqlDataReader reader3 = command3.ExecuteReader();

            while (reader3.Read())
            {
                string nom_individu;
                if (reader3["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader3["nom_individu"]; }
                string prenom_individu;
                if (reader3["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader3["prenom_individu"]; }
                string adresse_individu = (string)reader3["adresse_individu"];
                string tel_individu = (string)reader3["tel_individu"];
                string mail_individu = (string)reader3["mail_individu"];
                DateTime date_adhesion;
                if (reader3["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader3["date_adhesion"]; }
                int num_programme;
                if (reader3["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader3["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                individus3.Add(i);
            }
            reader3.Close();
            command3.Dispose();
            maConnexion3.Close();
            #endregion

            #region individus programme 4
            MySqlConnection maConnexion4 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion4 = new MySqlConnection(connexionString);
                maConnexion4.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command4 = maConnexion4.CreateCommand();
            command4.CommandText = requete4;

            MySqlDataReader reader4 = command4.ExecuteReader();

            while (reader4.Read())
            {
                string nom_individu;
                if (reader4["nom_individu"] == DBNull.Value) { nom_individu = null; } else { nom_individu = (string)reader4["nom_individu"]; }
                string prenom_individu;
                if (reader4["prenom_individu"] == DBNull.Value) { prenom_individu = null; } else { prenom_individu = (string)reader4["prenom_individu"]; }
                string adresse_individu = (string)reader4["adresse_individu"];
                string tel_individu = (string)reader4["tel_individu"];
                string mail_individu = (string)reader4["mail_individu"];
                DateTime date_adhesion;
                if (reader4["date_adhesion"] == DBNull.Value) { date_adhesion = DateTime.MinValue; } else { date_adhesion = (DateTime)reader4["date_adhesion"]; }
                int num_programme;
                if (reader4["num_programme"] == DBNull.Value) { num_programme = 0; } else { num_programme = (int)reader4["num_programme"]; }

                //Remplir la Liste
                Individu i = new Individu(nom_individu, prenom_individu, adresse_individu, tel_individu, mail_individu, date_adhesion, num_programme);
                individus4.Add(i);
            }
            reader4.Close();
            command4.Dispose();
            maConnexion4.Close();
            #endregion

            #region Boutiques
            MySqlConnection maConnexionB = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexionB = new MySqlConnection(connexionString);
                maConnexionB.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand commandB = maConnexionB.CreateCommand();
            commandB.CommandText = requeteB;

            MySqlDataReader readerB = commandB.ExecuteReader();

            while (readerB.Read())
            {
                string nom_boutique;
                if (readerB["nom_boutique"] == DBNull.Value) { nom_boutique = null; } else { nom_boutique = (string)readerB["nom_boutique"]; }
                string adresse_boutique;
                if (readerB["adresse_boutique"] == DBNull.Value) { adresse_boutique = null; } else { adresse_boutique = (string)readerB["adresse_boutique"]; }
                string tel_boutique = (string)readerB["tel_boutique"];
                string mail_boutique = (string)readerB["mail_boutique"];
                string nom_contact = (string)readerB["nom_contact"];
                float remise;
                if (readerB["remise"] == DBNull.Value) { remise = 0; } else { remise = (float)readerB["remise"]; }

                //Remplir la Liste
                Boutique b = new Boutique(nom_boutique, adresse_boutique, tel_boutique, mail_boutique, nom_contact, remise);
                boutiques.Add(b);
            }
            readerB.Close();
            commandB.Dispose();
            maConnexionB.Close();
            #endregion

            #region commandes de modeles
            MySqlConnection maConnexion5 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion5 = new MySqlConnection(connexionString);
                maConnexion5.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command5 = maConnexion5.CreateCommand();
            command5.CommandText = requete5;

            MySqlDataReader reader5 = command5.ExecuteReader();
            //float total_temp0 = 0;
            while (reader5.Read())
            {
                int num_commande;
                if (reader5["num_commande"] == DBNull.Value) { num_commande = 0; } else { num_commande = (int)reader5["num_commande"]; }
                string num_modele;
                if (reader5["num_modele"] == DBNull.Value) { num_modele = null; } else { num_modele = (string)reader5["num_modele"]; }
                int quantite_modele;
                if (reader5["quantite_modele"] == DBNull.Value) { quantite_modele = 0; } else { quantite_modele = (int)reader5["quantite_modele"]; }
                float prix_modele;
                if (reader5["prix_modele"] == DBNull.Value) { prix_modele = 0; } else { prix_modele = (float)reader5["prix_modele"]; }

                //Remplir la Liste
                Contient_modele cm = new Contient_modele(num_commande, num_modele, quantite_modele);
                commande_modele.Add(cm);
                total_vente += prix_modele; // pour avoir prix total des ventes
                quantite_m.Add(quantite_modele);
                //total_temp0 += prix_modele;
            }
            //if(total_temp0>best_client)
            //{

            //}
            reader5.Close();
            command5.Dispose();
            maConnexion5.Close();
            #endregion

            #region commandes de pieces
            MySqlConnection maConnexion6 = null;
            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=" + access + ";PASSWORD=" + access + ";";

                maConnexion6 = new MySqlConnection(connexionString);
                maConnexion6.Open();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(" ErreurConnexion : " + e.ToString());
                return;
            }
            MySqlCommand command6 = maConnexion6.CreateCommand();
            command6.CommandText = requete6;

            MySqlDataReader reader6 = command6.ExecuteReader();

            while (reader6.Read())
            {
                int num_commande;
                if (reader6["num_commande"] == DBNull.Value) { num_commande = 0; } else { num_commande = (int)reader6["num_commande"]; }
                string num_piece;
                if (reader6["num_piece"] == DBNull.Value) { num_piece = null; } else { num_piece = (string)reader6["num_piece"]; }
                int quantite_piece;
                if (reader6["quantite_piece"] == DBNull.Value) { quantite_piece = 0; } else { quantite_piece = (int)reader6["quantite_piece"]; }
                float prix_piece;
                if (reader6["prix_piece"] == DBNull.Value) { prix_piece = 0; } else { prix_piece = (float)reader6["prix_piece"]; }

                //Remplir la Liste
                Contient_piece cp = new Contient_piece(num_commande, num_piece, quantite_piece);
                commande_piece.Add(cp);
                total_vente += prix_piece;
                quantite_p.Add(quantite_piece);
            }
            reader6.Close();
            command6.Dispose();
            maConnexion6.Close();
            #endregion

            for (int i = 0; i < commande_piece.Count(); i++)
            {
                total_quantite += commande_piece[i].Quantite_piece;
            }
            for (int i = 0; i < commande_modele.Count(); i++)
            {
                total_quantite += commande_modele[i].Quantite_modele;
            }

            nb_clients = individus0.Count() + individus1.Count() + individus2.Count() + individus3.Count() + individus4.Count() + boutiques.Count();

            //moyenne de nombre de modèles par commande
            for (int i = 0; i < quantite_m.Count(); i++)
            {
                moyenne_modele += quantite_m[i];
            }
            moyenne_modele = Math.Round((moyenne_modele / quantite_m.Count()), 2);

            //moyenne de nombre de pièces par commande
            for (int i = 0; i < quantite_p.Count(); i++)
            {
                moyenne_piece += quantite_p[i];
            }
            moyenne_piece = Math.Round((moyenne_piece / quantite_p.Count()), 2);

            this.DataContext = this;
            InitializeComponent();
        }


        //public void BestClient()
        //{

        //}
        #region Boutons
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
        /// Export des clients dont le programme arrive à terme en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "clientsFinExport.json";

            //informations sur les clients
            List<string> noms = new List<string>();
            List<string> prenoms = new List<string>();
            List<string> adresses = new List<string>();
            List<string> tels = new List<string>();
            List<string> mails = new List<string>();
            List<string> dates = new List<string>();
            List<string> prog = new List<string>();

            for (int i = 0; i < individus0.Count(); i++)
            {
                noms.Add(individus0[i].Nom_individu);
                prenoms.Add(individus0[i].Prenom_individu);
                adresses.Add(individus0[i].Adresse_individu);
                tels.Add(individus0[i].Tel_individu);
                mails.Add(individus0[i].Mail_individu);
                if (DateTime.Compare(individus0[i].Fin_programme, DateTime.Now) > 0)
                {
                    dates.Add("false");
                }
                else { dates.Add(individus0[i].Date_adhesion.ToString("dd-MM-yyyy")); }
                prog.Add(individus0[i].Num_programme.ToString());
            }
            for (int i = 0; i < individus1.Count(); i++)
            {
                noms.Add(individus1[i].Nom_individu);
                prenoms.Add(individus1[i].Prenom_individu);
                adresses.Add(individus1[i].Adresse_individu);
                tels.Add(individus1[i].Tel_individu);
                mails.Add(individus1[i].Mail_individu);
                if (DateTime.Compare(individus1[i].Fin_programme, DateTime.Now) > 0)
                {
                    dates.Add("false");
                }
                else { dates.Add(individus1[i].Date_adhesion.ToString("dd-MM-yyyy")); }
                prog.Add(individus1[i].Num_programme.ToString());
            }
            for (int i = 0; i < individus2.Count(); i++)
            {
                noms.Add(individus2[i].Nom_individu);
                prenoms.Add(individus2[i].Prenom_individu);
                adresses.Add(individus2[i].Adresse_individu);
                tels.Add(individus2[i].Tel_individu);
                mails.Add(individus2[i].Mail_individu);
                if (DateTime.Compare(individus2[i].Fin_programme, DateTime.Now) > 0)
                {
                    dates.Add("false");
                }
                else { dates.Add(individus2[i].Date_adhesion.ToString("dd-MM-yyyy")); }
                prog.Add(individus2[i].Num_programme.ToString());
            }
            for (int i = 0; i < individus3.Count(); i++)
            {
                noms.Add(individus3[i].Nom_individu);
                prenoms.Add(individus3[i].Prenom_individu);
                adresses.Add(individus3[i].Adresse_individu);
                tels.Add(individus3[i].Tel_individu);
                mails.Add(individus3[i].Mail_individu);
                if (DateTime.Compare(individus3[i].Fin_programme, DateTime.Now) > 0)
                {
                    dates.Add("false");
                }
                else { dates.Add(individus3[i].Date_adhesion.ToString("dd-MM-yyyy")); }
                prog.Add(individus3[i].Num_programme.ToString());
            }
            for (int i = 0; i < individus4.Count(); i++)
            {
                noms.Add(individus4[i].Nom_individu);
                prenoms.Add(individus4[i].Prenom_individu);
                adresses.Add(individus4[i].Adresse_individu);
                tels.Add(individus4[i].Tel_individu);
                mails.Add(individus4[i].Mail_individu);
                if (DateTime.Compare(individus4[i].Fin_programme, DateTime.Now) > 0)
                {
                    dates.Add("false");
                }
                else { dates.Add(individus4[i].Date_adhesion.ToString("dd-MM-yyyy")); }
                prog.Add(individus4[i].Num_programme.ToString());
            }

            //instanciation des "writer"
            StreamWriter writer = new StreamWriter(monFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            //debut du fichier Json
            jwriter.WriteStartObject();

            //debut du tableau Json
            jwriter.WritePropertyName("Clients");
            jwriter.WriteStartArray();
            for (int index = 0; index <= noms.Count() - 1; index++)
            {
                if (dates[index] != "false" && dates[index] != "01-01-0001")
                {
                    jwriter.WriteStartObject();
                    jwriter.WritePropertyName("Nom");
                    jwriter.WriteValue(noms[index]);
                    jwriter.WritePropertyName("Prenom");
                    jwriter.WriteValue(prenoms[index]);
                    jwriter.WritePropertyName("Adresse");
                    jwriter.WriteValue(adresses[index]);
                    jwriter.WritePropertyName("Téléphone");
                    jwriter.WriteValue(tels[index]);
                    jwriter.WritePropertyName("Mail");
                    jwriter.WriteValue(mails[index]);
                    jwriter.WritePropertyName("Date d'adhésion");
                    jwriter.WriteValue(dates[index]);
                    jwriter.WritePropertyName("Numéro programme");
                    jwriter.WriteValue(prog[index]);
                    jwriter.WriteEndObject();
                }
            }
            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //fermeture de "writer"
            jwriter.Close();
            writer.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        #endregion
    }
}
