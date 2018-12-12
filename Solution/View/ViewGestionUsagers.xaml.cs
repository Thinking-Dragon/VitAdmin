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
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewGestionUsagers.xaml
    /// </summary>
    public partial class ViewGestionUsagers : Page, IEcranRetour, IEcranAAideContextuelle
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewGestionUsagers(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            DataContext = new ViewModelGestionUsagers(gestionnaireEcrans);
        }

        public Action CmdRetourEcranPrecedent => () => GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));

        public string TexteBoutonRetourEcran => "< Accueil";

        public string AncreSectionAideContextuelle => "MAusagers";
    }
}
