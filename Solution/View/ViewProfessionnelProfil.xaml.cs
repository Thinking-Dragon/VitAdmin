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

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelProfil.xaml
    /// </summary>
    public partial class ViewProfessionnelProfil : Page
    {
        public ViewProfessionnelProfil(Employe employe)
        {
            InitializeComponent();
            DataContext = new ViewModelProfessionnelProfil();

            Control.ControlDossierPatientInfos CDPI = new Control.ControlDossierPatientInfos(employe);
            Grid.SetColumn(CDPI, 0);
            Grid.SetRow(CDPI, 0);

            Control.ControlProfessionnelProfil CPP = new Control.ControlProfessionnelProfil(employe);
            Grid.SetColumn(CPP, 1);
            Grid.SetRow(CPP, 0);

            grdViewPro.Children.Add(CDPI);
            grdViewPro.Children.Add(CPP);
        }
    }
}
