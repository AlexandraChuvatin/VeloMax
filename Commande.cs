using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Commande
    {
        int num_commande;
        DateTime date_commande;
        string adresse_livraison;
        DateTime date_livraison;
        string nom_individu;
        string nom_boutique;

        public Commande(int nc, DateTime dc, string al, DateTime dl, string ni, string nb)
        {
            num_commande = nc;
            date_commande = dc;
            adresse_livraison = al;
            date_livraison = dl;
            nom_individu = ni;
            nom_boutique = nb;
        }
        public Commande()
        {
            num_commande = 0;
            date_commande = DateTime.Now.Date;
            adresse_livraison = "";
            date_livraison = DateTime.Now.Date;
            nom_individu = null;
            nom_boutique = null;
        }

        public int Num_commande { get { return num_commande; } set { num_commande = value; } }
        public DateTime Date_commande { get { return date_commande; } set { date_commande = value; } }
        public string Adresse_livraison { get { return adresse_livraison; } set { adresse_livraison = value; } }
        public DateTime Date_livraison { get { return date_livraison; } set { date_livraison = value; } }
        public string Nom_individu { get { return nom_individu; } set { nom_individu = value; } }
        public string Nom_boutique { get { return nom_boutique; } set { nom_boutique = value; } }

        public override string ToString()
        {
            return "n° commande : " + this.num_commande + ", Nom individu : " + this.nom_individu;
        }

    }
}
