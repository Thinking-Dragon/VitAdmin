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

namespace VitAdmin.ViewModel
{
    public class ViewModelGestionEquipements : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private ObservableCollection<Equipement> _equipements;
        public ObservableCollection<Equipement> Equipements
        {
            get => _equipements;
            set { _equipements = value; RaisePropertyChangedEvent("Equipements"); }
        }

        private Equipement _equipementSelectionne;
        public Equipement EquipementSelectionne
        {
            get => _equipementSelectionne;
            set { _equipementSelectionne = value; RaisePropertyChangedEvent("EquipementSelectionne"); }
        }

        public ICommand CmdAjouterEquipement => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionEquipement(new CommandeDeleguee(equipement =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);

                Equipement nouvelEquipement = equipement as Equipement;
                    // Éviter les duplicatats
                if (!new List<Equipement>(Equipements).Exists(e => e.Nom == nouvelEquipement.Nom))
                {
                    Equipements.Add(nouvelEquipement);
                    CmdEnregistrer.Execute(null);
                }
                else
                    GestionnaireEcrans.AfficherMessage("Un équipement avec ce nom existe déjà.");
            })), "dialogGeneral");
        });

        public ICommand CmdModifierEquipement => new CommandeDeleguee(param =>
        {
            DialogHost.Show(new ControlEditionEquipement(new CommandeDeleguee(equipement =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);

                Equipement equipementModifie = equipement as Equipement;
                // Éviter les duplicatats
                if (!new List<Equipement>(Equipements).Exists(e => e._identifiant != EquipementSelectionne._identifiant &&
                                                                            e.Nom == equipementModifie.Nom))
                {
                    EquipementSelectionne.Nom = equipementModifie.Nom;
                    EquipementSelectionne.Description = equipementModifie.Description;
                    Equipements = new ObservableCollection<Equipement>(Equipements);
                    CmdEnregistrer.Execute(null);
                }
                else
                    GestionnaireEcrans.AfficherMessage("Un équipement avec ce nom existe déjà.");
            }), EquipementSelectionne), "dialogGeneral");
        });

        public ICommand CmdSupprimerEquipement => new CommandeDeleguee(param =>
        {
            if(EquipementSelectionne != null)
            {
                Equipements.Remove(EquipementSelectionne);
                CmdEnregistrer.Execute(null);
            }
        });

        public ICommand CmdEnregistrer => new CommandeDeleguee(param =>
        {
            DataModelEquipement.PutEquipements(Equipements.ToList());
//            GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans));
        });

        public ViewModelGestionEquipements(GestionnaireEcrans gestionnaireEcrans, List<Equipement> equipements)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            Equipements = new ObservableCollection<Equipement>(equipements);
        }
    }
}
