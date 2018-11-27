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
using VitAdmin.Model;
using VitAdmin.ControlModel;
using VitAdmin.Data;
using System.Collections.ObjectModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlAjouterPatientLit.xaml
    /// </summary>
    public partial class ControlAjouterPatientLit : UserControl
    {
        public void CallRequeteLits() => (DataContext as ControlModelAjouterPatientLit).CallRequeteLit();
        //ObservableCollection<Lit> Lits { get; set; }
        public ControlAjouterPatientLit(GestionnaireEcrans gestionnaireEcrans, Citoyen citoyen, Lit lit, Hospitalisation hospitalisation)
        {
            InitializeComponent();

            DataContext = new ControlModelAjouterPatientLit(gestionnaireEcrans, citoyen, lit, hospitalisation, new List<Lit>());
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Pour tester
           //Hospitalisation hops = (DataContext as ControlModelAjouterPatientLit).Hospitalisation;
            if (dtgSelectionLit.SelectedItem != null /*&& (dtgSelectionLit.SelectedItem as Lit).EstDisponible*/)
            {
                (DataContext as ControlModelAjouterPatientLit).Lit = (Lit)dtgSelectionLit.SelectedItem;

                btnTerminer.IsEnabled = true;
            }
            else
                btnTerminer.IsEnabled = false;
        }
    }
}
