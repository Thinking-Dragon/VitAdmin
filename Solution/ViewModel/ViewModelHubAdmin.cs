using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelHubAdmin : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ICommand CmdPersonnel => new CommandeDeleguee(obj => {
            GestionnaireEcrans.Changer(new ViewProfessionnelHubAdmin(GestionnaireEcrans, UsagerConnecte.Usager));
        });

        public ICommand CmdInfrastructure => new CommandeDeleguee(obj => {
            GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans));
        });

        public ICommand CmdPatients => new CommandeDeleguee(obj => {
            GestionnaireEcrans.Changer(new ViewListeEmployesAdmin(GestionnaireEcrans));
        });

        public ICommand CmdEquipements => new CommandeDeleguee(obj =>
        {
            GestionnaireEcrans.Changer(new ViewGestionEquipements(GestionnaireEcrans));
        });

        public ICommand CmdTraitements => new CommandeDeleguee(obj => {
            GestionnaireEcrans.Changer(new ViewAdminModifierTraitements(GestionnaireEcrans));
        });

        public ICommand CmdUsagers => new CommandeDeleguee(obj => {
            GestionnaireEcrans.Changer(new ViewGestionUsagers(GestionnaireEcrans));
        });

        public ViewModelHubAdmin(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
