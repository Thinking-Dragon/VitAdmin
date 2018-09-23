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
using VitAdmin.ControlModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlBandeauNavigationGeneral.xaml
    /// </summary>
    public partial class ControlBandeauNavigationGeneral : UserControl
    {
        public ControlBandeauNavigationGeneral(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            InitializeComponent();
            DataContext = new ControlModelBandeauNavigationGeneral(gestionnaireEcrans, gestionnaireSousEcrans);
        }
    }
}
