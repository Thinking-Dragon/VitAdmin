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
using VitAdmin.ControlModel;
using VitAdmin.Data;

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlDossierPatientPrescriptions.xaml
    /// </summary>
    public partial class ControlDossierPatientPrescriptions : UserControl
    {
        private ControlModelDossierPatientPrescriptions ControlModelPrescription { get; set; }
        public ControlDossierPatientPrescriptions(Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = ControlModelPrescription = new ControlModelDossierPatientPrescriptions(patient, hospit ,DataModelPrescriptions.GetPrescriptionsCitoyens(patient.AssMaladie));
        }

        private void NouvellePrescription_Click(object sender, RoutedEventArgs e)
            => ControlModelPrescription.CmdBtnClicNouvellePrescription.Execute(ControlModelPrescription.Hospit);

    }
}
