using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelListeChoixDepartementsCRD : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ObservableCollection<Departement> Departements { get; set; }

        private Departement departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return departementSelectionne; }
            set { departementSelectionne = value; RaisePropertyChangedEvent("DepartementSelectionne"); }
        }

        public ICommand CmdCreer
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    System.Windows.MessageBox.Show("Hey!");
                });
            }
        }

        public ICommand CmdSupprimer
        {
            get
            {
                return new CommandeDeleguee(param =>
                {

                });
            }
        }

        public ControlModelListeChoixDepartementsCRD(GestionnaireEcrans gestionnaireEcrans, List<Departement> departements, Departement departementSelectionne)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departements = new ObservableCollection<Departement>(departements);
            DepartementSelectionne = departementSelectionne;
        }
    }
}
