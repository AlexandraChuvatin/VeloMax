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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Utilisateur> users;
        public List<Utilisateur> Users { get { return users; } set { users = value; } }

        /// <summary>
        /// Constructeur
        /// </summary>
        public MainWindow()
        {
            Utilisateur directeur = new Utilisateur("Legrand", "root");
            Utilisateur antoine = new Utilisateur("Antoine","root");
            Utilisateur alex = new Utilisateur("Alexandra", "root");
            Utilisateur bozo = new Utilisateur("Bozo", "bozo");
            users = new List<Utilisateur> { directeur, antoine, alex, bozo };
            this.DataContext = this;
            InitializeComponent();
        }

        /// <summary>
        /// Acces au menu avec l'utilisateur root ou bozo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Valider(object sender, RoutedEventArgs e)
        {
            foreach(Utilisateur u in listeView1.SelectedItems)
            {
                if(u.Mdp== PassBox.Password)
                {
                    if(u.Mdp=="bozo")
                    {
                        Menu menuB = new Menu("bozo");
                        menuB.Show();
                        this.Close();
                    }
                    else
                    {
                        Menu menu = new Menu("root");
                        menu.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect...");
                }
            }
        }
    }
}
