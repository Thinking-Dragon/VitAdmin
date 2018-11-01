using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.Model
{    // TODO: Max
    public class Departement : ObjetObservable
    {
        public int _identifiant { get; set; }
        public String Nom { get; set; }
        public String Abreviation { get; set; }
        private Employe _personnelMedicalEnChef;
        public Employe PersonnelMedicalEnChef
        {
            get => _personnelMedicalEnChef;
            set { _personnelMedicalEnChef = value; RaisePropertyChangedEvent("PersonnelMedicalEnChef"); }
        }
        private ObservableCollection<Chambre> _chambres;
        public ObservableCollection<Chambre> Chambres
        {
            get => _chambres;
            set { _chambres = value; RaisePropertyChangedEvent("Chambres"); }
        }

    }
}
