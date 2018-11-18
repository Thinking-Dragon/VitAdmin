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
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;
using VitAdmin.View;
using VitAdmin.Parameter;
using VitAdmin.Control;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewHubProfessionnel.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatient : Page, IEcranRetour
    {
        public GestionnaireEcrans GestEcrans { get; set; }
        public ControlProfessionnelDossierPatient ControlProfessionnelDossierPatient { get; set; }

        public ViewProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            InitializeComponent();
            GestEcrans = gestionnaireEcrans;
            ControlProfessionnelDossierPatient = new ControlProfessionnelDossierPatient(gestionnaireEcrans, new ObservableCollection<Hospitalisation>(Data.DataModelHospitalisation.getHospitalisation(patient)), patient);
            DataContext = new ViewModelProfessionnelDossierPatient(gestionnaireEcrans, patient);

            Grid.SetRow(ControlProfessionnelDossierPatient, 1);
            Grid.SetColumnSpan(ControlProfessionnelDossierPatient, 2);
            grdListeHospitalisation.Children.Add(ControlProfessionnelDossierPatient);

        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcrans.Changer(new ViewProfessionnelHub(GestEcrans, UsagerConnecte.Usager)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Liste patients"; }
        }

    }
}
