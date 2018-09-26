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

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListePatient.xaml
    /// </summary>
    public partial class ControlListePatient : UserControl
    {
        public ControlListePatient()
        {
            InitializeComponent();

            txtRecherche.Text = "Recherche";
        }

        // Enlève le placeholder lorsqu'il y a focus sur le txtbox
        private void txtRecherche_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRecherche.Text = "";
        }

        // Rajoute le placeholder lorsqu'il n'y a plus de texte et défocuse.
        private void txtRecherche_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRecherche.Text))
                txtRecherche.Text = "Recherche";
        }
    }
}
