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
        String Nom { get; set; }
        String Prenom { get; set; }
        String AssMaladie { get; set; }
        DateTime DateNaissance { get; set; }
        String Adresse { get; set; }
        String NumTelephone { get; set; }

        bool ValiderTelephone() { return new bool(); }
        bool ValiderAssMaladie() { return new bool(); }
        bool ValiderDateNaissance() { return new bool(); }
        String FormaterAdresse() { return ""; }
    }
}
