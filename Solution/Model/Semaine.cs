using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{
    public class Semaine
    {
        public DateTime PremierJour { get; set; }

        bool GenererHoraire() { return true; }
    }
}
