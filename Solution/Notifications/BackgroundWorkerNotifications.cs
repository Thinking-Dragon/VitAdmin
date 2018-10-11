using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.Parameter;

namespace VitAdmin.Notifications
{
    public class BackgroundWorkerNotifications
    {
        private DispatcherTimer Timer { get; set; }

        public event NotificationsEventHandler EventHandler;

        private void ObtenirNotifications()
            => EventHandler?.Invoke(this, new NotificationsEventArgs(DataModelNotification.GetNotifications(UsagerConnecte.Usager)));

        public BackgroundWorkerNotifications(uint interval)
        {
            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(interval) };
            Timer.Tick += (object sender, EventArgs args) => ObtenirNotifications();
            Timer.Start();
            ObtenirNotifications();
        }
    }
}
