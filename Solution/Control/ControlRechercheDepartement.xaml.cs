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
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlRechercheDepartement.xaml
    /// </summary>
    public partial class ControlRechercheDepartement : UserControl
    {
        public ControlModelRechercheDepartement ControlModel { get; set; }

        public ControlRechercheDepartement(Departement departement)
        {
            InitializeComponent();
            DataContext = ControlModel = new ControlModelRechercheDepartement(departement);
        }
    }
}
