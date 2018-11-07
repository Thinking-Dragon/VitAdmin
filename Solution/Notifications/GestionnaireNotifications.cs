using MaterialDesignThemes.Wpf;
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
        private SnackbarMessageQueue SnackbarMessageQueue { get; set; }

        public event NotificationsEventHandler ANotifier;
        
        #endregion

        #region Methodes
        
        private void BackgroundWorker_EventHandler(object sender, NotificationsEventArgs args)
        {
            for (int i = 0; i < args.Notifications.Count; i++)
            {
                if(!args.Notifications[i].EstLu)
                {
                    bool bEstPresente = false;
                        for (int j = 0; j < NotificationsDerniereActualisation.Count; j++)
                            if (args.Notifications[i].Message == NotificationsDerniereActualisation[j].Message &&
                                //args.Notifications[i].LienVersFenetre == NotificationsDerniereActualisation[j].LienVersFenetre &&
                                args.Notifications[i].TempsReception == NotificationsDerniereActualisation[j].TempsReception)
                                bEstPresente = true;
                    if (!bEstPresente)
                        SnackbarMessageQueue.Enqueue(args.Notifications[i].Message, "Voir", notification =>
                            {
                                notification.EstLu = true;
                                DataModelNotification.Set("estLu", notification, "true");
                                notification.Voir();
                            },
                            args.Notifications[i]
                        );
                }
            }
            NotificationsDerniereActualisation = args.Notifications;
            ANotifier?.Invoke(this, args);
        }
        
        public void PostNotification(string message, LienNotificationEcran lien, Employe employe)
            => DataModelNotification.PostNotification(message, lien, employe);

        public List<Notification> GetNotifications() => NotificationsDerniereActualisation;

        public SnackbarMessageQueue GetMessageQueue() => SnackbarMessageQueue;
        
        #endregion

        #region Constructeur

        public GestionnaireNotifications()
        {
            SnackbarMessageQueue = new SnackbarMessageQueue();
            BackgroundWorker = new BackgroundWorkerNotifications(DELAI);
            BackgroundWorker.EventHandler += BackgroundWorker_EventHandler;
        }

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

        public static void DetruireInstance()
            => _instance = null;
    
        #endregion
    }
}
