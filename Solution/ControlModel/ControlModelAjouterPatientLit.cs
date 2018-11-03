using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Data;
using VitAdmin.MVVM;
using VitAdmin.View;
using System.Windows.Input;

namespace VitAdmin.ControlModel
{
    public class ControlModelAjouterPatientLit : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        public Citoyen Citoyen { get; set; }
        public Hospitalisation Hospitalisation { get; set; }
        private ObservableCollection<Lit> lits;
        public ObservableCollection<Lit> Lits
        {
            get
            {
                return lits;
            }

            set
            {
                lits = value;
                RaisePropertyChangedEvent("Lits");
            }
        }
        public Action CallRequeteLit { get; set; }

        public ICommand CmdBtnTerminer
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    // On effectue la création de la nouvelle hospitalisation
                    Hospitalisation.DateDebut = DateTime.Now;
                    DataModelHospitalisation.PostHospitalisation(Citoyen, Hospitalisation, Hospitalisation.LstTraitements[0], Citoyen.Lit.Chambre, Citoyen.Lit);
                    GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, Citoyen));

                });
            }
        }


        public ControlModelAjouterPatientLit(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen, Hospitalisation hospitalisation, List<Lit> lits)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;
            Hospitalisation = hospitalisation;
            CallRequeteLit = () => {Lits = new ObservableCollection<Lit>(DataModelLit.GetLitsDepartement(hospitalisation.LstTraitements[0].DepartementAssocie)); };
        }
    }
}
