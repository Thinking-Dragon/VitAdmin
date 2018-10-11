using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelDossierPatientInfosModif : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        Citoyen Patient { get; set; }

        public ViewModelProfessionnelDossierPatientInfosModif(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Patient = patient;
        }
    }
}
