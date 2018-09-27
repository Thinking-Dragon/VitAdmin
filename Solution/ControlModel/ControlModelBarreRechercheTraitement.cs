using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelBarreRechercheTraitement : ObjetObservable
    {
        public ObservableCollection<Traitement> Traitements { get; set; }

        public ControlModelBarreRechercheTraitement(ObservableCollection<Traitement> traitements)
            => Traitements = traitements;
    }
}
