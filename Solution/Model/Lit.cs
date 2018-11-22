using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Lit
    {
        public int _identifiant { get; set; }
        public String Numero { get; set; }
        public Chambre Chambre { get; set; } 
        public EtatLit EtatLit { get; set; }
        public Citoyen Citoyen { get; set; }
        public bool EstDisponible => EtatLit != EtatLit.Occupé;

    }
}
