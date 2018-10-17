using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;
using VitAdmin.Data;

namespace VitAdmin.ControlModel
{
    class ControlModelProfessionnelDossierPatient : ObjetObservable
    {
        public GestionnaireEcrans GestEcrans { get; set; }
        public Citoyen Patient { get; set; }
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

        public ControlModelProfessionnelDossierPatient(GestionnaireEcrans gestEcrans, ObservableCollection<Hospitalisation> hospitalisations, Citoyen patient)
        {
            Hospitalisations = hospitalisations;
            // TODO : C'est ici qui indique à la datagrid quel département est associé une hospitalisation!! Bug affichage du département tout le temps en chirurgie.
            DepartementAssocie = hospitalisations[0].LstTraitements[0].DepartementAssocie;
            GestEcrans = gestEcrans;
            Patient = patient;
        }

        public ICommand CmdMouseDoubleClickHospit
        {
            get
            {
                return new CommandeDeleguee(hospitalisationSelectionne =>
                {
                    if (hospitalisationSelectionne != null)
                        GestEcrans.Changer(new ViewPatientHospitalisation(GestEcrans, Patient, (Hospitalisation)hospitalisationSelectionne));

                });
            }
        }
    }
}
