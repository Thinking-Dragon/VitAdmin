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
        public DateTime DateFin { get; set; }
       
        public List<Traitement> LstTraitements { get; set; }
        public List<Symptome> LstSymptomes { get; set; }
    }
}
