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
using VitAdmin.Data;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewGestionEquipements.xaml
    /// </summary>
    public partial class ViewGestionEquipements : Page, IEcranRetour
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private ViewModelGestionEquipements ViewModel { get; set; }

        public ViewGestionEquipements(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            DataContext = ViewModel = new ViewModelGestionEquipements(gestionnaireEcrans, DataModelEquipement.GetEquipements());
        }

        private void btnModifierEquipement_Click(object sender, RoutedEventArgs e)
            => ViewModel.CmdModifierEquipement.Execute(null);

        public Action CmdRetourEcranPrecedent => () => GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans));

        public string TexteBoutonRetourEcran => "< Infrastructure";
    }
}
