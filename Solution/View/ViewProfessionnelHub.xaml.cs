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
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.ViewModel;
using VitAdmin.View.Tool;
using VitAdmin.Parameter;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelHub.xaml
    /// </summary>
    public partial class ViewProfessionnelHub : Page, IEcranRetour
    {
        ViewModelProfessionnelHub ViewModelProfessionnelHub { get; set; }
        // TODO: Modifier le paramètres pour qu'il recoit les infos du professionnel qui se connecte
        // Ainsi, le filtre département sera par défaut le département de l'employé ainsi la liste des
        // professionnels sera mis à jour.
        public ViewProfessionnelHub(GestionnaireEcrans gestionnaireEcrans, Employe employe) //Remplacer tous les paramètres par Employe, voir avec Clément
        {
            InitializeComponent();
            Departement departementEmploye = DataModelDepartement.GetDepartementEmploye(employe);

            ViewModelProfessionnelHub = new ViewModelProfessionnelHub(gestionnaireEcrans);
            DataContext = ViewModelProfessionnelHub;

            // TODO : À refactoriser ****
            Control.ControlListePatient ctrlLstPatient = 
                new Control.ControlListePatient(
                    gestionnaireEcrans, 
                    UsagerConnecte.Usager.NomUtilisateur == "admin" ? new ObservableCollection<Citoyen>(DataModelCitoyen.GetTousCitoyensDepartement(new Departement { Nom = "Chirurgie", Abreviation = "CHR" })) : new ObservableCollection<Citoyen>(DataModelCitoyen.GetCitoyensLstPatient(employe)), 
                    new ObservableCollection<Departement>(DataModelDepartement.GetDepartements()), 
                    new ObservableCollection<Employe>(DataModelEmploye.GetLstEmployesDepartement(departementEmploye)),
                    departementEmploye, 
                    employe);

            Grid.SetColumnSpan(ctrlLstPatient, 2);

            grdLstPatient.Children.Add(ctrlLstPatient);
        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { ViewModelProfessionnelHub.GestionnaireEcrans.Changer(new ViewChargementApp(ViewModelProfessionnelHub.GestionnaireEcrans)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Accueil"; }
        }
    }
}
