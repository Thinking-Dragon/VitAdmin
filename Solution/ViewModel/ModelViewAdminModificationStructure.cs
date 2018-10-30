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
    public class ModelViewAdminModificationStructure : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private Departement departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return departementSelectionne; }
            set { departementSelectionne = value; RaisePropertyChangedEvent("DepartementSelectionne"); }
        }

        public ICommand CmdEquipements
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    GestionnaireEcrans.Changer(new ViewGestionEquipements(GestionnaireEcrans));
                });
            }
        }

        public ModelViewAdminModificationStructure(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
