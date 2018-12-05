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
    /// <summary>
    /// View model du control DépartementsAvecDétails
    /// </summary>
    public class ControlModelDepartementsAvecDetails : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ObservableCollection<Departement> Departements { get; set; }

        private Departement departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return departementSelectionne; }
            set
            {
                departementSelectionne = value;
                RaisePropertyChangedEvent("DepartementSelectionne");
            }
        }

        public bool IsDepartementSelectionneNull
        {
            get { return DepartementSelectionne == null; }
        }

        private Chambre _chambreSelectionnee;
        public Chambre ChambreSelectionnee
        {
            get => _chambreSelectionnee;
            set { _chambreSelectionnee = value; RaisePropertyChangedEvent("ChambreSelectionnee"); }
        }

        public ICommand CmdModifierDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    GestionnaireEcrans.Changer(new ViewModifierDepartement(GestionnaireEcrans, DepartementSelectionne));
                });
            }
        }

        public ICommand CmdCreerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    GestionnaireEcrans.Changer(new ViewModifierDepartement(GestionnaireEcrans));
                });
            }
        }

        public ICommand CmdSupprimerDepartement
        {
            get
            {
                return new CommandeDeleguee(param =>
                {
                    if (DepartementSelectionne != null)
                    {
                        if (DepartementSelectionne.Chambres.Count > 0)
                        {
                            if (DepartementSelectionne.Chambres.ToList().Exists(chambre => chambre.Lits.ToList().Exists(lit => !lit.EstDisponible)))
                                GestionnaireEcrans.AfficherMessage("Ce département contient au moins un local occupé, vous ne pouvez pas le supprimer");
                            else
                            {
                                GestionnaireEcrans.DemanderOuiNon("Ce département contient un ou plusieurs locaux, êtes-vous sûr de vouloir le supprimer ?",
                                    usagerVeutContinuer =>
                                    {
                                        if (usagerVeutContinuer)
                                            SupprimerDepartementSelectionne();
                                    }
                                );
                            }
                        }
                        else SupprimerDepartementSelectionne();
                    }
                    else GestionnaireEcrans.AfficherMessage("Veuillez choisir un département!");
                });
            }
        }

        public ICommand CmdModifierLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(
                GestionnaireEcrans, chambre =>
                {
                    ChambreSelectionnee.Numero = chambre.Numero;
                    ChambreSelectionnee.Lits = chambre.Lits;
                    ChambreSelectionnee.Equipements = chambre.Equipements;
                    DepartementSelectionne.Chambres = new ObservableCollection<Chambre>(DepartementSelectionne.Chambres);
                }, ChambreSelectionnee
            ), "dialogGeneral:modal=false");
        });

        private void SupprimerDepartementSelectionne()
        {
            Departement departementTemporaire = DepartementSelectionne;
            Departements.Remove(DepartementSelectionne);
            DataModelDepartement.DeleteDepartement(departementTemporaire);
        }


        public ControlModelDepartementsAvecDetails(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Departements = new ObservableCollection<Departement>(DataModelDepartement.GetDepartements());
        }
    }
}
