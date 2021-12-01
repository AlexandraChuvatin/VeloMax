using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Contient_piece
    {
        int num_commande;
        string num_piece;
        int quantite_piece;

        public Contient_piece(int nc, string np, int qp)
        {
            num_commande = nc;
            num_piece = np;
            quantite_piece = qp;
        }

        public Contient_piece()
        {
            num_commande = 0;
            num_piece = null;
            quantite_piece = 0;
        }

        public int Num_commande { get { return num_commande; } set { num_commande = value; } }
        public string Num_piece { get { return num_piece; } set { num_piece = value; } }
        public int Quantite_piece { get { return quantite_piece; } set { quantite_piece = value; } }

        public override string ToString()
        {
            return "n° commande : " + this.num_commande + ", n° pièce : " + this.num_piece + ", Quantité : " + this.quantite_piece;
        }
    }
}
