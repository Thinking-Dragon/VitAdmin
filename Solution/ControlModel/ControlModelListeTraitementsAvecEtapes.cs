using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelListeTraitementsAvecEtapes : ObjetObservable
    {
        public List<Traitement> Traitements { get; set; }

        public ControlModelListeTraitementsAvecEtapes(List<Traitement> traitements)
            => Traitements = traitements;
    }
}
