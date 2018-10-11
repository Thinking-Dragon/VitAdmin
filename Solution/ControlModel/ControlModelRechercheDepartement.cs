using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelRechercheDepartement : ObjetObservable
    {
        public ObservableCollection<Departement> Departements { get; set; }

        private Departement _departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return _departementSelectionne; }
            set
            {
                _departementSelectionne = value;
                RaisePropertyChangedEvent("DepartementSelectionne");
            }
        }

        public ControlModelRechercheDepartement()
            => Departements = new ObservableCollection<Departement>(DataModelDepartement.GetDepartements());
    }
}
