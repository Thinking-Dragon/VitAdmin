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
        public ControlBarreRechercheTraitement(ObservableCollection<Traitement> traitements)
        {
            InitializeComponent();
            DataContext = new ControlModelBarreRechercheTraitement(traitements);
            LstTraitementsTemp = traitements.ToList<Traitement>();
        }

        private void cboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            (DataContext as ControlModelBarreRechercheTraitement).Traitements = new ObservableCollection<Traitement>(LstTraitementsTemp.FindAll(traitement => traitement.Nom.IndexOf(cboRecherche.Text) != -1));
            //(sender as ComboBox).IsDropDownOpen = true;
        }
    }
}
