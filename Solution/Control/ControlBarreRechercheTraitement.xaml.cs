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
        public ControlBarreRechercheTraitement(ObservableCollection<Traitement> traitements)
        {
            InitializeComponent();
            DataContext = new ControlModelBarreRechercheTraitement(traitements);
        }

        private void cboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as ControlModelBarreRechercheTraitement).Traitements.Add(new Traitement
            {
                Nom = "Radiographie"
            });
            (sender as ComboBox).IsDropDownOpen = true;
        }
    }
}
