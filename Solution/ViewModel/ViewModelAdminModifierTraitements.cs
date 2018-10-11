using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelAdminModifierTraitements : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        public ObservableCollection<Traitement> Traitements { get; set; }

        public ICommand CmdEnregistrer
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DataModelTraitement.PutTraitements(new List<Traitement>(Traitements));
                        GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));
                    }
                );
            }
        }

        public ViewModelAdminModifierTraitements(GestionnaireEcrans gestionnaireEcrans, List<Traitement> traitements)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Traitements = new ObservableCollection<Traitement>(traitements);
        }
    }
}
