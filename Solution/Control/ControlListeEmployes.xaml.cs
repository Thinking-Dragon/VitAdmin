using System;
using System.Collections.Generic;
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

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListeEmployes.xaml
    /// </summary>
    public partial class ControlListeEmployes : UserControl
    {
        public ControlListeEmployes(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            btnHoraire.IsEnabled = false;
            btnProfil.IsEnabled = false;
            DataContext = new ControlModelListeEmployes(gestionnaireEcrans);
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            btnHoraire.IsEnabled = true;
            btnProfil.IsEnabled = true;
        }
    }
}