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
using VitAdmin.Model;
using VitAdmin.ViewModel;
using VitAdmin.ControlModel;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlAjoutSymptome.xaml
    /// </summary>
    public partial class ControlSymptome : UserControl
    {
        public ControlSymptome(Hospitalisation hospitalisation)
        {
            InitializeComponent();
            DataContext = new ControlModelSymptome(hospitalisation);
        }

        /*private void btnAjouterSymptome_Click(object sender, RoutedEventArgs e)
        {
            Symptome symptomeAjout = new Symptome { Description = "Ajouter la description" };

            if (DataContext.LstSymptomes.Any<Symptome>())
                (DataContext as Hospitalisation).LstSymptomes = new List<Symptome>();

            (DataContext as Hospitalisation).LstSymptomes.Add(symptomeAjout);
            //dtgSymptome.Items.Add(symptomeAjout);
        }*/
    }
}
