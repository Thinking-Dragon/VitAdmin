using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlProfessionnelDossierPatient.xaml
    /// </summary>
    public partial class ControlProfessionnelDossierPatient : UserControl
    {
        public ControlProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Hospitalisation> hospitalisations)
        {
            InitializeComponent();

            DataContext = new ControlModel.ControlModelProfessionnelDossierPatient(hospitalisations);
        }
    }
}
