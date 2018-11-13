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
using VitAdmin.Parameter;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelDossierPatientCreerHospitalisation.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatientCreerHospitalisation : Page, IEcranRetour
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        ViewModelProfessionnelDossierPatientCreerHospitalisation ViewModelProfessionnelDossierPatient { get; set; }

        public ViewProfessionnelDossierPatientCreerHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            ViewModelProfessionnelDossierPatient = new ViewModelProfessionnelDossierPatientCreerHospitalisation(gestionnaireEcrans, citoyen);
            DataContext = ViewModelProfessionnelDossierPatient;




        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestionnaireEcrans.Changer(new ViewProfessionnelDossierPatient(GestionnaireEcrans, ViewModelProfessionnelDossierPatient.Citoyen)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Annuler"; }
        }


    }
}
