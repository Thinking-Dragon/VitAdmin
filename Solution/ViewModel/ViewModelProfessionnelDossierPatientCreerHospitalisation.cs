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
        public GestionnaireEcrans GestionnaireEcrans { get; set; }
        public Citoyen Citoyen { get; set; }
        public List<UserControl> LstUserControl { get; set; }
        public Hospitalisation Hospitalisation { get; set; }
        public int TotalEtape { get; set; }
        public int NumEtape { get; set; }

        /*private UserControl contenu;
        public UserControl Contenu
        {
            get { return contenu; }
            set
            {
                contenu = value;
                RaisePropertyChangedEvent("Contenu");
            }
        }*/

        public ViewModelProfessionnelDossierPatientCreerHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            LstUserControl = new List<UserControl>();
            Hospitalisation = new Hospitalisation();
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;

            ControlAjouterPatientLit UCPatientLits = new ControlAjouterPatientLit(GestionnaireEcrans, Citoyen, Hospitalisation);

            LstUserControl.Add(new ControlTextBoxHospitalisation("Contexte", Hospitalisation));
            LstUserControl.Add(new ControlSymptome(Hospitalisation));
            LstUserControl.Add(new ControlTraitementCreationHospitalisation(Hospitalisation.LstTraitements = new List<Traitement>(), UCPatientLits.CallRequeteLits));
            LstUserControl.Add(UCPatientLits);

            TotalEtape = LstUserControl.Count();
            NumEtape = 1;
            
        }


     
    }
}
