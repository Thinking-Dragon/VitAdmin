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
using VitAdmin.Data;
using VitAdmin.Parameter;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewDemandesTransfert.xaml
    /// </summary>
    public partial class ViewDemandesTransfert : Page
    {
        ViewModelDemandesTransfert ViewModelDemandesTransfert { get; set; }
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        public ViewDemandesTransfert(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            DataContext = ViewModelDemandesTransfert = new ViewModelDemandesTransfert();
            InitialiserUsersControls();



        }

        private void InitialiserUsersControls()
        {
            Departement departement = DataModelDepartement.GetDepartementInfChef(UsagerConnecte.Usager);

            List<Lit> LstLitsDepartement = DataModelLit.GetLitsDepartement(departement, true);
            List<Citoyen> lstCitoyenDemandeTransfert = DataModelCitoyen.GetCitoyenDemandeTraitement(departement);

            ControlListeLits controlInfos = new ControlListeLits(LstLitsDepartement);
            ControlListeDemandesTransfert controlListeDemandesTransfert = new ControlListeDemandesTransfert(lstCitoyenDemandeTransfert);

            Grid.SetRow(controlInfos, 1);
            Grid.SetRow(controlListeDemandesTransfert, 1);
            Grid.SetColumn(controlListeDemandesTransfert, 1);

            grdGestionLit.Children.Add(controlInfos);
            grdGestionLit.Children.Add(controlListeDemandesTransfert);
        }
    }
}
