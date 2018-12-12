using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ModelView
{
    public class ViewModelConnexion : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ICommand CmdAideContextuelle
            => new CommandeDeleguee(param => System.Diagnostics.Process.Start("http://420.cstj.qc.ca/vitadmin/GuideUtilisateur#Mconnexion"));

        public ViewModelConnexion(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
