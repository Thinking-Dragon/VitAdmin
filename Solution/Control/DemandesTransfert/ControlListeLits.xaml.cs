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
        public ControlListeDemandesTransfert ControlListeDemandesTransfert { get; set; }
        public ControlListeLits(List<Lit> lstLits)
        {
            InitializeComponent();
            DataContext = ControlModelListeLits = new ControlModelListeLits(lstLits);

        }

        private void dtgLstLit_Drop(object sender, DragEventArgs e)
        {
            Lit litLibre = new Lit();
            foreach(Lit lit in ControlModelListeLits.Lits)
            {
                if (lit.EtatLit == EtatLit.Libre)
                {
                    litLibre = lit;
                }
            }

            if (litLibre.Numero != null)
            {
                // If the DataObject contains citoyen data, extract it.
                if (e.Data.GetDataPresent(DataFormats.Serializable))
                {
                    Citoyen demandeTransfert = (Citoyen)e.Data.GetData(DataFormats.Serializable);

                    litLibre.Citoyen = demandeTransfert;
                    ControlModelListeLits.Lits.Remove(litLibre);
                    ControlModelListeLits.Lits.Add(litLibre);
                    DataModelLit.PutNouveauLitCitoyen(litLibre, demandeTransfert);
                    (ControlListeDemandesTransfert.DataContext as ControlModelListeDemandesTransfert).Citoyens = (ControlListeDemandesTransfert.DataContext as ControlModelListeDemandesTransfert).Citoyens;


                }
            }
        }

    }
}
