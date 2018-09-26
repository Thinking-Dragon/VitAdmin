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
    /// Logique d'interaction pour ViewDossierPatientInfos.xaml
    /// </summary>
    public partial class ViewProfessionnelDossierPatientInfosModif : Page
    {
        public ViewProfessionnelDossierPatientInfosModif()
        {
            InitializeComponent();

            grdPatientInfos.Children.Add(new Control.ControlDossierPatientInfos());
        }
    }
}
