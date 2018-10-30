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
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlTraitementCreationHospitalisation.xaml
    /// </summary>
    public partial class ControlTraitementCreationHospitalisation : UserControl
    {
        public ControlTraitementCreationHospitalisation()
        {
            InitializeComponent();

            InitialiserControlRechercheTraitement();

        }

        private void InitialiserControlRechercheTraitement()
        {
            ControlBarreRechercheTraitement controlBarreRechercheTraitement = new ControlBarreRechercheTraitement(
                new ObservableCollection<Traitement>(DataModelTraitement.GetTraitements()));
            controlBarreRechercheTraitement.HorizontalAlignment = HorizontalAlignment.Left;
            controlBarreRechercheTraitement.VerticalAlignment = VerticalAlignment.Center;
            controlBarreRechercheTraitement.Width = 120;

            Grid.SetRow(controlBarreRechercheTraitement, 1);

            grdTraitements.Children.Add(controlBarreRechercheTraitement);
        }

        
    }
}
