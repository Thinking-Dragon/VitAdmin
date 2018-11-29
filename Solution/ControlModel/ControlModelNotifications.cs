using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Notifications;

namespace VitAdmin.ControlModel
{
    public class ControlModelNotifications : ObjetObservable
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        private Notification _notificationSelectionnee;
        public Notification NotificationSelectionnee
        {
            get { return _notificationSelectionnee; }
            set
            {
                _notificationSelectionnee = value;
                RaisePropertyChangedEvent("NotificationSelectionnee");
            }
        }

        /// <summary>
        /// Commande qui s'exécute lorsque l'on clique sur une notification dans la liste
        /// </summary>
        public ICommand CmdOuvrirNotification
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        if(NotificationSelectionnee != null)
                        {
                            NotificationSelectionnee.EstLu = true;
                            Data.DataModelNotification.Set("estLu", NotificationSelectionnee, "true");
                            DialogHost.CloseDialogCommand.Execute(null, null);
                            NotificationSelectionnee.Voir(ViewModel.ViewModelSuperEcran.GestionnaireSousEcrans);
                        }
                    }
                );
            }
        }

        private void ActualiserListeNotifications(object sender, NotificationsEventArgs args)
        {
            Notification notificationSelectionneeTemporaire = NotificationSelectionnee;
            Notifications.Clear();
            foreach (Notification notification in args.Notifications)
                Notifications.Add(notification);
            NotificationSelectionnee = notificationSelectionneeTemporaire;
        }

        public ControlModelNotifications()
        {
            Notifications = new ObservableCollection<Notification>(GestionnaireNotifications.Instance.GetNotifications());
            GestionnaireNotifications.Instance.ANotifier += ActualiserListeNotifications;
        }
    }
}
