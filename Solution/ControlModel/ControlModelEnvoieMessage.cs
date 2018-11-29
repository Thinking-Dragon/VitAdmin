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
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
    public class ControlModelEnvoieMessage : ObjetObservable
    {
        private ObservableCollection<Employe> _employes { get; set; }
        public ObservableCollection<Employe> Employes
        {
            get => _employes;
            set { _employes = value; RaisePropertyChangedEvent(nameof(Employes)); }
        }

        private string _titre = string.Empty;
        public string Titre
        {
            get => _titre;
            set { _titre = value; RaisePropertyChangedEvent(nameof(Titre)); }
        }

        private Employe _employe { get; set; }
        public Employe Employe
        {
            get => _employe;
            set { _employe = value; RaisePropertyChangedEvent(nameof(Employe)); }
        }

        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set { _message = value; RaisePropertyChangedEvent(nameof(Message)); }
        }

        public ICommand CmdEnvoyer => new CommandeDeleguee(param =>
        {
            Notifications.GestionnaireNotifications.Instance.PostNotification(
                string.Format("Nouveau message de {0} «{1}»", UsagerConnecte.Usager.NomComplet, Titre),
                new LienNotificationEcran
                {
                    TypeEcran = typeof(ViewMessageNotification),
                    Parametres = new Dictionary<string, object>
                    {
                        { "Sender", UsagerConnecte.Usager.idEmploye.ToString() },
                        { "Titre", Titre },
                        { "Message", Message }
                    }
                },
                DataModelEmploye.GetEmploye(Employe.idEmploye)
            );
            DialogHost.CloseDialogCommand.Execute(null, null);
        });

        public ControlModelEnvoieMessage()
            => Employes = new ObservableCollection<Employe>(DataModelEmploye.GetEmployes());
    }
}
