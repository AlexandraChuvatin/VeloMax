using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Piece
    {
        #region Attributs
        string num_piece;
        string description;
        string nom_fournisseur;
        string num_produit_catalogue;
        float prix_piece;
        DateTime date_intro_piece;
        DateTime date_disc_piece;
        string delai;
        int stock;
        #endregion

        #region Constructeurs
        public Piece(string num, string desc, string nom, string num_p, float prix, DateTime date1, DateTime date2, string delai, int stock)
        {
            num_piece = num;
            description = desc;
            nom_fournisseur = nom;
            num_produit_catalogue = num_p;
            prix_piece = prix;
            date_intro_piece = date1;
            date_disc_piece = date2;
            this.delai = delai;
            this.stock = stock;
        }
        public Piece()
        {
            num_piece = null;
            description = null;
            nom_fournisseur = "";
            num_produit_catalogue = "";
            prix_piece = 0;
            date_intro_piece = DateTime.Now.Date;
            date_disc_piece = DateTime.Now.Date;
            this.delai = "";
            this.stock = 0;
        }
        #endregion

        #region Propriétés
        public string Num_Piece { get { return num_piece; } set { num_piece = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Nom_fournisseur { get { return nom_fournisseur; } set { nom_fournisseur = value; } }
        public string Num_produit_catalogue { get { return num_produit_catalogue; } set { num_produit_catalogue = value; } }
        public float Prix_Piece { get { return prix_piece; } set { prix_piece = value; } }
        public DateTime Date_intro_piece { get { return date_intro_piece; } set { date_intro_piece = value; } }
        public DateTime Date_disc_piece { get { return date_disc_piece; } set { date_disc_piece = value; } }
        public string Delai { get { return delai; } set { delai = value; } }
        public int Stock { get { return stock; } set { stock = value; } }
        #endregion

        public override string ToString()
        {
            return "n° pièce : " + this.num_piece + ", description : " + this.description;
        }
    }
}
