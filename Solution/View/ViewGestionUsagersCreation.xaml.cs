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
using VitAdmin.Model;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewGestionUsagersCreation.xaml
    /// </summary>
    public partial class ViewGestionUsagersCreation : Page, IEcranRetour
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        public ViewGestionUsagersCreation(GestionnaireEcrans gestionnaireEcrans, Usager usager = null)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            DataContext = new ViewModelGestionUsagersCreation(gestionnaireEcrans, usager);
        }

        public Action CmdRetourEcranPrecedent => () => GestionnaireEcrans.Changer(new ViewGestionUsagers(GestionnaireEcrans));

        public string TexteBoutonRetourEcran => (DataContext as ViewModelGestionUsagersCreation).Usager == null ? "< Gestion usagers" : "Annuler";

    }
}
