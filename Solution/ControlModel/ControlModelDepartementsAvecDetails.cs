using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
    public class ControlModelDepartementsAvecDetails : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ObservableCollection<Departement> Departements { get; set; }

        private Departement departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return departementSelectionne; }
            set
            {
                departementSelectionne = value;
                RaisePropertyChangedEvent("DepartementSelectionne");
            }
        }

        public bool IsDepartementSelectionneNull
        {
            get { return DepartementSelectionne == null; }
        }

        public ICommand CmdModifierDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    GestionnaireEcrans.Changer(new ViewModifierDepartement(GestionnaireEcrans, DepartementSelectionne));
                });
            }
        }

        public ICommand CmdCreerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    GestionnaireEcrans.Changer(new ViewModifierDepartement(GestionnaireEcrans));
                });
            }
        }

        public ICommand CmdSupprimerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {

                });
            }
        }


        public ControlModelDepartementsAvecDetails(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departements = new ObservableCollection<Departement>(DataModelDepartement.GetDepartements());
        }
    }
}
