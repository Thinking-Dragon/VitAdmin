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

namespace VitAdmin.Control.DemandesTransfert
{
    /// <summary>
    /// Logique d'interaction pour ControlListeDemandesTransfert.xaml
    /// </summary>
    public partial class ControlListeDemandesTransfert : UserControl
    {
        public ControlListeDemandesTransfert(List<Citoyen> LstCitoyen)
        {
            InitializeComponent();
        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
