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
        UserControl UserControl { get; set; }
        // Ce dictionnaire permet d'ajouter des types de usercontrol pour trouver le type précis d'un control que l'on recherche.
        public Dictionary<Type, int> userControlDictionary = new Dictionary<Type, int>
        {
            { typeof(ControlTraitementCreationHospitalisation), 0 }
        };

        // Constructeur
        public ControlBarreRechercheTraitement(ObservableCollection<Traitement> traitements, UserControl userControl)
        {
            InitializeComponent();
            DataContext = new ControlModelBarreRechercheTraitement(traitements, userControl);
            UserControl = userControl;
            LstTraitementsTemp = traitements.ToList<Traitement>();
        }

        private void cboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as ControlModelBarreRechercheTraitement).Traitements = new ObservableCollection<Traitement>(LstTraitementsTemp.FindAll(traitement => traitement.Nom.IndexOf(cboRecherche.Text) != -1));
            (sender as ComboBox).IsDropDownOpen = true;
        }

        private void cboRecherche_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Fonctionne pas, je pense qu'il faudrait tout simplement passé la liste des traitements de la nouvelle hospitalisation au lieu d'aller ajouter le traitement par le datagrid
            switch (userControlDictionary[(DataContext as ControlModelBarreRechercheTraitement).UserControl.GetType()])
            {
                case 0 : (UserControl as ControlTraitementCreationHospitalisation).dtgTraitements.Items.Add((Traitement)cboRecherche.SelectedItem);
                    break;
                default:
                    break;
            }
           
        }

    }
}
