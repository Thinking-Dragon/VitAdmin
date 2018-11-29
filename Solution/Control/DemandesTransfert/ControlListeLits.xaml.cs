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
using VitAdmin.ControlModel.DemandesTransfert;
using VitAdmin.Data;


namespace VitAdmin.Control.DemandesTransfert
{
    /// <summary>
    /// Logique d'interaction pour test.xaml
    /// </summary>
    public partial class ControlListeLits : UserControl
    {
        ControlModelListeLits ControlModelListeLits { get; set; }
        public ControlListeLits(List<Lit> lstLits)
        {
            InitializeComponent();
            DataContext = ControlModelListeLits = new ControlModelListeLits(lstLits);

        }

        private void dtgLstLit_Drop(object sender, DragEventArgs e)
        {
            Lit LitSelectionne = (dtgLstLit.SelectedItem as Lit);

            

            if (LitSelectionne.EstDisponible)
            {
                // If the DataObject contains citoyen data, extract it.
                if (e.Data.GetDataPresent(DataFormats.Serializable))
                {
                    Citoyen demandeTransfert = (Citoyen)e.Data.GetData(DataFormats.StringFormat);

                    LitSelectionne.Citoyen = demandeTransfert;
                    DataModelLit.PutLitCitoyen(LitSelectionne, demandeTransfert);
                   
                }
            }
        }

        private void dtgLstLit_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //dtgLstLit.SelectedItem = ControlModelListeLits.
        }
    }
}
