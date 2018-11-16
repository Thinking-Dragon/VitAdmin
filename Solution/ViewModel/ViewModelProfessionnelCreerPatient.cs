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
        public ControlDossierPatientInfos ControlDossierPatientInfos { get; set; }

        public ViewModelProfessionnelCreerPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen, ControlDossierPatientInfos controlDossierPatientInfos)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;
            ControlDossierPatientInfos = controlDossierPatientInfos;
        }

        public ICommand CmdBtnCreer
        {
            get
            {
                return new CommandeDeleguee( action =>
                {
                    if ((ControlDossierPatientInfos.DataContext as ControlModelDossierPatientInfos).Citoyen.ValiderInfos())
                    {
                        DataModelCitoyen.PostCitoyen(Citoyen);

                        this.GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager));

                    }
                    else
                    {
                        (ControlDossierPatientInfos.DataContext as ControlModelDossierPatientInfos).MessageErreurInfosPatient.ViderMessages();
                        (ControlDossierPatientInfos.DataContext as ControlModelDossierPatientInfos).MessageErreurInfosPatient.ActiverMessageErreur(Citoyen);

                    }


                });
            }
        }
    }
}
