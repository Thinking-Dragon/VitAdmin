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
    class ControlModelProfessionnelDossierPatient : ObjetObservable
    {
        public ObservableCollection<Hospitalisation> Hospitalisations { get; set; }
        private Departement DepartementAss { get; set; }
        public Departement DepartementAssocie
        {
            get
            {
                return DepartementAss;
            }

            set
            {
                DepartementAss = value;
                RaisePropertyChangedEvent("DepartementAssocie");
            }
        }

        public ControlModelProfessionnelDossierPatient(ObservableCollection<Hospitalisation> hospitalisations)
        {
            Hospitalisations = hospitalisations;
            DepartementAssocie = hospitalisations[0].LstTraitements[0].DepartementAssocie;
        }
    }
}
