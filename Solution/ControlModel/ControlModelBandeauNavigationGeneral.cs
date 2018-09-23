using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.ControlModel
{
    public class ControlModelBandeauNavigationGeneral
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        public ControlModelBandeauNavigationGeneral(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;
        }
    }
}
