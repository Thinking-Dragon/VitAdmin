using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Notifications
{
    /// <summary>
    /// Conteneur qui est passé en argument à l'évènement de mise à jour des notifications.
    /// </summary>
    public class NotificationsEventArgs
    {
        public List<Notification> Notifications { get; set; }
        public NotificationsEventArgs(List<Notification> notifications)
            => Notifications = notifications;
    }

    /// <summary>
    /// Manutenteur de l'évènement de mise à jour des notifications.
    /// </summary>
    /// <param name="sender">Appelant de l'évènement</param>
    /// <param name="args">Arguments de l'évènement</param>
    public delegate void NotificationsEventHandler(object sender, NotificationsEventArgs args);
}
