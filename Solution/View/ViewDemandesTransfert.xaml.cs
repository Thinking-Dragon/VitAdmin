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
using VitAdmin.Control.DemandesTransfert;
using VitAdmin.Model;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewDemandesTransfert.xaml
    /// </summary>
    public partial class ViewDemandesTransfert : Page
    {
        ViewModelDemandesTransfert ViewModelDemandesTransfert { get; set; }
        public ViewDemandesTransfert(Citoyen citoyen)
        {
            InitializeComponent();
            DataContext = ViewModelDemandesTransfert = new ViewModelDemandesTransfert(citoyen);
            InitialiserUsersControls(citoyen);



        }

        private void InitialiserUsersControls(Citoyen citoyen)
        {
            ControlInfos controlInfos = new ControlInfos(citoyen);
            ControlEquipements controlEquipements = new ControlEquipements(citoyen);
            ControlListeDemandesTransfert controlListeDemandesTransfert = new ControlListeDemandesTransfert(citoyen);

            Grid.SetRow(controlInfos, 1);
            Grid.SetRow(controlEquipements, 2);
            Grid.SetRow(controlListeDemandesTransfert, 1);
            Grid.SetColumn(controlListeDemandesTransfert, 1);
            Grid.SetRowSpan(controlListeDemandesTransfert, 2);

            grdGestionLit.Children.Add(controlInfos);
            grdGestionLit.Children.Add(controlEquipements);
            grdGestionLit.Children.Add(controlListeDemandesTransfert);
        }
    }
}
