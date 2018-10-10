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
        List<Hospitalisation> LstHospitalisation;
        ComboBox cboDepartements = new ComboBox
        {
            VerticalAlignment = VerticalAlignment.Center,
        };

        public ControlProfessionnelDossierPatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Hospitalisation> hospitalisations)
        {
            InitializeComponent();

            ControlModelProfessionnelDossierPatient controlModelProfessionnelDossierPatient = new ControlModelProfessionnelDossierPatient(hospitalisations);

            InitialiserCboDepartement(controlModelProfessionnelDossierPatient.Hospitalisations);

            DataContext = controlModelProfessionnelDossierPatient;
            LstHospitalisation = controlModelProfessionnelDossierPatient.Hospitalisations.ToList();
        }

        private void InitialiserCboDepartement(ObservableCollection<Hospitalisation> hospitalisations)
        {
            ObservableCollection<Traitement> traitements = new ObservableCollection<Traitement>();

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

            cboDepartements.ItemsSource = traitements;
            cboDepartements.DisplayMemberPath = "DepartementAssocie.Nom";
            cboDepartements.SelectionChanged += cboDepartement_SelectionChanged;

            grdDossierPatient.Children.Add(cboDepartements);
        }

        private void dtpkrDebut_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dtpkrDebut.SelectedDate != null)
            {

                List<Hospitalisation> LstHospitalisationsRecherche = new List<Hospitalisation>();
                DateTime dateTimeDebut = new DateTime(dtpkrDebut.SelectedDate.Value.Year, dtpkrDebut.SelectedDate.Value.Month, dtpkrDebut.SelectedDate.Value.Day);
           

                ControlModelProfessionnelDossierPatient controlModelProfessionnelDossierPatient = (ControlModelProfessionnelDossierPatient)DataContext;

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


                ControlModelProfessionnelDossierPatient controlModelProfessionnelDossierPatient = (ControlModelProfessionnelDossierPatient)DataContext;

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
            ControlModelProfessionnelDossierPatient controlModelProfessionnelDossierPatient = (ControlModelProfessionnelDossierPatient)DataContext;

            dtpkrDebut.SelectedDate = null;
            dtpkrFin.SelectedDate = null;

            cboDepartements.Text = "";

            LstHospitalisation.ForEach((hospitalisation) => controlModelProfessionnelDossierPatient.Hospitalisations.Add(hospitalisation));
        }

        private void cboDepartement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /*void initialiserDataGridHospit(ObservableCollection<Hospitalisation> hospitalisations)
        {
            DataGrid dataGridHospitalisations = new DataGrid
            {
                ItemsSource = hospitalisations,
                IsReadOnly = true,
                AutoGenerateColumns = false,  
            };

            // Binding de DateDebut
            Binding bdgDateDebut = new Binding("DateDebut");
            DataGridTextColumn dtgTextColDateDebut = new DataGridTextColumn { Binding = bdgDateDebut };
            // Binding de DateDebut
            Binding bdgDateFin = new Binding("Hospitalisations.DateFin");
            DataGridTextColumn dtgTextColDateDebut = new DataGridTextColumn { Binding = bdgDateDebut };
        }*/
    }
}
