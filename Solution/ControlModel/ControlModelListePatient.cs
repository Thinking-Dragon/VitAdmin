using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

// Le control modèle sert à faire le binding entre la BD et le control
namespace VitAdmin.ControlModel
{
    class ControlModelListePatient : ObjetObservable
    {
        public ObservableCollection<Citoyen> Citoyens { get; set; }
        public ObservableCollection<Departement> Departements { get; set; }
        public ObservableCollection<Employe> Employes { get; set; }
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        // constructeur
        public ControlModelListePatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Citoyen> citoyens, ObservableCollection<Departement> departements, ObservableCollection<Employe> employes)
        {
            Citoyens = citoyens;
            Departements = departements;
            Employes = employes;
            GestionnaireEcrans = gestionnaireEcrans;
        }

        public ICommand CmdDoubleClic
        {
            get
            {
                return new CommandeDeleguee(citoyenSelectionne =>
                {
                    if(citoyenSelectionne != null)
                        GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, (Citoyen)citoyenSelectionne));
                    
                });
            }
        }

        public ICommand CmdDepartementSelectionChanged
        {
            get
            {   // departementSelectionne reçoit l'item sélectionné dans la combobox
                return new CommandeDeleguee(departementSelectionne =>
                {
                    List<Employe> lstEmploye = Data.DataModelEmploye.GetLstEmployesDepartement((Departement)departementSelectionne);
                    Citoyens.Clear();
                    lstEmploye.ForEach(employe => Employes.Add(employe));
                    /*List<Citoyen> lstCitoyen = Data.DataModelCitoyen.GetCitoyensLstPatient((Employe)employeSelectionne);
                    Citoyens.Clear();
                    lstCitoyen.ForEach(c => Citoyens.Add(c));*/
                });
            }
        } 

    }
}
