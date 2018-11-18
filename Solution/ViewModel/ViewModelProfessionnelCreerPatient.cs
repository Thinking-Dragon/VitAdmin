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
using VitAdmin.ControlModel;
using VitAdmin.Control;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelCreerPatient
    {
        public GestionnaireEcrans GestionnaireEcrans { get; set; }
        public Citoyen Citoyen { get; set; }
        public ControlModelDossierPatientInfos ControlModelDossierPatientInfos { get; set; }

        public ViewModelProfessionnelCreerPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;
        }

        public ICommand CmdBtnCreer
        {
            get
            {
                return new CommandeDeleguee( action =>
                {
                    if (ControlModelDossierPatientInfos.Citoyen.ValiderInfos(ControlModelDossierPatientInfos.LstCitoyen))
                    {
                        DataModelCitoyen.PostCitoyen(Citoyen);

                        this.GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));

                    }
                    else
                    {
                        ControlModelDossierPatientInfos.MessageErreurInfosPatient.ViderMessages();
                        ControlModelDossierPatientInfos.MessageErreurInfosPatient.ActiverMessageErreur(Citoyen, ControlModelDossierPatientInfos.LstCitoyen);

                    }


                });
            }
        }
    }
}
