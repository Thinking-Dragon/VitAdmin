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
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;
using VitAdmin.Control;
using VitAdmin.ControlModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewDossierPatientInfos.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatientInfosModif : Page, IEcranRetour
    {
        GestionnaireEcrans GestEcrans { get; set; }
        Citoyen Patient { get; set; }
        ControlDossierPatientInfos ControlDossierPatientInfos { get; set; }

        public ViewProfessionnelDossierPatientInfosModif(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            InitializeComponent();
            Patient = patient;
            GestEcrans = gestionnaireEcrans;
            ControlDossierPatientInfos = new ControlDossierPatientInfos(Patient);
            DataContext = new ViewModelProfessionnelDossierPatientInfosModif(gestionnaireEcrans, Patient, (ControlDossierPatientInfos.DataContext as ControlModelDossierPatientInfos));

            Grid.SetRow(ControlDossierPatientInfos, 1);
            grdPatientInfos.Children.Add(ControlDossierPatientInfos);
        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcrans.Changer(new ViewProfessionnelDossierPatient(GestEcrans, Patient)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Dossier patient"; }
        }
    }
}
