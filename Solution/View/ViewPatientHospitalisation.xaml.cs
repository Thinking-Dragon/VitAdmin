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
    /// Interaction logic for ViewPatientHospitalisation.xaml
    /// </summary>
    public partial class ViewPatientHospitalisation : Page
    {
        public ViewPatientHospitalisation(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            DataContext = new ViewModelPatientHospitalisation(gestionnaireEcrans);
            this.Content = new ControlDossierPatientOnglets(gestionnaireEcrans);
            
        }
    }
}
