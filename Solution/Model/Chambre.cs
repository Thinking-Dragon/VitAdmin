using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.Model
{    // TODO: Max
    public class Chambre : ObjetObservable
    {
        public int _identifiant { get; set; } = -1;

        private ObservableCollection<Equipement> _equipements = null;
        public ObservableCollection<Equipement> Equipements
        {
            get => _equipements;
            set { _equipements = value; RaisePropertyChangedEvent("Equipements"); }
        }

        private ObservableCollection<Lit> _lits = null;
        public ObservableCollection<Lit> Lits
        {
            get => _lits;
            set { _lits = value; RaisePropertyChangedEvent("Lits"); }
        }

        public Departement UnDepartement { get; set; }
        public String Numero { get; set; }

        public String EquipementsString
        {
            get
            {
                if (Equipements == null || Equipements.Count == 0)
                    return "Aucun équipement";

                StringBuilder stringBuilderResultat = new StringBuilder(Equipements[0].Nom);
                for (int i = 1; i < Equipements.Count; i++)
                    stringBuilderResultat.Append(", ").Append(Equipements[i].Nom);

                return stringBuilderResultat.ToString();
            }
        }

    }
}
