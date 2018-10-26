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

        ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide> Slides { get; set; }

        public ViewProfessionnelDossierPatientCreerHospitalisation(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen)
        {
            InitializeComponent();

            Slides = new ObservableCollection<MaterialDesignThemes.Wpf.Transitions.TransitionerSlide>();
            MaterialDesignThemes.Wpf.Transitions.Transitioner transitioner = new MaterialDesignThemes.Wpf.Transitions.Transitioner { SelectedIndex = 0 };
            DataContext = new ViewModelProfessionnelDossierPatientCreerHospitalisation(gestionnaireEcrans, citoyen);

            #region Création des Grid et buttons
            

            Grid grdContexte = new Grid();
            Grid grdContexte2 = new Grid();

            grdContexte.Children.Add((DataContext as ViewModelProfessionnelDossierPatientCreerHospitalisation).LstUserControl[0]);

      

            grdContexte2.Children.Add(new Control.ControlTextBoxHospitalisation("","Contexte 2"));

            #endregion

            #region Création des transitions slides
            MaterialDesignThemes.Wpf.Transitions.TransitionerSlide transitionerSlideContexte = new MaterialDesignThemes.Wpf.Transitions.TransitionerSlide
            {
                Content = grdContexte,
                //OpeningEffect = new MaterialDesignThemes.Wpf.Transitions.TransitionEffect(MaterialDesignThemes.Wpf.Transitions.TransitionEffectKind.SlideInFromLeft)
                

            };
          

            MaterialDesignThemes.Wpf.Transitions.TransitionerSlide transitionerSlideContexte2 = new MaterialDesignThemes.Wpf.Transitions.TransitionerSlide
            {
                Content = grdContexte2
            };

            Slides.Add(transitionerSlideContexte);
            Slides.Add(transitionerSlideContexte2);

            #endregion

            transitioner.ItemsSource =  Slides;

            Grid.SetRow(transitioner, 1);

            grdCreerHospitalisation.Children.Add(transitioner);
         


        }

        private List<Grid> CreerListGridPourChaqueUC()
        {
            List<Grid> lstGrid = new List<Grid>();
            int iCompteur = 0;

            (DataContext as ViewModelProfessionnelDossierPatientCreerHospitalisation).LstUserControl.ForEach(uc =>
            {
                Grid grdTempAjoutChildren; // grid tem
                lstGrid.Add(new Grid { Name = new StringBuilder("grd" + iCompteur.ToString()).ToString()});
                grdTempAjoutChildren = lstGrid.Find(grid => grid.Name == new StringBuilder("grd" + iCompteur.ToString()).ToString());

            });

            return new List<Grid>();
        }

        /// <summary>
        /// Fonction qui créer dans chaque grid un bouton suivant et un bouton précédent qui sont lié au transitioner.
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
    }
}
