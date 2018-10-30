using MaterialDesignThemes.Wpf;
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

namespace VitAdmin.ControlModel
{
    /// <summary>
    /// View model du contrôle de recherche d'un équipement
    /// </summary>
    public class ControlModelRechercheEquipement : ObjetObservable
    {
        private Action<Equipement> CallBack { get; set; }
        /// <summary>
        /// Liste des résultats possibles
        /// </summary>
        public ObservableCollection<Equipement> ResultatRecherche { get; set; }

        private Equipement _equipement;
        /// <summary>
        /// L'équipement sélectionné par la recherche
        /// </summary>
        public Equipement Equipement
        {
            get => _equipement;
            set { _equipement = value; RaisePropertyChangedEvent("Equipement"); }
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

        public ICommand CmdChoisir => new CommandeDeleguee(param =>
        {
            if (Equipement == null)
                MessageErreur = "Veuillez sélectionner un équipement existant";
            else
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                CallBack(Equipement);
            }
        });

        public ControlModelRechercheEquipement(Action<Equipement> callback)
        {
            CallBack = callback;
            ResultatRecherche = new ObservableCollection<Equipement>(DataModelEquipement.GetEquipements());
        }
    }
}
