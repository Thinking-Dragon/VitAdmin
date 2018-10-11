using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Notifications
{
    public class GestionnaireNotifications
    {
        #region Constantes

        private const uint DELAI = 3;

        #endregion

        #region Attributs
        
        private BackgroundWorkerNotifications BackgroundWorker { get; set; }
        private List<Notification> NotificationsDerniereActualisation { get; set; } = new List<Notification>();
        
        public event NotificationsEventHandler ANotifier;
        
        #endregion

        #region Methodes
        
        private void BackgroundWorker_EventHandler(object sender, NotificationsEventArgs args)
        {
            NotificationsDerniereActualisation = args.Notifications;
            ANotifier?.Invoke(this, args);
        }
        
        public void PostNotification(string message, string lien, Employe employe)
            => DataModelNotification.PostNotification(message, lien, employe);

        public List<Notification> GetNotifications() => NotificationsDerniereActualisation;
        
        #endregion

        #region Constructeur

       /* public GestionnaireNotifications()
        {
            BackgroundWorker = new BackgroundWorkerNotifications(DELAI);
            BackgroundWorker.EventHandler += BackgroundWorker_EventHandler;
        }*/

        #endregion

        #region Instance du singleton

        private static GestionnaireNotifications _instance = null;
        public static GestionnaireNotifications Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GestionnaireNotifications();
                return _instance;
            }
        }

        #endregion
    }
}
