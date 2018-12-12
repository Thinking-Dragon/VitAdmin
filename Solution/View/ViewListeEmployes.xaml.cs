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
using VitAdmin.Control;
using VitAdmin.Model;
using VitAdmin.Parameter;
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewListeEmployes.xaml
    /// </summary>
    public partial class ViewListeEmployes : Page, IEcranAAideContextuelle
    {
        public ViewListeEmployes(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();

            if (!(this is ViewListeEmployesAdmin) && UsagerConnecte.Usager.RoleUsager == Role.admin)
                gestionnaireEcrans.Changer(new ViewListeEmployesAdmin(gestionnaireEcrans));

            this.Content = new ControlListeEmployes(gestionnaireEcrans);
        }

        public string AncreSectionAideContextuelle => "MgestionnairePersonnel";
    }
}
