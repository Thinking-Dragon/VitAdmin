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
    /// <summary>
    /// Description: va chercher les notifications de l'usager connecté périodiquement.
    /// Auteur: Clément Gaßmann-Prince
    /// </summary>
    public class BackgroundWorkerNotifications
    {
        private DispatcherTimer Timer { get; set; }

        public event NotificationsEventHandler EventHandler;
        
        private void ObtenirNotifications()
            => EventHandler?.Invoke(this, new NotificationsEventArgs(DataModelNotification.GetNotifications(UsagerConnecte.Usager)));

        /// <summary>
        /// Construit un backgroundworker de notifications.
        /// </summary>
        /// <param name="interval">fréquence de mise à jour</param>
        public BackgroundWorkerNotifications(uint interval)
        {
            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(interval) };
            Timer.Tick += (object sender, EventArgs args) => {
                if (!UsagerConnecte.EstConnecte)
                    Timer.Stop();
                else
                    ObtenirNotifications();
            };
            Timer.Start();
            DispatcherTimer dtFirst = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
            dtFirst.Tick += (object sender, EventArgs args) =>
            {
                ObtenirNotifications();
                dtFirst.Stop();
            };
            dtFirst.Start();
        }
    }
}
