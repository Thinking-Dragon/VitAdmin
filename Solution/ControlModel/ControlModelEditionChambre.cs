using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    /// <summary>
    /// ViewModel du contrôle de création et de modification d'une chambre
    /// </summary>
    public class ControlModelEditionChambre : ObjetObservable
    {
        private Action<Chambre> CallBack { get; set; }
        public Chambre Chambre { get; set; }

        private string _titre = "Nouvelle chambre";
        /// <summary>
        /// Titre de l'écran
        /// </summary>
        public string Titre
        {
            get => _titre;
            set { _titre = value; RaisePropertyChangedEvent("Titre"); }
        }

        /// <summary>
        /// Commande qui s'exécute lorsque l'on clique sur le bouton « Confirmer ».
        /// Donne la chambre au créateur de l'instance.
        /// </summary>
        public ICommand CmdConfirmer => new CommandeDeleguee(param =>
        {
            CallBack(Chambre);
        });

        public ControlModelEditionChambre(Chambre chambre, Action<Chambre> callback)
        {
            CallBack = callback;
            if (chambre != null)
            {
                Chambre = chambre;
                Titre = "Modification de la chambre : «" + chambre.Numero + '»';
            }
            else Chambre = new Chambre();
        }
    }
}
