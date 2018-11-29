using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ViewModel
{
    public class ViewModelMessageNotification : ObjetObservable
    {
        private Employe _employe { get; set; }
        public Employe Employe
        {
            get => _employe;
            set { _employe = value; RaisePropertyChangedEvent(nameof(Employe)); }
        }

        private string _titre;
        public string Titre
        {
            get => _titre;
            set { _titre = value; RaisePropertyChangedEvent(nameof(Titre)); }
        }

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; RaisePropertyChangedEvent(nameof(Message)); }
        }

        private string _reponse = string.Empty;
        public string Reponse
        {
            get => _reponse;
            set { _reponse = value; RaisePropertyChangedEvent(nameof(Reponse)); }
        }

        public ICommand CmdRepondre => new CommandeDeleguee(param =>
        {
            Notifications.GestionnaireNotifications.Instance.PostNotification(
                string.Format("Réponse de {0} «{1}»", UsagerConnecte.Usager.NomComplet, Titre),
                new LienNotificationEcran
                {
                    TypeEcran = typeof(ViewMessageNotification),
                    Parametres = new Dictionary<string, object>
                    {
                        { "Sender", UsagerConnecte.Usager.idEmploye.ToString() },
                        { "Titre", Titre },
                        { "Message", Reponse }
                    }
                },
                Employe
            );
        });

        public ViewModelMessageNotification(int senderID, string titre, string message)
        {
            Employe = DataModelEmploye.GetEmploye(senderID);
            Titre = string.Format("{0}: {1}", Employe.NomComplet, titre);
            Message = message;
        }
    }
}
