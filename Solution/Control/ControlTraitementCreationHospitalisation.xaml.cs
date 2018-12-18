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
using System.Collections.Specialized;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlTraitementCreationHospitalisation.xaml
    /// </summary>
    public partial class ControlTraitementCreationHospitalisation : UserControl
    {
        ObservableCollection<Traitement> Traitements { get; set; }
        

        public ControlTraitementCreationHospitalisation(List<Traitement> traitements, Action callRequeteLits) // IMPORTANT : cette liste de traitement est associé à l'hospitalisation nouvellement créée.
        {
            InitializeComponent();
            Traitements = new ObservableCollection<Traitement>(traitements);
            DataContext = new ControlModelTraitementCreationHospitalisation(Traitements, callRequeteLits, traitements);

            // Pas le choix pour être en mesure d'ajouter l'event CollectionChanged... aye
            dtgTraitements.ItemsSource = Traitements;

            (dtgTraitements.ItemsSource as ObservableCollection<Traitement>).CollectionChanged += new NotifyCollectionChangedEventHandler(DataGrid_CollectionChanged);

            InitialiserControlRechercheTraitement();

        }

        private void InitialiserControlRechercheTraitement()
        {
            // On crée une nouvelle liste de traitements indépendante qui va stocker tous les traitements existants
            ControlBarreRechercheTraitement controlBarreRechercheTraitement = new ControlBarreRechercheTraitement(
                new ObservableCollection<Traitement>(DataModelTraitement.GetTraitements()), Traitements);

            controlBarreRechercheTraitement.HorizontalAlignment = HorizontalAlignment.Left;
            controlBarreRechercheTraitement.VerticalAlignment = VerticalAlignment.Center;
            controlBarreRechercheTraitement.Width = 150;
            controlBarreRechercheTraitement.ToolTip = "Taper le nom du traitement à ajouter dans la liste. Appuyer sur entrer pour l'ajouter.";

            Grid.SetRow(controlBarreRechercheTraitement, 1);

            grdTraitements.Children.Add(controlBarreRechercheTraitement);
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ControlModelTraitementCreationHospitalisation).Traitements.Remove((Traitement)dtgTraitements.SelectedItem);

            // Il faut s'assurer qu'il y a au moins un traitement avant de passer à la sélection du lit, sinon aucun lit sera trouvé.
            if ((DataContext as ControlModelTraitementCreationHospitalisation).Traitements.Count == 0)
                btnSuivant.IsEnabled = false;
        }

        void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if ((DataContext as ControlModelTraitementCreationHospitalisation).Traitements.Count > 0)
                btnSuivant.IsEnabled = true;
        }
    }
}
