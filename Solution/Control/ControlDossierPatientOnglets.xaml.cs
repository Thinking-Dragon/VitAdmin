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
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Interaction logic for ControlDossierPatientOnglets.xaml
    /// </summary>
    public partial class ControlDossierPatientOnglets : UserControl
    {
        public ControlDossierPatientOnglets(GestionnaireEcrans gestionnaireEcrans, Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();

            NotesMed.Content = new ControlDossierPatientNotesMed(patient, hospit);
            NotesInf.Content = new ControlDossierPatientNotesInf(patient, hospit);
            Prescriptions.Content = new ControlDossierPatientPrescriptions(patient, hospit);
            ResultatsLabo.Content = new ControlDossierPatientResultatsLabo(patient, hospit);
        }
    }
}
