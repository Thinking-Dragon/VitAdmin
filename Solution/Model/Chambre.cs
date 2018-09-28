using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Chambre
    {
        public List<Lit> Lits { get; set; }
        public List<Equipement> Equipements { get; set; }
        public Departement Departement { get; set; }

        public String Numero { get; set; }

    }
}
