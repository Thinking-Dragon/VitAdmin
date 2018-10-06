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
using VitAdmin.ControlModel;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewSuperEcran.xaml
    /// </summary>
    public partial class ViewSuperEcran : Page
    {
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }
        public ViewSuperEcran(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            ControlBandeauNavigationGeneral controlBandeauNavigationGeneral = new ControlBandeauNavigationGeneral(gestionnaireEcrans, grdSousEcran);
            grdMain.Children.Add(controlBandeauNavigationGeneral);
            GestionnaireSousEcrans = controlBandeauNavigationGeneral.GestionnaireSousEcrans;
            DataContext = new ViewModelSuperEcran(gestionnaireEcrans, GestionnaireSousEcrans);
        }

        public ViewSuperEcran(GestionnaireEcrans gestionnaireEcrans, Page premierSousEcran) : this(gestionnaireEcrans)
            => GestionnaireSousEcrans.Changer(premierSousEcran);

        public ViewSuperEcran(GestionnaireEcrans gestionnaireEcrans, Type typePremierSousEcran) : this(gestionnaireEcrans)
            => GestionnaireSousEcrans.Changer(Activator.CreateInstance(typePremierSousEcran, GestionnaireSousEcrans) as Page);
    }
}
