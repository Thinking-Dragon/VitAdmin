using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelChargementApp : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        
        public ICommand CmdAccueilAdministrateur
        {
            get
            {
                return new CommandeDeleguee(password => GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans)));
            }
        }

        public ICommand CmdAccueilProfessionnel
        {
            get
            {
                return new CommandeDeleguee(password => GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager)));
                }
        }

        public ICommand CmdEcranHospitalisation
        {
            get
            {
                return new CommandeDeleguee(password => GestionnaireEcrans.Changer(new ViewPatientHospitalisation(GestionnaireEcrans, new Citoyen("tous059615"), new Hospitalisation(new DateTime(2018, 09, 28, 16, 16, 15)))));
            }
        }

        public ViewModelChargementApp(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
