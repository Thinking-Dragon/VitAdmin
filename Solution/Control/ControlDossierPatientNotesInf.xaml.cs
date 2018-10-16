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
    /// Interaction logic for ControlDossierPatientNotesInf.xaml
    /// </summary>
    public partial class ControlDossierPatientNotesInf : UserControl
    {
        private ControlModelDossierPatientNotesInf ControlModelNoteInf { get; set; }

        public ControlDossierPatientNotesInf(Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = ControlModelNoteInf = new ControlModelDossierPatientNotesInf(patient, hospit, Data.DataModelNotesInf.GetNotesInfirmiereCitoyens(patient.AssMaladie, hospit.DateDebut));

            /*http://420.cstj.qc.ca/laurencedarveau/vitadmin/radio1.jpg*/
        }

        private void NouvelleNote_Click(object sender, RoutedEventArgs e)
            => ControlModelNoteInf.CmdBtnClicNouvelleNoteInf.Execute(ControlModelNoteInf.Hospit);
    }
}
