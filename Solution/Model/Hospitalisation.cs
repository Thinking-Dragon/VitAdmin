using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class Hospitalisation
    {
        public DateTime DateDebut { get; set; }
        private DateTime? _dateFin;
        public DateTime? DateFin
        {
            get { return _dateFin; }
            set { _dateFin = value == new DateTime(0) ? null : value; }
        }
        public string Contexte { get; set; }
       
        public List<Traitement> LstTraitements { get; set; }
        public List<Symptome> LstSymptomes { get; set; }

        public Hospitalisation()
        {
        }

        public Hospitalisation(DateTime dt)
        {
            DateDebut = dt;
        }
    }
}
