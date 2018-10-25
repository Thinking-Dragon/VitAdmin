using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelDossierPatient : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        Citoyen Patient { get; set; }

        public ViewModelProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Patient = patient;
        }

        public ICommand CmdBtnClicInfosPatient
        {
            get
            {
                return new CommandeDeleguee(action =>
                {

                    GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatientInfosModif(GestionnaireEcrans, Patient));

                });
            }
        }

        public ICommand CmdBtnCreerHospitalisation
        {
            get
            {
                return new CommandeDeleguee(action =>
                {

                    GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatientCreerHospitalisation(GestionnaireEcrans, Patient));

                });
            }
        }
    }
}
