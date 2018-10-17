using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelCreerPatient
    {
        public GestionnaireEcrans GestionnaireEcrans { get; set; }
        public Citoyen Citoyen { get; set; }

        public ViewModelProfessionnelCreerPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;
        }

        public ICommand CmdBtnCreer
        {
            get
            {
                return new CommandeDeleguee( newPatient =>
                {
                    DataModelCitoyen.PostCitoyen(Citoyen);

                    //ViewProfessionnelDossierPatientInfosModif winModif = (ViewProfessionnelDossierPatientInfosModif)viewModif;

                    this.GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));


                });
            }
        }
    }
}
