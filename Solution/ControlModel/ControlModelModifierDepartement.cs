using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
    public class ControlModelModifierDepartement : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public Departement Departement { get; set; }

        public ObservableCollection<string> InfirmieresChef { get; set; }

        private string AbrevInitiale { get; set; }

        public ICommand CmdValider
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    DataModelDepartement.PutDepartement(AbrevInitiale, Departement);
                    GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans));
                });
            }
        }

        public ControlModelModifierDepartement(GestionnaireEcrans gestionnaireEcrans, Departement departement)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departement = departement;
            AbrevInitiale = departement.Abreviation;

            InfirmieresChef = new ObservableCollection<string>();

            List<Usager> infirmieresChef = DataModelUsager.GetInfirmieresChef();
            for (int i = 0; i < infirmieresChef.Count; i++)
                InfirmieresChef.Add(infirmieresChef[i].NomComplet);
        }
    }
}
