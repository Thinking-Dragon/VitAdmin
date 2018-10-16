using MaterialDesignThemes.Wpf;
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
    /// Logique d'interaction pour QuelPosteOccupesTuAdmin.xaml
    /// </summary>
    public partial class QuelPosteOccupesTuAdmin : UserControl
    {
        private Action Action { get; set; }
        public QuelPosteOccupesTuAdmin(Action action)
        {
            InitializeComponent();
            Action = action;
        }

        private void estInf_Click(object sender, RoutedEventArgs e)
        {
            Parameter.UsagerConnecte.Usager.Poste = estInf.Content.ToString();
            DialogHost.CloseDialogCommand.Execute(null, null);
            Action();
        }

        private void estMed_Click(object sender, RoutedEventArgs e)
        {
            Parameter.UsagerConnecte.Usager.Poste = estMed.Content.ToString();
            DialogHost.CloseDialogCommand.Execute(null, null);
            Action();
        }
    }
}
