using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Notifications;

namespace VitAdmin.ViewModel
{
    public class ViewModelSuperEcran
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        public static GestionnaireEcrans GestionnaireSousEcrans { get; private set; }

        public SnackbarMessageQueue MessageQueue { get; set; }

        public ViewModelSuperEcran(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;
            MessageQueue = GestionnaireNotifications.Instance.GetMessageQueue();
        }
    }
}
