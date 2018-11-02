using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Data;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelAjouterPatientLit : ObjetObservable
    {
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
     

        public ControlModelAjouterPatientLit(Citoyen citoyen, Hospitalisation hospitalisation, List<Lit> lits)
        {
            Citoyen = citoyen;
            Hospitalisation = hospitalisation;
            CallRequeteLit = () => {Lits = new ObservableCollection<Lit>(DataModelLit.GetLitsDepartement(hospitalisation.LstTraitements[0].DepartementAssocie)); };
        }
    }
}
