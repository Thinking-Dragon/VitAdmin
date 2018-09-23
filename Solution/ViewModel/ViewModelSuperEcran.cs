using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.ViewModel
{
    public class ViewModelSuperEcran
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        public ViewModelSuperEcran(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;
        }
    }
}
