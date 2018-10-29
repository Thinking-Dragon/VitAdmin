using System;
using System.Windows.Controls;
using VitAdmin.ControlModel;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlEditionChambre.xaml
    /// </summary>
    public partial class ControlEditionChambre : UserControl
    {
        public ControlEditionChambre(Action<Chambre> callback, Chambre chambre = null)
        {
            InitializeComponent();
            DataContext = new ControlModelEditionChambre(chambre, callback);
        }

        private void LitSelectionneEtat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
