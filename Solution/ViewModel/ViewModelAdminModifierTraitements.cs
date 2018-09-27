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
    public class ViewModelAdminModifierTraitements : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        public ObservableCollection<Traitement> Traitements { get; set; }

        public ViewModelAdminModifierTraitements(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Traitements = new ObservableCollection<Traitement>();
        }
    }
}
