using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Notifications
{
    public class NotificationsEventArgs
    {
        public List<Notification> Notifications { get; set; }
        public NotificationsEventArgs(List<Notification> notifications)
            => Notifications = notifications;
    }

    public delegate void NotificationsEventHandler(object sender, NotificationsEventArgs args);
}
