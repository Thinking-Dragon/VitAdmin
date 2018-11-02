using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Data;

namespace VitAdmin.ControlModel
{
    public class ControlModelAjouterPatientLit
    {
        public Citoyen Citoyen { get; set; }
        public Hospitalisation Hospitalisation { get; set; }
        public ObservableCollection<Lit> Lits { get; set; }
        public Action CallRequeteLit { get; set; }
     

        public ControlModelAjouterPatientLit(Citoyen citoyen, Hospitalisation hospitalisation, List<Lit> lits, Action callRequetLit)
        {
            Citoyen = citoyen;
            Hospitalisation = hospitalisation;
            Lits = new ObservableCollection<Lit>(lits);
            CallRequeteLit = callRequetLit = () => { DataModelLit.GetLitsDepartement(hospitalisation.LstTraitements[0].DepartementAssocie); };
        }
    }
}
