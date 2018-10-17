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
using VitAdmin.Model;
using VitAdmin.View.Tool;
using VitAdmin.Parameter;

namespace VitAdmin.View
{
    /// <summary>
    /// Interaction logic for ViewPatientHospitalisation.xaml
    /// </summary>
    public partial class ViewPatientHospitalisation : Page, IEcranRetour
    {
        public GestionnaireEcrans GestEcrans { get; set; }
        public Citoyen Patient { get; set; }

        public ViewPatientHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen patient, Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = new ViewModelPatientHospitalisation(gestionnaireEcrans, patient);
            this.Content = new ControlDossierPatientOnglets(gestionnaireEcrans, patient, hospit);
            Patient = patient;
            GestEcrans = gestionnaireEcrans;
        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcrans.Changer(new ViewProfessionnelDossierPatient(GestEcrans, Patient)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Hospitalisations"; }
        }
    }
}
