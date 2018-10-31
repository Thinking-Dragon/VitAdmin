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
using VitAdmin.ControlModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlTraitementCreationHospitalisation.xaml
    /// </summary>
    public partial class ControlTraitementCreationHospitalisation : UserControl
    {
        ObservableCollection<Traitement> Traitements { get; set; }

        public ControlTraitementCreationHospitalisation(List<Traitement> traitements) // IMPORTANT : cette liste de traitement est associé à l'hospitalisation nouvellement créée.
        {
            InitializeComponent();
            Traitements = new ObservableCollection<Traitement>(traitements);
            DataContext = new ControlModelTraitementCreationHospitalisation(Traitements);

            InitialiserControlRechercheTraitement();

        }

        private void InitialiserControlRechercheTraitement()
        {
            // On crée une nouvelle liste de traitements indépendante qui va stocker tous les traitements existants
            ControlBarreRechercheTraitement controlBarreRechercheTraitement = new ControlBarreRechercheTraitement(
                new ObservableCollection<Traitement>(DataModelTraitement.GetTraitements()), this);

            controlBarreRechercheTraitement.HorizontalAlignment = HorizontalAlignment.Left;
            controlBarreRechercheTraitement.VerticalAlignment = VerticalAlignment.Center;
            controlBarreRechercheTraitement.Width = 120;

            Grid.SetRow(controlBarreRechercheTraitement, 1);

            grdTraitements.Children.Add(controlBarreRechercheTraitement);
        }

        
    }
}
