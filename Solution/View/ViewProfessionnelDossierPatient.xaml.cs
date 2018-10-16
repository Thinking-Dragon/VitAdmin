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
using VitAdmin.ModelView;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewHubProfessionnel.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatient : Page, IEcranRetour
    {
        public GestionnaireEcrans GestEcrans { get; set; }

        public ViewProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, Citoyen patient)
        {
            InitializeComponent();

            DataContext = new ViewModelProfessionnelDossierPatient(gestionnaireEcrans, patient);

            grdListeHospitalisation.Children.Add(new Control.ControlProfessionnelDossierPatient(gestionnaireEcrans, new ObservableCollection<Hospitalisation>(Data.DataModelHospitalisation.getHospitalisation(patient)), patient));

        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcrans.Changer(new ViewChargementApp(GestEcrans)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Accueil"; }
        }

    }
}
