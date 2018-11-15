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

namespace VitAdmin.ViewModel
{
    class ViewModelProfessionnelDossierPatientInfosModif : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        Citoyen Patient { get; set; }
        string AssMaladieAncien { get; set; }

        public ViewModelProfessionnelDossierPatientInfosModif(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Patient = patient;
            AssMaladieAncien = patient.AssMaladie;
        }

        public ICommand CmdBtnModif
        {
            get
            {
                return new CommandeDeleguee(action =>
                {
                    DataModelCitoyen.PutCitoyen(Patient, AssMaladieAncien);

                    //ViewProfessionnelDossierPatientInfosModif winModif = (ViewProfessionnelDossierPatientInfosModif)viewModif;
                    
                    this.GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, Patient));

                    
                });
            }
        }

    }
}
