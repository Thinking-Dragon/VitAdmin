using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    /// <summary>
    /// ViewModel du contrôle de création et de modification d'une chambre
    /// </summary>
    public class ControlModelEditionChambre : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private Action<Chambre> CallBack { get; set; }
        public Chambre Chambre { get; set; }

        /// <summary>
        /// État du bouton de suppression d'un lit (Activé ou Désactivé; change selon l'état du lit sélectionné)
        /// </summary>
        public bool EstBtnSupprimerLitActive => true; // LitSelectionne == null ? false : LitSelectionne.EtatLit != EtatLit.Occupé;

        /// <summary>
        /// Actualise l'état du bouton de suppression d'un lit
        /// </summary>
        public void ActualiserEtatBtnSupprimerLit() => RaisePropertyChangedEvent("EstBtnSupprimerLitActive");

        private Lit _litSelectionne;
        /// <summary>
        /// Lit sélectionné dans la liste des lits de la chambre
        /// </summary>
        public Lit LitSelectionne
        {
            get => _litSelectionne;
            set { _litSelectionne = value; RaisePropertyChangedEvent("LitSelectionne"); }
        }

        private Equipement _equipementSelectionne;
        /// <summary>
        /// Équipement sélectionné dans la liste des équipements de la chambre
        /// </summary>
        public Equipement EquipementSelectionne
        {
            get => _equipementSelectionne;
            set { _equipementSelectionne = value; RaisePropertyChangedEvent("EquipementSelectionne"); }
        }

        private string _titre = "Nouvelle chambre";
        /// <summary>
        /// Titre de l'écran
        /// </summary>
        public string Titre
        {
            get => _titre;
            set { _titre = value; RaisePropertyChangedEvent("Titre"); }
        }

        private string _messageErreur = string.Empty;
        /// <summary>
        /// Contenu du message qui s'affiche à l'écran s'il y a une erreur
        /// </summary>
        public string MessageErreur
        {
            get => _messageErreur;
            set { _messageErreur = value; RaisePropertyChangedEvent("MessageErreur"); }
        }

        /// <summary>
        /// Crée un nouveau lit {État: libre, Numéro: MAX + 1}
        /// </summary>
        public ICommand CmdCreerLit => new CommandeDeleguee(param =>
        {
            Lit nouveauLit = new Lit { EtatLit = EtatLit.Libre };
            if (Chambre.Lits.Count == 0)
                nouveauLit.Numero = "1"; // S'il n'y a encore aucun lit, on commence à 1
            else
            {
                nouveauLit.Numero = "0";
                foreach (var lit in Chambre.Lits)
                    if (int.Parse(lit.Numero) > int.Parse(nouveauLit.Numero))
                        nouveauLit.Numero = lit.Numero;
                if(nouveauLit.Numero != "0")
                    nouveauLit.Numero = (int.Parse(nouveauLit.Numero) + 1).ToString();
            }

            if (nouveauLit.Numero != "0")
                Chambre.Lits.Add(nouveauLit);
        });

        /// <summary>
        /// Retire le lit sélectionné de la liste des lits de la chambre
        /// </summary>
        public ICommand CmdSupprimerLit => new CommandeDeleguee(param =>
        {
            if (LitSelectionne != null && LitSelectionne.EstDisponible)
                Chambre.Lits.Remove(LitSelectionne);
            else
                GestionnaireEcrans.AfficherMessage("Vous ne pouvez pas supprimer un lit s'il est occupé!", "Okay", "dialogEditionChambre");
        });

        /// <summary>
        /// Ouvre l'écran de recherche d'un équipement et l'ajoute à la liste des équipements de la chambre si la recherche aboutit à un choix
        /// </summary>
        public ICommand CmdAjouterEquipement =>
            new CommandeDeleguee(
                param =>
                    DialogHost.Show(new ControlRechercheEquipement(equipement => Chambre.Equipements.Add(equipement)), "dialogEditionChambre")
            );

        /// <summary>
        /// Retire l'équipement sélectionné de la liste des équipements de la chambre
        /// </summary>
        public ICommand CmdRetirerEquipement => new CommandeDeleguee(param => Chambre.Equipements.Remove(EquipementSelectionne));

        /// <summary>
        /// Commande qui s'exécute lorsque l'on clique sur le bouton « Confirmer ».
        /// Donne la chambre au créateur de l'instance.
        /// </summary>
        public ICommand CmdConfirmer => new CommandeDeleguee(param =>
        {
            if (Chambre.Numero == string.Empty)
                MessageErreur = "Une chambre doit avoir un numéro";
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                CallBack(Chambre);
            }
        });

        /// <summary>
        /// Crée un contexte de données pour une instance de contrôle de modification/création d'une chambre.
        /// </summary>
        /// <param name="chambre">La chambre à modifier (null si on en veut créer une)</param>
        /// <param name="callback">Fonction de retour qui est appelée lorsque l'utilisateur confirme la modification ou la création de la chambre</param>
        public ControlModelEditionChambre(GestionnaireEcrans gestionnaireEcrans, Chambre chambre, Action<Chambre> callback)
        {
            GestionnaireEcrans = gestionnaireEcrans;

            CallBack = callback;
            if (chambre != null)
            {
                Chambre = new Chambre
                {
                    Numero = chambre.Numero,
                    Lits = new ObservableCollection<Lit>(chambre.Lits),
                    Equipements = new ObservableCollection<Equipement>(chambre.Equipements)
                };
                Titre = "Modification de la chambre : «" + chambre.Numero + '»';
            }
            else
            {
                Chambre = new Chambre
                {
                    Numero = string.Empty,
                    Lits = new ObservableCollection<Lit>(),
                    Equipements = new ObservableCollection<Equipement>()
                };
            }
        }
    }
}
