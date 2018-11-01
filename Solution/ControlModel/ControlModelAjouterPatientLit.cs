using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.ControlModel
{
    public class ControlModelAjouterPatientLit
    {
        public Citoyen Citoyen { get; set; }
        public Hospitalisation Hospitalisation { get; set; }
        public ObservableCollection<Lit> Lits { get; set; }

        public ControlModelAjouterPatientLit(Citoyen citoyen, Hospitalisation hospitalisation, List<Lit> lits)
        {
            Citoyen = citoyen;
            Hospitalisation = hospitalisation;
            Lits = new ObservableCollection<Lit>(lits);
        }
    }
}
