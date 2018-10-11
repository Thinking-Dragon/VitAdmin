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

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlDossierPatientNotesMed.xaml
    /// </summary>
    public partial class ControlDossierPatientNotesMed : UserControl
    {
        public ControlDossierPatientNotesMed(Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = new ControlModelDossierPatientNotesMed(patient, hospit, Data.DataModelNotesMed.GetNotesMedecinCitoyens(patient.AssMaladie, hospit.DateDebut));
        }
    }
}
