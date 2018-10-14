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

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlNotifications.xaml
    /// </summary>
    public partial class ControlNotifications : UserControl
    {
        private ControlModelNotifications ControlModel { get; set; }
        public ControlNotifications()
        {
            InitializeComponent();
            DataContext = ControlModel = new ControlModelNotifications();
        }

        private void dtgNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => ControlModel.CmdOuvrirNotification.Execute(null);
    }
}
