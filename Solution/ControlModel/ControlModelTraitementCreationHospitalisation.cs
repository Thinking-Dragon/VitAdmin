using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelTraitementCreationHospitalisation : ObjetObservable
    {
        public ObservableCollection<Traitement> Traitements { get; set; }

        public ControlModelTraitementCreationHospitalisation(ObservableCollection<Traitement> traitements)
        {
            Traitements = traitements;
        }
    }
}
