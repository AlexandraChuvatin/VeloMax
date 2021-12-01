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
    /// Logique d'interaction pour Gestion_Piece.xaml
    /// </summary>
    public partial class Gestion_Piece : Window
    {
        string access;
        public string Access { get { return access; } }
        List<Piece> listePiece = new List<Piece>();
        public List<Piece> ListePiece { get { return listePiece; } set { listePiece = value; OnPropertyChanged("ListePiece"); } }

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
        public Gestion_Piece(string access)
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

            string requete = "Select * from piece;";
            MySqlCommand command1 = maConnexion.CreateCommand();
            command1.CommandText = requete;

            MySqlDataReader reader = command1.ExecuteReader();

            while (reader.Read())
            {
                string num_piece;
                if (reader["num_piece"] == DBNull.Value) { num_piece = null; } else { num_piece = (string)reader["num_piece"]; }
                string description;
                if (reader["description"] == DBNull.Value) { description = null; } else { description = (string)reader["description"]; }
                
                string nom_fournisseur = (string)reader["nom_fournisseur"];
                string num_produit_catalogue = (string)reader["num_produit_catalogue"];
                float prix_piece = (float)reader["prix_piece"];
                DateTime date_intro_piece = (DateTime)reader["date_intro_piece"];
                DateTime date_disc_piece = (DateTime)reader["date_disc_piece"];
                string delai = (string)reader["delai"];
                int stock_piece = (int)reader["stock_piece"];

                //Remplir la Liste
                Piece p = new Piece(num_piece, description, nom_fournisseur, num_produit_catalogue, prix_piece, date_intro_piece, date_disc_piece, delai, stock_piece);
                listePiece.Add(p);
                //Console.WriteLine(p);
            }
            reader.Close();
            command1.Dispose();
            maConnexion.Close();

            this.DataContext = this;
            InitializeComponent();
        }

        #region Boutons
        /// <summary>
        /// Supprimer une quantité de la pièce du stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Supprimer(object sender, RoutedEventArgs e)
        {
            foreach(Piece p in Liste1.SelectedItems)
            {
                MessageBoxResult result = MessageBox.Show(this, "Attention, êtes vous sûrs de vouloir supprimer : " + p + " de la liste ?", null, MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    if(access=="root")
                    {
                        if (p.Stock > 0) { p.Stock -= 1; } else { return; }
                    }
                    
                    
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

                    string requete = "UPDATE piece SET stock_piece="+ (p.Stock) +" WHERE num_piece='"+ p.Num_Piece +"';";
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
                }
            }
            Liste1.Items.Refresh();
        }
        /// <summary>
        /// Accès à la page de modification d'une pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier(object sender, RoutedEventArgs e)
        {
            foreach(Piece p in Liste1.SelectedItems)
            {
                Modif_Piece modif = new Modif_Piece(p, false, access);
                modif.Show();
                this.Close();
            }
        }
        /// <summary>
        /// Accès à la page de création d'une pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creer(object sender, RoutedEventArgs e)
        {
            Piece p = new Piece();
            Modif_Piece modif = new Modif_Piece(p, true, access);
            modif.Show();
            this.Close();
        }
        /// <summary>
        /// Retourn au menu principal
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
        /// Export de la table pièces en JSON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_JSON_Click(object sender, RoutedEventArgs e)
        {
            string monFichier = "piecesExport.json";

            StreamWriter fileWriter = new StreamWriter(monFichier);
            JsonTextWriter jsonWriter = new JsonTextWriter(fileWriter);

            // sérialisation des objets vers le flux d'écriture fichier
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, listePiece);

            //fermeture de "writer"
            jsonWriter.Close();
            fileWriter.Close();

            MessageBox.Show("Fichier JSON exporté avec succès !");
        }
        /// <summary>
        /// Export de la table pièces en XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_XML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Piece>));
            StreamWriter wr = new StreamWriter("piecesXML.xml");

            //sérialisation de bdtheque
            xs.Serialize(wr, listePiece);

            wr.Close();

            MessageBox.Show("Fichier XML exporté avec succès !");
        }
        #endregion
    }
}
