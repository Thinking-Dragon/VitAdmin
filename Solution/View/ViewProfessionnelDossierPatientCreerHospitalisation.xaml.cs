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
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelDossierPatientCreerHospitalisation.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatientCreerHospitalisation : Page
    {

        //ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide> Slides { get; set; }

        public ViewProfessionnelDossierPatientCreerHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            InitializeComponent();
            DataContext = new ViewModelProfessionnelDossierPatientCreerHospitalisation(gestionnaireEcrans, citoyen);


        }

        

        /// <summary>
        /// Cette fonction crée des Grids contenant chacun un UserControl qui sont destiné à être chacun ajouter à un transitionerSlide. 
        /// </summary>
        /// <returns></returns>
        private List<Grid> CreerListGridPourChaqueUC()
        {
            List<Grid> lstGrid = new List<Grid>();
            int iCompteur = 0;

            // Pour chaque usercontrol dans la liste, on crée une grid qui va contenir l'usercontrol.
            (DataContext as ViewModelProfessionnelDossierPatientCreerHospitalisation).LstUserControl.ForEach(uc =>
            {
                Grid grdTempAjoutChildren; // grid temporaire pour ajouter des éléments dans le grid
                lstGrid.Add(new Grid
                {
                    Name = new StringBuilder("grd" + iCompteur.ToString()).ToString()
                });


                // On ajoute dans la liste un nouveau grid pour chaque UC
                grdTempAjoutChildren = lstGrid.Find(grid => grid.Name == new StringBuilder("grd" + iCompteur.ToString()).ToString()); // On retourne dans le grid temporaire le grid dans lequel on ajoute dans son children le UC
               /* grdTempAjoutChildren.RowDefinitions.Add(new RowDefinition());
                grdTempAjoutChildren.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
                grdTempAjoutChildren.RowDefinitions.Add(new RowDefinition());*/


                // On ajoute dans le grid un usercontrol
                grdTempAjoutChildren.Children.Add(uc);

            });

            return lstGrid;
        }

        /// <summary>
        /// Fonction qui crée dans chaque grid un bouton suivant et un bouton précédent qui sont lié au transitioner.
        /// </summary>
        /// <param name="lstGrid">liste de grid qui contient chaque usercontrol pour la création d'une hospitalisation</param>
        private void CreerBoutonSuivantPrecedentPourUserControl(List<Grid> lstGrid)
        {
            int iCompteur = 0;
            
            lstGrid.ForEach(grid =>
            {
                iCompteur++;
                StringBuilder NomBoutton = new StringBuilder("btnSuivant" + iCompteur.ToString());
                grid.Children.Add(new Button
                {
                    Name = NomBoutton.ToString(),
                    Content = "Suivant",
                    Width = 100,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Command = MaterialDesignThemes.Wpf.Transitions.Transitioner.MoveNextCommand
                });
            });

            iCompteur = 0;

            lstGrid.ForEach(grid =>
            {
                iCompteur++;
                StringBuilder NomBoutton = new StringBuilder("btnPrecedent" + iCompteur.ToString());
                grid.Children.Add(new Button
                {
                    Name = NomBoutton.ToString(),
                    Content = "Précédent",
                    Width = 100,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Command = MaterialDesignThemes.Wpf.Transitions.Transitioner.MovePreviousCommand
                });
            });
        }

        /// <summary>
        /// On crée des transitionerSlides qui vont détenir un grid comme contexte.
        /// </summary>
        /// <param name="lstGrid"></param>
        /// <returns></returns>
        private ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide> CreerOCtransitionerSlides(List<Grid> lstGrid)
        {
            ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide> slides = new ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide>();

            // On ajoute au Content de chaque transitionerSlide une grid contenant un UC
            lstGrid.ForEach(grid =>
            {
                MaterialDesignThemes.Wpf.Transitions.TransitionerSlide newSlide = new MaterialDesignThemes.Wpf.Transitions.TransitionerSlide
                {
                    Content = grid
                    // TODO: Voir ici pour modifier les transitions effects
                };

                slides.Add(newSlide);

            });

            return slides;
        }
    }
}
