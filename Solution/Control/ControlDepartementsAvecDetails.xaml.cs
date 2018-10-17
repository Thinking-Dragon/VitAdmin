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
    /// Logique d'interaction pour ControlDepartementsAvecDetails.xaml
    /// </summary>
    public partial class ControlDepartementsAvecDetails : UserControl
    {
        public ControlDepartementsAvecDetails(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            DataContext = new ControlModelDepartementsAvecDetails(gestionnaireEcrans);
        }

        private void dtgDepartements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DataContext as ControlModelDepartementsAvecDetails).DepartementSelectionne != null)
                btnModifierDepartement.IsEnabled = true;
            else
                btnModifierDepartement.IsEnabled = false;
        }
    }
}
