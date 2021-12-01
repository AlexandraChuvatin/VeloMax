using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Probleme_BDD_AlexandraCHUVATIN_AntoineCALDICHOURY
{
    public class Individu
    {
        string nom_individu;
        string prenom_individu;
        string adresse_individu;
        string tel_individu;
        string mail_individu;
        DateTime date_adhesion;
        DateTime fin_programme;
        int num_programme; //0 si n'adhère à aucun programme

        public Individu(string nom, string prenom, string adresse, string tel, string mail, DateTime date, int np)
        {
            nom_individu = nom;
            prenom_individu = prenom;
            adresse_individu = adresse;
            tel_individu = tel;
            mail_individu = mail;
            date_adhesion = date;
            num_programme = np;

            //if(num_programme == 0)
            //{
            //    fin_programme = DateTime.Now.Date;
            //}

            if (num_programme == 1)
            {
                fin_programme = date_adhesion.AddYears(1);
            }
            else if (num_programme == 2 || num_programme == 3)
            {
                fin_programme = date_adhesion.AddYears(2);
            }
            else
            {
                fin_programme = date_adhesion.AddYears(3);
            }
        }

        public Individu()
        {
            nom_individu = "";
            prenom_individu = "";
            adresse_individu = "";
            tel_individu = "";
            mail_individu = "";
            date_adhesion = DateTime.Now.Date;
            num_programme = 0;
        }

        public string Nom_individu { get { return nom_individu; } set { nom_individu = value; } }
        public string Prenom_individu { get { return prenom_individu; } set { prenom_individu = value; } }
        public string Adresse_individu { get { return adresse_individu; } set { adresse_individu = value; } }
        public string Tel_individu { get { return tel_individu; } set { tel_individu = value; } }
        public string Mail_individu { get { return mail_individu; } set { mail_individu = value; } }
        public DateTime Date_adhesion { get { return date_adhesion; } set { date_adhesion = value; } }
        public DateTime Fin_programme { get { return fin_programme; } set { fin_programme = value; } }
        public int Num_programme { get { return num_programme; } set { num_programme = value; } }

        public override string ToString()
        {
            return "Nom : " + this.nom_individu + ", Prenom : " + this.prenom_individu;
        }
    }
}
