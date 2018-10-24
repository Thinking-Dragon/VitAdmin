using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Control;
namespace VitAdmin.ViewModel
{
    public class ViewModelProfessionnelDossierPatientCreerHospitalisation : ObjetObservable
    {
        public List<UserControl> LstUserControl { get; set; }
        public Hospitalisation Hospitalisation { get; set; }
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

        public ViewModelProfessionnelDossierPatientCreerHospitalisation()
        {
            LstUserControl.Add(new ControlTextBoxHospitalisation(Hospitalisation.Contexte, "Contexte"));
            
        }


        public ICommand CmdSuivant
        {
            get
            {
                return new CommandeDeleguee(newPatient =>
                {
                    //DataModelCitoyen.PostCitoyen(Citoyen);
                    //ViewProfessionnelDossierPatientInfosModif winModif = (ViewProfessionnelDossierPatientInfosModif)viewModif;
                    //this.GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));
                });
            }
        }
    }
}
