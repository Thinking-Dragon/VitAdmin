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
using VitAdmin.Control;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewSuperEcran.xaml
    /// </summary>
    public partial class ViewSuperEcran : Page
    {
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }
        public ViewSuperEcran(GestionnaireEcrans gestionnaireEcrans, Page premierSousEcran)
        {
            InitializeComponent();
            GestionnaireSousEcrans = new GestionnaireEcrans(grdSousEcran, premierSousEcran);
            grdMain.Children.Add(new ControlBandeauNavigationGeneral(gestionnaireEcrans, GestionnaireSousEcrans));
            DataContext = new ViewModelSuperEcran(gestionnaireEcrans, GestionnaireSousEcrans);
        }
    }
}
