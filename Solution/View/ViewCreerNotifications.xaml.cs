using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VitAdmin.Notifications;
using VitAdmin.Parameter;
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewCreerNotifications.xaml
    /// </summary>
    public partial class ViewCreerNotifications : Page, IEcranRetour
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewCreerNotifications(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
        }

        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestionnaireEcrans.Changer(new ViewChargementApp(GestionnaireEcrans)); }; }
        }

        public string TexteBoutonRetourEcran
        {
            get { return "< Chargement"; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GestionnaireNotifications.Instance.PostNotification("Vous avez un nouveau message : « Hello world ! »", "message", UsagerConnecte.Usager);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GestionnaireNotifications.Instance.PostNotification("Le dossier du patient « Tourlou » nécessite votre attention", "dossier:tourlou", UsagerConnecte.Usager);
        }
    }
}
