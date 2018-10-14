using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListeTraitementsAvecEtapes.xaml
    /// </summary>
    public partial class ControlListeTraitementsAvecEtapes : UserControl
    {
        private ControlModelListeTraitementsAvecEtapes ControlModel { get; set; }

        public ControlListeTraitementsAvecEtapes(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Traitement> traitements)
        {
            InitializeComponent();
            DataContext = ControlModel = new ControlModelListeTraitementsAvecEtapes(gestionnaireEcrans, traitements);

            cpTraitementsCD.Content = new ControlAjoutSuppression(ControlModel.CmdAjoutTraitement, ControlModel.CmdSuppressionTraitement);
            cpEtapesCD.Content = new ControlAjoutSuppression(ControlModel.CmdAjoutEtapes, ControlModel.CmdSuppressionEtapes);
            cpInstructionsCD.Content = new ControlAjoutSuppression(ControlModel.CmdAjoutInstructions, ControlModel.CmdSuppressionInstructions);
        }

        private void btnModifierEtape_Click(object sender, RoutedEventArgs e)
            => ControlModel.CmdEditerEtape.Execute(null);

        private void btnModifierTraitement_Click(object sender, RoutedEventArgs e)
            => ControlModel.CmdEditerTraitement.Execute(null);

        private void btnModifierInstruction_Click(object sender, RoutedEventArgs e)
            => ControlModel.CmdEditerInstructions.Execute(null);
    }
}
