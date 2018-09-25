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
    public class ViewModelHubAdmin : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ICommand CmdPersonnel
        {
            get
            {
                return new CommandeDeleguee(obj => {
                    // GestionnaireEcrans.Changer(new View(...));
                    GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient());
                });
            }
        }

        public ICommand CmdInfrastructure
        {
            get
            {
                return new CommandeDeleguee(obj => {
                    // GestionnaireEcrans.Changer(new View(...));
                });
            }
        }

        public ICommand CmdPatients
        {
            get
            {
                return new CommandeDeleguee(obj => {
                    // GestionnaireEcrans.Changer(new View(...));
                });
            }
        }

        public ICommand CmdTraitments
        {
            get
            {
                return new CommandeDeleguee(obj => {
                    // GestionnaireEcrans.Changer(new View(...));
                });
            }
        }

        public ViewModelHubAdmin(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
