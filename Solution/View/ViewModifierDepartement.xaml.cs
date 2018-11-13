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
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewModifierDepartement.xaml
    /// </summary>
    public partial class ViewModifierDepartement : Page, IEcranRetour
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewModifierDepartement(GestionnaireEcrans gestionnaireEcrans, Departement departement = null)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            cpControlCRUD.Content = new ControlModifierDepartement(gestionnaireEcrans, departement);
        }

        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestionnaireEcrans.Changer(new ViewAdminModificationStructure(GestionnaireEcrans)); }; }
        }

        public string TexteBoutonRetourEcran
        {
            get { return "< Départements"; }
        }
    }
}
