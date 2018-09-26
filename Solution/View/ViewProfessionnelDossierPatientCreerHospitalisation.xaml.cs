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

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelDossierPatientCreerHospitalisation.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatientCreerHospitalisation : Page
    {
        public ViewProfessionnelDossierPatientCreerHospitalisation()
        {
            InitializeComponent();

            Control.ControlRechercheTraitement ctrlRechTraitement = new Control.ControlRechercheTraitement();
            Grid.SetColumn(ctrlRechTraitement, 1);
            Grid.SetRow(ctrlRechTraitement, 1);

            grdCreerHospitalisation.Children.Add(ctrlRechTraitement);
        }
    }
}
