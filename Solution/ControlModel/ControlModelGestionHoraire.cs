using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{
    class ControlModelGestionHoraire
    {
        public Employe Employe { get; set; }
        public ControlModelGestionHoraire(Employe employe)
        {
            Employe = employe;
        }
    }
}
