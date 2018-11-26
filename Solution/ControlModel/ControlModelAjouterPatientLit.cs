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
        public Lit Lit { get; set; } // Nouveau lit à attribuer au citoyen
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
                    // Pour l'instant, seulement un traitement peut être assigné au patient et sera actif par défaut.
                    DataModelHospitalisation.PostHospitalisation(Citoyen, Hospitalisation, Hospitalisation.LstTraitements[0], Lit.Chambre, Lit);
                    GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, Citoyen));

                });
            }
        }


        public ControlModelAjouterPatientLit(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen, Lit lit, Hospitalisation hospitalisation, List<Lit> lits)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Citoyen = citoyen;
            Hospitalisation = hospitalisation;
            Lit = lit;
            CallRequeteLit = () => {Lits = new ObservableCollection<Lit>(DataModelLit.GetLitsDepartement(hospitalisation.LstTraitements[0].DepartementAssocie)); };
        }
    }
}
