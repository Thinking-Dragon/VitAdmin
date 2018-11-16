using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    // TODO: Max
    public class Citoyen
    {
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String NomComplet { get { return string.Format("{0} {1}", Nom, Prenom); } set { } }
        public String AssMaladie { get; set; }
        public DateTime DateNaissance { get; set; }
        public String Adresse { get; set; }
        public String NumTelephone { get; set; }
        public Genre UnGenre { get; set; }
        public List<Hospitalisation> Hospitalisations { get; set; }
        public Lit Lit { get; set; }

        public static int iMAX_CARAC_NOMPRENOM = 15;
        public static int iMIN_CARAC_NOMPRENOM = 2;
        public static int iCaracteresTelephone = 10;
        public static int iCaracteresAssMaladie = 12;
        public static int iMaxAdresse = 100;

        public Citoyen(String NumAssMaladie)
        {
            AssMaladie = NumAssMaladie;
            
        }

        public Citoyen()
        {
            DateNaissance = new DateTime(1980, 1, 1);
        }

        public bool ValiderInfos()
        {
            
            bool bInfosValide = false;

            bInfosValide =  ValiderNom() &&
                            ValiderPrenom() &&
                            ValiderAssMaladie() &&
                            ValiderTelephone() &&
                            ValiderAdresse();
    

            return bInfosValide;
        }

        public bool ValiderTelephone()
        {
            if (NumTelephone != null)
                return (NumTelephone.Length == 0 || NumTelephone.Length == iCaracteresTelephone);
            else
                return false;
        }
        public bool ValiderAssMaladie()
        {
            Regex regexAssMaladie = new Regex("[A-Z a-z]{4}[0-9]{8}");
            if (AssMaladie != null)
                return (AssMaladie.Length == iCaracteresAssMaladie && regexAssMaladie.IsMatch(AssMaladie));
            else
                return false;
        }
        public bool ValiderPrenom()
        {
            if (Prenom != null)
                return (Prenom.Length < iMAX_CARAC_NOMPRENOM && Prenom.Length > iMIN_CARAC_NOMPRENOM);
            else
                return false;
        }
        public bool ValiderNom()
        {
            if (Nom != null)
                return (Nom.Length < iMAX_CARAC_NOMPRENOM && Nom.Length > iMIN_CARAC_NOMPRENOM);
            else
                return false;
        }
        public bool ValiderAdresse()
        {
            if (Adresse != null)
                return (Adresse.Length == 0 || Adresse.Length < iMaxAdresse);
            else
                return false;
        }


        String FormaterAdresse() { return ""; }
        DateTime AvoirDateNaissance() { return new DateTime(); }
    }
}