using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelPatientHospitalisation
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }


        public ViewModelPatientHospitalisation(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;


    }
}
