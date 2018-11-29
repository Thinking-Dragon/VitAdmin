using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.Notifications;
using VitAdmin.View.Tool;

namespace VitAdmin.ControlModel
{
    public class ControlModelBandeauNavigationGeneral : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        private string _textBoutonRetourEcran = string.Empty;
        public string TexteBoutonRetourEcran
        {
            get { return _textBoutonRetourEcran; }
            set
            {
                _textBoutonRetourEcran = value;
                RaisePropertyChangedEvent("TexteBoutonRetourEcran");
            }
        }

        public string _nbNotificationsNonLues = string.Empty;
        public string NbNotificationsNonLues
        {
            get { return _nbNotificationsNonLues; }
            set
            {
                _nbNotificationsNonLues = value;
                RaisePropertyChangedEvent("NbNotificationsNonLues");
            }
        }

        public ICommand CmdRetourEcran
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        if (GestionnaireSousEcrans.GetEcranPresent() is IEcranRetour)
                            (GestionnaireSousEcrans.GetEcranPresent() as IEcranRetour).CmdRetourEcranPrecedent();
                    }
                );
            }
        }

        public ICommand CmdProfil
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.Show(new ControlMenuUtilisateur(GestionnaireEcrans, GestionnaireSousEcrans), "dialogGeneral:modal=false");
                    }
                );
            }
        }

        public ICommand CmdNotifications
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        DialogHost.Show(new ControlNotifications(), "dialogGeneral:modal=false");
                    }
                );
            }
        }

        public ICommand CmdMessages
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        
                    })
            }
        }

        private void ActualiserNbNotificationsNonLues(object sender, NotificationsEventArgs args)
        {
            int nombre = 0;
            foreach (Notification notification in args.Notifications)
                if (!notification.EstLu)
                    ++nombre;
            NbNotificationsNonLues = nombre.ToString();
        }

        public ControlModelBandeauNavigationGeneral(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;

            GestionnaireNotifications.Instance.ANotifier += ActualiserNbNotificationsNonLues;
        }
    }
}
