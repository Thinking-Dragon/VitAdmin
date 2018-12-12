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
using VitAdmin.View.Tool;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlBandeauNavigationGeneral.xaml
    /// </summary>
    public partial class ControlBandeauNavigationGeneral : UserControl
    {
        public GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        public ControlBandeauNavigationGeneral(GestionnaireEcrans gestionnaireEcrans, Panel grdSousEcran)
        {
            InitializeComponent();
            GestionnaireSousEcrans = new GestionnaireEcrans(grdSousEcran, prochainEcran =>
            {
                if (prochainEcran is IEcranRetour)
                {
                    (DataContext as ControlModelBandeauNavigationGeneral).TexteBoutonRetourEcran = (prochainEcran as IEcranRetour).TexteBoutonRetourEcran;
                    btnRetourEcran.Visibility = Visibility.Visible;
                }
                else btnRetourEcran.Visibility = Visibility.Hidden;

                if (prochainEcran is IEcranAAideContextuelle)
                {
                    (DataContext as ControlModelBandeauNavigationGeneral).AncreAideContextuelle = (prochainEcran as IEcranAAideContextuelle).AncreSectionAideContextuelle;
                    btnAideContextuelle.Visibility = Visibility.Visible;
                }
                else btnAideContextuelle.Visibility = Visibility.Hidden;
            });
            DataContext = new ControlModelBandeauNavigationGeneral(gestionnaireEcrans, GestionnaireSousEcrans);
        }
    }
}
