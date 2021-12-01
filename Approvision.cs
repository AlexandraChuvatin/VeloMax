using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Approvision
    {
        string num_piece;
        string siret;

        public Approvision(string np, string s)
        {
            num_piece = np;
            siret = s;
        }
        public Approvision()
        {
            num_piece = null;
            siret = null;
        }

        public string Num_piece { get { return num_piece; }set { num_piece = value; } }
        public string Siret { get { return siret; } set { siret = value; } }

        public override string ToString()
        {
            return "n° pièce : " + this.num_piece + ", Siret fournisseur : " + this.siret;
        }
    }
}
