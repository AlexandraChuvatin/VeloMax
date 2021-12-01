using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    class Fidelio
    {
        int num_programme;
        string description_programme;
        float cout_programme;
        string duree_programme;
        float rabais_programme;

        public Fidelio(int np, string dp, float cp, string duree, float rp)
        {
            num_programme = np;
            description_programme = dp;
            cout_programme = cp;
            duree_programme = duree;
            rabais_programme = rp;
        }

        public Fidelio()
        {
            num_programme = 0;
            description_programme = null;
            cout_programme = 0;
            duree_programme = null;
            rabais_programme = 0;
        }

        public int Numero_programme { get { return num_programme; } set { num_programme = value; } }
        public string Description_programme { get { return description_programme; } set { description_programme = value; } }
        public float Cout_programme { get { return cout_programme; } set { cout_programme = value; } }
        public string Duree_programme { get { return duree_programme; } set { duree_programme = value; } }
        public float Rabais_programme { get { return rabais_programme; } set { rabais_programme = value; } }

        public override string ToString()
        {
            return "n° programme : " + this.num_programme;
        }
    }
}
