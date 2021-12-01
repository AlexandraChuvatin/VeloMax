using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Fournisseur
    {
        #region Attributs
        string siret;
        string nom_entreprise;
        string contact_fournisseur;
        string adresse_fournisseur;
        int libelle;
        #endregion

        #region Constructeurs

        public Fournisseur(string s, string ne, string cf, string af, int l)
        {
            siret = s;
            nom_entreprise = ne;
            contact_fournisseur = cf;
            adresse_fournisseur = af;
            libelle = l;
        }

        public Fournisseur()
        {
            siret = null;
            nom_entreprise = null;
            contact_fournisseur = null;
            adresse_fournisseur = null;
            libelle = 0;
        }
        #endregion

        #region Propriétés
        public string Siret { get { return siret; } set { siret = value; } }
        public string Nom_entreprise { get { return nom_entreprise; } set { nom_entreprise = value; } }
        public string Contact_fournisseur { get { return contact_fournisseur; } set { contact_fournisseur = value; } }
        public string Adresse_fournisseur { get { return adresse_fournisseur; } set { adresse_fournisseur = value; } }
        public int Libelle { get { return libelle; } set { libelle = value; } }
        #endregion

        public override string ToString()
        {
            return "n° siret : " + this.siret + ", Nom entreprise : " + this.nom_entreprise;
        }
    }
}
