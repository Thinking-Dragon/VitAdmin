using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.ModelView
{
    public class ViewModelConnexion : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewModelConnexion(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
