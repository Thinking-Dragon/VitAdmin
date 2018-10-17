using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    // TODO: Max
    public class Citoyen
    {
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String NomComplet { get { return string.Format("{0} {1}", Nom, Prenom); } }
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
        }


        bool ValiderTelephone() { return new bool(); }
        bool ValiderAssMaladie() { return new bool(); }
        bool ValiderDateNaissance() { return new bool(); }
        String FormaterAdresse() { return ""; }
        DateTime AvoirDateNaissance() { return new DateTime(); }
    }
}