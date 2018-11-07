using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
    public class ControlModelConnexion : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private string _usager = "admin";//string.Empty;
        /// <summary>
        /// Le texte entré dans le champ "Usager"
        /// </summary>
        public string Usager
        {
            get { return _usager; }
            set
            {
                _usager = value;
                RaisePropertyChangedEvent("Usager");
            }
        }

        private string _messageErreur = string.Empty;
        /// <summary>
        /// Contenu du message d'erreur qui s'affiche si une erreur advient lors de la connexion
        /// </summary>
        public string MessageErreur
        {
            get { return _messageErreur; }
            set
            {
                _messageErreur = value;
                RaisePropertyChangedEvent("MessageErreur");
            }
        }

        /// <summary>
        /// Commande qui s'éxécute lorsque l'utilisateur clique sur le bouton "Connexion" ou lorsqu'il appuie sur le bouton "Entrer" et qu'un des champs texte (usager et mot de passe) est "focusé"
        /// </summary>
        public ICommand CmdConnexion
        {
            get
            {
                return new CommandeDeleguee(password =>
                {
                    EtatAvecMessage validation = UsagerConnecte.TenterConnexion(Usager, (password as PasswordBox).Password);
                    if (validation.Etat)
                        GestionnaireEcrans.Changer(new ViewSuperEcran(GestionnaireEcrans, typeof(ViewChargementApp)));
                    else MessageErreur = validation.Message;
                });
            }
        }

        public ControlModelConnexion(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;
    }
}
