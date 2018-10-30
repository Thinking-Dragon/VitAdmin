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
using VitAdmin.Parameter;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;


namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelCreerPatient.xaml
    /// </summary>
    public partial class ViewProfessionnelCreerPatient : Page, IEcranRetour
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        ViewModelProfessionnelCreerPatient ViewModelProfessionnelCreerPatient { get; set; }

        public ViewProfessionnelCreerPatient(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            ViewModelProfessionnelCreerPatient = new ViewModelProfessionnelCreerPatient(gestionnaireEcrans, new Citoyen());
            DataContext = ViewModelProfessionnelCreerPatient;

            grdCreerPatient.Children.Add(new Control.ControlDossierPatientInfos(ViewModelProfessionnelCreerPatient.Citoyen));

        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestionnaireEcrans.Changer(new ViewProfessionnelHub(GestionnaireEcrans, UsagerConnecte.Usager)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Annuler"; }
        }
    }
}
