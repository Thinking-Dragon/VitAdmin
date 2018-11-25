using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Model
{    // TODO: Max
    public class Traitement
    {
        public int IdTraitment { get; set; }
        public String Nom { get; set; }
        public ObservableCollection<Etape> EtapesTraitement { get; set; }
        public Departement DepartementAssocie { get; set; }
        public bool EstEnCours { get; set; }

        public Traitement()
        {
            EstEnCours = false;
        }
    }
}
