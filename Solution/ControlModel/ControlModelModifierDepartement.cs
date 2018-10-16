using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelModifierDepartement : ObjetObservable
    {
        public Departement Departement { get; set; }

        public ControlModelModifierDepartement(Departement departement)
            => Departement = departement;
    }
}
