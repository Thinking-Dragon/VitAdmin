using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    // TODO: Max
    public class Employe : Citoyen
    {
        public String NumEmploye { get; set; }
        public String Poste { get; set; }
        public String NumPermis { get; set; }
        public String NAS { get; set; }

        public string idPrenomNom
        {
            get { return NumEmploye + " " + Prenom + " " + Nom; }
        }

        public List<QuartEmploye> LstQuartEmployes;
        public List<QuartIndisponible> LstQuartIndisponibles;
        public List<Evenement> LstEvenements;
        

        public void GenererNumEmploye() { }
    }
}
