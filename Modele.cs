using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Modele
    {
        #region Attributs
        string num_modele;
        float prix_modele;
        string ligne_produit;
        string nom_modele;
        string grandeur;
        DateTime date_intro_modele;
        DateTime date_disc_modele;
        string cadre;
        string guidon;
        string freins;
        string selle;
        string derailleur_avant;
        string derailleur_arriere;
        string roue_avant;
        string roue_arriere;
        string reflecteurs;
        string pedalier;
        string ordinateur;
        string panier;
        int stock_modele;
        #endregion

        #region Constructeurs
        public Modele(string num, float prix, string ligne, string nom, string g, DateTime d1, DateTime d2, string cadre, string guidon, string freins, string selle, string derailleur_avant, string derailleur_arriere, string roue_avant, string roue_arriere, string reflecteurs, string pedalier, string ordinateur, string panier, int stock)
        {
            num_modele = num;
            prix_modele = prix;
            ligne_produit = ligne;
            nom_modele = nom;
            grandeur = g;
            date_intro_modele = d1;
            date_disc_modele = d2;
            this.cadre = cadre;
            this.guidon = guidon;
            this.freins = freins;
            this.selle = selle;
            this.derailleur_avant = derailleur_avant;
            this.derailleur_arriere = derailleur_arriere;
            this.roue_avant = roue_avant;
            this.roue_arriere = roue_arriere;
            this.reflecteurs = reflecteurs;
            this.pedalier = pedalier;
            this.ordinateur = ordinateur;
            this.panier = panier;
            stock_modele = stock;
        }
        public Modele()
        {
            num_modele = null;
            prix_modele = 0;
            ligne_produit = null;
            nom_modele = null;
            grandeur = null;
            date_intro_modele = DateTime.Now.Date;
            date_disc_modele = DateTime.Now.Date;
            this.cadre = null;
            this.guidon = null;
            this.freins = null;
            this.selle = null;
            this.derailleur_avant = null;
            this.derailleur_arriere = null;
            this.roue_avant = null;
            this.roue_arriere = null;
            this.reflecteurs = null;
            this.pedalier = null;
            this.ordinateur = null;
            this.panier = null;
            stock_modele = 0;
        }
        #endregion

        #region Propriétés
        public string Num_modele { get { return num_modele; } set { num_modele = value; } }
        public float Prix_modele { get { return prix_modele; } set { prix_modele = value; } }
        public string Ligne_produit { get { return ligne_produit; } set { ligne_produit = value; } }
        public string Nom_modele { get { return nom_modele; } set { nom_modele = value; } }
        public string Grandeur { get { return grandeur; } set { grandeur = value; } }
        public DateTime Date_intro_modele { get { return date_intro_modele; } set { date_intro_modele = value; } }
        public DateTime Date_disc_modele { get { return date_disc_modele; } set { date_disc_modele = value; } }
        public string Cadre { get { return cadre; } set { cadre = value; } }
        public string Guidon { get { return guidon; } set { guidon = value; } }
        public string Freins { get { return freins; } set { freins = value; } }
        public string Selle { get { return selle; } set { selle = value; } }
        public string Derailleur_avant { get { return derailleur_avant; } set { derailleur_avant = value; } }
        public string Derailleur_arriere { get { return derailleur_arriere; } set { derailleur_arriere = value; } }
        public string Roue_avant { get { return roue_avant; } set { roue_avant = value; } }
        public string Roue_arriere { get { return roue_arriere; } set { roue_arriere = value; } }
        public string Reflecteurs { get { return reflecteurs; } set { reflecteurs = value; } }
        public string Pedalier { get { return pedalier; } set { pedalier = value; } }
        public string Ordinateur { get { return ordinateur; } set { ordinateur = value; } }
        public string Panier { get { return panier; } set { panier = value; } }
        public int Stock_modele { get { return stock_modele; } set { stock_modele = value; } }
        #endregion

        public override string ToString()
        {
            return "n° modèle : " + this.num_modele + ", Nom modèle : " + this.nom_modele;
        }
    }
}
