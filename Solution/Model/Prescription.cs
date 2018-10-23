using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class Prescription : Evenement
    {
        public string Produit { get; set; }
        public string Posologie { get; set; }
        public DateTime DateDebut { get; set; }
        public string DateDebutISO { get; set; }
        public int NbJour { get; set; }

        public Prescription(string produit, string posologie, DateTime dateDebut, int nbJour, bool estNotifier)
        {
            Produit = produit;
            Posologie = posologie;
            DateDebut = dateDebut;
            NbJour = nbJour;
            EstNotifier = estNotifier;
        }

        public Prescription()
        {

        }

        public void addISODateDebut()
        {
            DateDebutISO = DateDebut.ToString("yyyy-MM-dd h:mm tt");
        }
    }
}
