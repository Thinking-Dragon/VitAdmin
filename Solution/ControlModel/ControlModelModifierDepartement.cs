using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
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

        private Chambre _chambreSelectionnee;
        public Chambre ChambreSelectionnee
        {
            get => _chambreSelectionnee;
            set { _chambreSelectionnee = value; RaisePropertyChangedEvent("ChambreSelectionnee"); }
        }

        public ObservableCollection<string> InfirmieresChef { get; set; }

        private string AbrevInitiale { get; set; }

        public ICommand CmdValider => new CommandeDeleguee(param =>
        {
            DataModelDepartement.PutDepartement(AbrevInitiale, Departement);
            GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans));
        });

        public ICommand CmdCreerLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(
                chambre =>
                {

                }
            ), "dialogGeneral");
        });

        public ICommand CmdModifierLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(
                chambre =>
                {
                    
                }, ChambreSelectionnee
            ), "dialogGeneral");
        });

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
