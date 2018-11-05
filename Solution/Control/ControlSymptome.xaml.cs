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

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if((DataContext as ControlModelSymptome).Symptomes.Count() > 0)
            (DataContext as ControlModelSymptome).Symptomes.Remove((Symptome)dtgSymptome.SelectedItem);
        }

        private void dtgSymptome_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Hospitalisation h = (DataContext as ControlModelSymptome).Hospitalisation;
        }
    }
}
