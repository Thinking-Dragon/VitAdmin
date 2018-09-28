using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;
using VitAdmin.View;
using VitAdmin.Model;

namespace VitAdmin.ViewModel
{
    public class ViewModelPatientHospitalisation
    {
        public Citoyen Patient { get; set; }
        private GestionnaireEcrans GestionnaireEcrans { get; set; }


        public ViewModelPatientHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
            => GestionnaireEcrans = gestionnaireEcrans;

        /*ici les infos du patient genre nom, départ., etc.*/
    }
}
