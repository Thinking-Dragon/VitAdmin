using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Chambre
    {
        public List<Equipement> Equipements { get; set; }
        public Departement UnDepartement { get; set; }

        public String Nom { get; set; }

    }
}
