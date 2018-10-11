using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Notifications;

namespace VitAdmin.ControlModel
{
    public class ControlModelNotifications : ObjetObservable
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        private void ActualiserListeNotifications(object sender, NotificationsEventArgs args)
        {
            Notifications.Clear();
            foreach (Notification notification in args.Notifications)
                Notifications.Add(notification);
        }

        public ControlModelNotifications()
        {
            Notifications = new ObservableCollection<Notification>(GestionnaireNotifications.Instance.GetNotifications());
            GestionnaireNotifications.Instance.ANotifier += ActualiserListeNotifications;
        }
    }
}
