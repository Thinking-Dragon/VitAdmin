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
using VitAdmin.Data;
using VitAdmin.View;

namespace VitAdmin
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConnexionBD.Instance().NomBD = "vitadmin-dev"; // Initialiser la connexion à la base de donnée.
            GestionnaireEcrans.Initialiser(grdMain, new ViewConnexion()); // Initialiser le gestionnaire d'écrans.
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            => ConnexionBD.Instance().Fermer();
    }
}
