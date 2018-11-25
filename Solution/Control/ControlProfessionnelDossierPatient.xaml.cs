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
using VitAdmin.ControlModel;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlProfessionnelDossierPatient.xaml
    /// </summary>
    public partial class ControlProfessionnelDossierPatient : UserControl
    {
        List<Hospitalisation> LstHospitalisation; // List temporaire qui garde en mémoire tous les hospitalisations trouvées lors de l'accès au dossier du patient. Cela permet d'augmenter la vitesse de l'application et diminuer le nombre de demande à la bd.

        ControlModelProfessionnelDossierPatient controlModelProfessionnelDossierPatient;

        public ControlProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Hospitalisation> hospitalisations, Citoyen patient)
        {
            InitializeComponent();

            controlModelProfessionnelDossierPatient = new ControlModelProfessionnelDossierPatient(gestionnaireEcrans, hospitalisations, patient);

            InitialiserCboDepartement(controlModelProfessionnelDossierPatient.Hospitalisations);

            DataContext = controlModelProfessionnelDossierPatient;
            LstHospitalisation = controlModelProfessionnelDossierPatient.Hospitalisations.ToList();
        }

        // TODO: À refactoriser
        private void InitialiserCboDepartement(ObservableCollection<Hospitalisation> hospitalisations)
        {
            ObservableCollection<Traitement> traitements = new ObservableCollection<Traitement>();
            ObservableCollection<Departement> departements = new ObservableCollection<Departement>();

            // On fait le tour des hospitalisations du patient pour faire sortir les départements liés.
            foreach(Hospitalisation hospitalisation in hospitalisations)
            {
                bool bDepartementPresent = false;
                // On vérifie pour empêcher les doublons de département dans la liste temporaire de traitements.
                foreach(Traitement traitement in traitements)
                {
                    if (traitement.DepartementAssocie.Nom == hospitalisation.LstTraitements[0].DepartementAssocie.Nom)
                    {
                        bDepartementPresent = true;
                        break;
                    }

                }

                if (!bDepartementPresent)
                    traitements.Add(hospitalisation.LstTraitements[0]);
            }

            foreach (Traitement traitement in traitements)
                departements.Add(traitement.DepartementAssocie);

            cboDepartements.ItemsSource = departements;
            cboDepartements.DisplayMemberPath = "Nom";
            cboDepartements.SelectionChanged += cboDepartement_SelectionChanged;

            //Plus besoin de l'ajouter, il est déjà dans le xaml
            //grdDossierPatient.Children.Add(cboDepartements);
        }

        private void dtpkrDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtpkrDebut.SelectedDate != null)
            {

                List<Hospitalisation> LstHospitalisationsRecherche = new List<Hospitalisation>();
                DateTime dateTimeDebut = new DateTime(dtpkrDebut.SelectedDate.Value.Year, dtpkrDebut.SelectedDate.Value.Month, dtpkrDebut.SelectedDate.Value.Day);
           
                // On recherche dans la liste des hospitalisations ceux qui correspondent à la date demandée.
                LstHospitalisationsRecherche = LstHospitalisation.FindAll((hospitalisation) => hospitalisation.DateDebut.ToString("MM/dd/yyyy") == dateTimeDebut.ToString("MM/dd/yyyy"));
                // On vide le contenu de la liste dans le datacontext.
                controlModelProfessionnelDossierPatient.Hospitalisations.Clear();
                // Ajoute dans la liste les hospitalisations trouvées.
                LstHospitalisationsRecherche.ForEach((hospitalisation) => controlModelProfessionnelDossierPatient.Hospitalisations.Add(hospitalisation));
            }

        }

        private void dtpkrFin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtpkrFin.SelectedDate != null)
            {
                List<Hospitalisation> LstHospitalisationsRecherche = new List<Hospitalisation>();
                DateTime dateTimeFin = new DateTime(dtpkrFin.SelectedDate.Value.Year, dtpkrFin.SelectedDate.Value.Month, dtpkrFin.SelectedDate.Value.Day);

                // On recherche dans la liste des hospitalisations ceux qui correspondent à la date demandée.
                LstHospitalisationsRecherche = LstHospitalisation.FindAll((hospitalisation) => hospitalisation.DateFin.ToString("MM/dd/yyyy") == dateTimeFin.ToString("MM/dd/yyyy"));
                // On vide le contenu de la liste dans le datacontext.
                controlModelProfessionnelDossierPatient.Hospitalisations.Clear();
                // Ajoute dans la liste les hospitalisations trouvées.
                LstHospitalisationsRecherche.ForEach((hospitalisation) => controlModelProfessionnelDossierPatient.Hospitalisations.Add(hospitalisation));

            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dtpkrDebut.SelectedDate = null;
            dtpkrFin.SelectedDate = null;

            cboDepartements.Text = "";
            controlModelProfessionnelDossierPatient.Hospitalisations.Clear();

            LstHospitalisation.ForEach((hospitalisation) => controlModelProfessionnelDossierPatient.Hospitalisations.Add(hospitalisation));
        }

        private void cboDepartement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Hospitalisation> LstHospitalisationTrouve = new List<Hospitalisation>();
            List<Hospitalisation> LstHospitalisationDtgTemp = controlModelProfessionnelDossierPatient.Hospitalisations.ToList<Hospitalisation>();
            Departement departementSelectionne = (Departement)cboDepartements.SelectedItem;

            // S'il n'y a plus rien dans la datagrid, tout plante puisqu'il n'y a plus rien à trouver lorsqu'on tente de réinitialiser le datagrid.
            if(departementSelectionne != null)
            {
                LstHospitalisationTrouve = LstHospitalisationDtgTemp.FindAll((hospit) => hospit.LstTraitements[0].DepartementAssocie.Nom == departementSelectionne.Nom);

                controlModelProfessionnelDossierPatient.Hospitalisations.Clear();
                LstHospitalisationTrouve.ForEach((hospit) => controlModelProfessionnelDossierPatient.Hospitalisations.Add(hospit));

            }

            
        }

     
    }
}
