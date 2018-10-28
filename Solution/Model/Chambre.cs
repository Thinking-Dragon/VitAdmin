using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Chambre
    {
        public ObservableCollection<Equipement> Equipements { get; set; }
        public ObservableCollection<Lit> Lits { get; set; }
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
