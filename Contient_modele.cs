using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Contient_modele
    {
        int num_commande;
        string num_modele;
        int quantite_modele;

        public Contient_modele(int nc, string nm, int qm)
        {
            num_commande = nc;
            num_modele = nm;
            quantite_modele = qm;
        }

        public Contient_modele()
        {
            num_commande = 0;
            num_modele = null;
            quantite_modele = 0;
        }

        public int Num_commande { get { return num_commande; } set { num_commande = value; } }
        public string Num_modele { get { return num_modele; } set { num_modele = value; } }
        public int Quantite_modele { get { return quantite_modele; } set { quantite_modele = value; } }

        public override string ToString()
        {
            return "n° commande : " + this.num_commande + ", n° modèle : " + this.num_modele + ", Quantité : " + this.quantite_modele;
        }
    }
}
