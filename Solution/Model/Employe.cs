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
        String NumEmploye { get; set; }
        String Poste { get; set; }
        String NumPermis { get; set; }
        String NAS { get; set; }

        void GenererNumEmploye() { }
    }
}
