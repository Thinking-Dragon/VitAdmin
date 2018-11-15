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

        public Citoyen(String NumAssMaladie)
        {
            AssMaladie = NumAssMaladie;
            
        }

        public Citoyen()
        {
            DateNaissance = new DateTime(1980, 1, 1);
        }

        bool ValiderInfos()
        {
            const int iMAX_CARAC_NOMPRENOM = 15;
            const int iMIN_CARAC_NOMPRENOM = 2;
            const int iCaracteresTelephone = 10;
            const int iCaracteresAssMaladie = 12;
            const int iMaxAdresse = 100;
            Regex regexAssMaladie = new Regex("[A-Z a-z]{4}[0-9]{8}");

            bool bInfosValide = false;

            // Nom du citoyen
            bInfosValide =  (Nom.Length < iMAX_CARAC_NOMPRENOM && Nom.Length > iMIN_CARAC_NOMPRENOM) &&
                            (Prenom.Length < iMAX_CARAC_NOMPRENOM && Prenom.Length > iMIN_CARAC_NOMPRENOM) &&
                            (AssMaladie.Length == iCaracteresAssMaladie && regexAssMaladie.IsMatch(AssMaladie)) &&
                            (NumTelephone.Length == 0 || NumTelephone.Length == iCaracteresTelephone) &&
                            (Adresse.Length == 0 || Adresse.Length == iMaxAdresse);
            /*// Prénom du citoyen
            bInfosValide = Prenom.Length < iMAX_CARAC_NOMPRENOM && Prenom.Length > iMIN_CARAC_NOMPRENOM;
            // NumAssuranceMaladie
            bInfosValide = AssMaladie.Length == iCaracteresAssMaladie && regexAssMaladie.IsMatch(AssMaladie);
            // Téléphone
            bInfosValide = NumTelephone.Length == 0 || NumTelephone.Length == iCaracteresTelephone;
            // Adresse
            bInfosValide = Num*/

            return new bool();
        }

        bool ValiderTelephone() { return new bool(); }
        bool ValiderAssMaladie()
        {

            return new bool();
        }
        bool ValiderDateNaissance() { return new bool(); }
        String FormaterAdresse() { return ""; }
        DateTime AvoirDateNaissance() { return new DateTime(); }
    }
}