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

        private ObservableCollection<Departement> _departements { get; set; }
        public ObservableCollection<Departement> Departements
        {
            get => _departements;
            set { _departements = value; RaisePropertyChangedEvent(nameof(Departements)); }
        }

        private Departement departementSelectionne;
        public Departement DepartementSelectionne
        {
            get { return departementSelectionne; }
            set
            {
                departementSelectionne = value;

                if (DepartementSelectionne != null &&
                    DepartementSelectionne.PersonnelMedicalEnChef != null)
                {
                    for (int i = 0; i < InfirmieresChef.Count; i++)
                        if (InfirmieresChef[i].NumEmploye == DepartementSelectionne.PersonnelMedicalEnChef.NumEmploye)
                            PersonnelMedicalEnChef = InfirmieresChef[i];
                }
                else
                {
                    for (int i = 0; i < InfirmieresChef.Count; i++)
                        if (InfirmieresChef[i].Nom == "S/O")
                            PersonnelMedicalEnChef = InfirmieresChef[i];
                }

                RaisePropertyChangedEvent(nameof(DepartementSelectionne));
                RaisePropertyChangedEvent(nameof(IsDepartementSelected));
                if (DepartementSelectionne != null)
                {
                    _dernierNomDepartement = DepartementSelectionne.Nom;
                    NomDepartement = DepartementSelectionne.Nom;
                    AbreviationDepartement = DepartementSelectionne.Abreviation;
                    PersonnelMedicalEnChef = DepartementSelectionne.PersonnelMedicalEnChef;
                }
                else
                    _dernierNomDepartement = string.Empty;
            }
        }

        private string _dernierNomDepartement = string.Empty;
        public string NomDepartement
        {
            get => DepartementSelectionne.Nom;
            set
            {
                DepartementSelectionne.Nom = value;
                RaisePropertyChangedEvent(nameof(NomDepartement));
                RaisePropertyChangedEvent(nameof(DepartementSelectionne));
                DataModelDepartement.PutDepartement(DepartementSelectionne);

                if(value != _dernierNomDepartement)
                    Departements = new ObservableCollection<Departement>(Departements);
                _dernierNomDepartement = value;
            }
        }

        public string AbreviationDepartement
        {
            get => DepartementSelectionne.Abreviation;
            set
            {
                DepartementSelectionne.Abreviation = value;
                RaisePropertyChangedEvent(nameof(AbreviationDepartement));
                RaisePropertyChangedEvent(nameof(DepartementSelectionne));
                DataModelDepartement.PutDepartement(DepartementSelectionne);
            }
        }

        private ObservableCollection<Employe> _infirmieresChef;

        public ObservableCollection<Employe> InfirmieresChef
        {
            get => _infirmieresChef;
            set { _infirmieresChef = value; RaisePropertyChangedEvent("InfirmieresChef"); }
        }

        private Employe _personnelMedicalEnChef;
        public Employe PersonnelMedicalEnChef
        {
            get => _personnelMedicalEnChef;
            set
            {
                if(value == null)
                {
                    for (int i = 0; i < InfirmieresChef.Count; i++)
                        if (InfirmieresChef[i].Nom == "S/O")
                            _personnelMedicalEnChef = InfirmieresChef[i];
                }
                else _personnelMedicalEnChef = value;
                if (value != null && value.Nom != "S/O") DepartementSelectionne.PersonnelMedicalEnChef = value;
                else if (DepartementSelectionne != null) DepartementSelectionne.PersonnelMedicalEnChef = null;
                RaisePropertyChangedEvent(nameof(PersonnelMedicalEnChef));
                RaisePropertyChangedEvent(nameof(DepartementSelectionne));
                if(DepartementSelectionne != null)
                    DataModelDepartement.PutDepartement(DepartementSelectionne);
            }
        }

        public bool IsDepartementSelectionneNull
        {
            get { return DepartementSelectionne == null; }
        }

        public bool IsDepartementSelected => DepartementSelectionne != null;

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

        public ICommand CmdCreerLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(GestionnaireEcrans, chambre =>
            {
                DepartementSelectionne.Chambres.Add(chambre);
                DataModelDepartement.PutDepartement(DepartementSelectionne);
            }), "dialogGeneral:modal=false");
        });

        public ICommand CmdModifierLocal => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionChambre(
                GestionnaireEcrans, chambre =>
                {
                    ChambreSelectionnee.Numero = chambre.Numero;
                    ChambreSelectionnee.Lits = chambre.Lits;
                    ChambreSelectionnee.Equipements = chambre.Equipements;
                    DepartementSelectionne.Chambres = new ObservableCollection<Chambre>(DepartementSelectionne.Chambres);
                    DataModelDepartement.PutDepartement(DepartementSelectionne);
                }, ChambreSelectionnee
            ), "dialogGeneral:modal=false");
        });

        public ICommand CmdSupprimerLocal => new CommandeDeleguee(param =>
        {
            if (!(ChambreSelectionnee.Lits.ToList().Exists(lit => !lit.EstDisponible)))
            {
                DepartementSelectionne.Chambres.Remove(ChambreSelectionnee);
                DataModelDepartement.PutDepartement(DepartementSelectionne);
            }
            else
                GestionnaireEcrans.AfficherMessage("Cette chambre contient au moins un lit occupé, vous ne pouvez pas la supprimer!");
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

            InfirmieresChef = new ObservableCollection<Employe>(DataModelUsager.GetInfirmieresChef());
            InfirmieresChef.Add(new Usager { Nom = "S/O" });

            if (Departements.Count > 0)
                DepartementSelectionne = Departements[0];
        }
    }
}
