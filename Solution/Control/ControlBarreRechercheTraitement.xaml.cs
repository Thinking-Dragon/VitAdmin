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
    /// Logique d'interaction pour ControlBarreRechercheTraitement.xaml
    /// </summary>
    public partial class ControlBarreRechercheTraitement : UserControl
    {
        List<Traitement> LstTraitementsTemp { get; set; }
        // Ce dictionnaire permet d'ajouter des types de usercontrol pour trouver le type précis d'un control que l'on recherche.
        /*public Dictionary<Type, int> userControlDictionary = new Dictionary<Type, int>
        {
            { typeof(ControlTraitementCreationHospitalisation), 0 }
        };*/

        // Constructeur
        public ControlBarreRechercheTraitement(ObservableCollection<Traitement> traitementsTemps, ObservableCollection<Traitement> traitements) // Pour rendre la barre de recherche un jour accessible à plus de contexte
        {
            InitializeComponent();
            DataContext = new ControlModelBarreRechercheTraitement(traitementsTemps, traitements);
            LstTraitementsTemp = traitementsTemps.ToList<Traitement>();
        }

        private void cboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as ControlModelBarreRechercheTraitement).TraitementsTemp = new ObservableCollection<Traitement>(LstTraitementsTemp.FindAll(traitement => traitement.Nom.IndexOf(cboRecherche.Text) != -1));
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void cboRecherche_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            (DataContext as ControlModelBarreRechercheTraitement).Traitements.Add((Traitement)cboRecherche.SelectedItem);


        }

    }
}
