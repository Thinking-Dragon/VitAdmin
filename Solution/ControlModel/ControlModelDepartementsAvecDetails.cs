using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;

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
            set { departementSelectionne = value; RaisePropertyChangedEvent("DepartementSelectionne"); }
        }

        public ICommand CmdModifierDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");
                });
            }
        }

        public ICommand CmdCreerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");

                });
            }
        }

        public ICommand CmdSupprimerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");

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
