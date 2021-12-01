using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Boutique
    {
        string nom_boutique;
        string adresse_boutique;
        string tel_boutique;
        string mail_boutique;
        string nom_contact;
        float remise;

        public Boutique(string nb, string ab, string tb, string mb, string nc, float r)
        {
            nom_boutique = nb;
            adresse_boutique = ab;
            tel_boutique = tb;
            mail_boutique = mb;
            nom_contact = nc;
            remise = r;
        }

        public Boutique()
        {
            nom_boutique = "";
            adresse_boutique = "";
            tel_boutique = "";
            mail_boutique = "";
            nom_contact = "";
            remise = 0;
        }

        public string Nom_boutique { get { return nom_boutique; } set { nom_boutique = value; } }
        public string Adresse_boutique { get { return adresse_boutique; } set { adresse_boutique = value; } }
        public string Tel_boutique { get { return tel_boutique; } set { tel_boutique = value; } }
        public string Mail_boutique { get { return mail_boutique; } set { mail_boutique = value; } }
        public string Nom_contact { get { return nom_contact; } set { nom_contact = value; } }
        public float Remise { get { return remise; } set { remise = value; } }

        public override string ToString()
        {
            return "Nom boutique : " + nom_boutique;
        }
    }
}
