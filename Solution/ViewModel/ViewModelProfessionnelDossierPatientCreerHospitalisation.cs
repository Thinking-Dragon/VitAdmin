using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ViewProfessionnelDossierPatientCreerHospitalisation: ObjetObservable
    {
        public List<UserControl> LstUserControl { get; set; }
        private UserControl contenu;
        public UserControl Contenu
        {
            get { return contenu; }

            set
            {
                contenu = value;
                RaisePropertyChangedEvent("Contenu");
            }
        }

        public ViewProfessionnelDossierPatientCreerHospitalisation()
        {

        }

        public ICommand CmdSuivant
        {
            get
            {
                return new CommandeDeleguee(newPatient =>
                {
                    DataModelCitoyen.PostCitoyen(Citoyen);

                    //ViewProfessionnelDossierPatientInfosModif winModif = (ViewProfessionnelDossierPatientInfosModif)viewModif;

                    this.GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));


                });
            }
        }
    }
}
