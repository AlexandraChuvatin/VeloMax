using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Utilisateur
    {
        string identifiant;
        string mdp;

        public Utilisateur(string i, string m)
        {
            identifiant = i;
            mdp = m;
        }

        public string Identifiant { get { return identifiant; } set { identifiant = value; } }
        public string Mdp { get { return mdp; } set { mdp = value; } }
    }
}
