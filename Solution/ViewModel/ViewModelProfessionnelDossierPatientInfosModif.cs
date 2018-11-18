using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Data;
using VitAdmin.View;
using VitAdmin.ControlModel;

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelDossierPatientInfosModif : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        Citoyen Patient { get; set; }
        string AssMaladieAncien { get; set; }
        ControlModelDossierPatientInfos ControlModelDossierPatientInfos { get; set; }

        public ViewModelProfessionnelDossierPatientInfosModif(GestionnaireEcrans gestionnaireEcrans, Citoyen patient, ControlModelDossierPatientInfos controlModelDossierPatientInfos)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Patient = patient;
            AssMaladieAncien = patient.AssMaladie;
            ControlModelDossierPatientInfos = controlModelDossierPatientInfos;
        }

        public ICommand CmdBtnModif
        {
            get
            {
                return new CommandeDeleguee(action =>
                {
                    if (ControlModelDossierPatientInfos.Citoyen.ValiderInfos(ControlModelDossierPatientInfos.LstCitoyen))
                    {
                        DataModelCitoyen.PutCitoyen(Patient, AssMaladieAncien);

                        this.GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, Patient));

                    }
                    else
                    {
                        ControlModelDossierPatientInfos.MessageErreurInfosPatient.ViderMessages();
                        ControlModelDossierPatientInfos.MessageErreurInfosPatient.ActiverMessageErreur(Patient, ControlModelDossierPatientInfos.LstCitoyen);

                    }
                    

                    
                });
            }
        }

    }
}
