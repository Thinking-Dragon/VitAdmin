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

        private ObservableCollection<Usager> _infirmieresChef;

        public ObservableCollection<Usager> InfirmieresChef
        {
            get => _infirmieresChef;
            set { _infirmieresChef = value; RaisePropertyChangedEvent("InfirmieresChef"); }
        }

        private Usager _personnelMedicalEnChef;
        public Usager PersonnelMedicalEnChef
        {
            get => _personnelMedicalEnChef;
            set { _personnelMedicalEnChef = value; RaisePropertyChangedEvent("PersonnelMedicalEnChef"); }
        }

        private string AbrevInitiale { get; set; }

        public ICommand CmdValider => new CommandeDeleguee(param =>
        {
            DataModelDepartement.PutDepartement(Departement);
            GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans));
        });

        public ICommand CmdCreerLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(chambre =>
            {
                Departement.Chambres.Add(chambre);
            }), "dialogGeneral");
        });

        public ICommand CmdModifierLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(
                chambre =>
                {
                    ChambreSelectionnee.Numero = chambre.Numero;
                    ChambreSelectionnee.Lits = chambre.Lits;
                    ChambreSelectionnee.Equipements = chambre.Equipements;
                    Departement.Chambres = new ObservableCollection<Chambre>(Departement.Chambres);
                }, ChambreSelectionnee
            ), "dialogGeneral");
        });

        public ICommand CmdSupprimerLocal => new CommandeDeleguee(param => Departement.Chambres.Remove(ChambreSelectionnee));

        public ControlModelModifierDepartement(GestionnaireEcrans gestionnaireEcrans, Departement departement)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departement = departement;
            AbrevInitiale = departement.Abreviation;

            InfirmieresChef = new ObservableCollection<Usager>(DataModelUsager.GetInfirmieresChef());

            if (departement.PersonnelMedicalEnChef != null)
                foreach (var inf in InfirmieresChef)
                    if (inf.NomComplet == departement.PersonnelMedicalEnChef.NomComplet)
                        PersonnelMedicalEnChef = inf;
        }
    }
}
