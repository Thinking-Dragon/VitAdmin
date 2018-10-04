using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelDossierPatient : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewModelProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
        }
    }
}
