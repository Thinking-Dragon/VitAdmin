using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    class ControlModelListeEmployes
    {
        public ObservableCollection<Employe> LstEmployes { get; set; }
        private GestionnaireEcrans gestionnaireEcran { get; set; }

        public ControlModelListeEmployes(GestionnaireEcrans gestionnaire)
        {
            LstEmployes = new ObservableCollection<Employe>(DataModelEmploye.GetEmployes());
            gestionnaireEcran = gestionnaire;
        }


        public ICommand CommandGestionHoraire
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        //gestionnaireEcran.Changer()
                    }
                );
            }
        }
    }
}
