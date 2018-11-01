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
    /// Logique d'interaction pour ControlProfessionnelProfil.xaml
    /// </summary>
    public partial class ControlProfessionnelProfil : UserControl
    {
        ControlModelProfessionnelProfil CMPP;
        // Dépendamment de si le Control reçoit un employé ou non, il modifiera ou créera un employé
        // On peut choisir spécifiquement quel champ sera modifiable
        public ControlProfessionnelProfil(Employe employe = null, bool isPosteActive = false, bool isNASActive = false, bool isNumEmployeActive = false, bool isNumPermisActive = false)
        {
            InitializeComponent();

            // Prévient la modification involontaire des champs.
            txtPosteEmploye.IsEnabled = isPosteActive;
            txtNASEmploye.IsEnabled = isNASActive;
            txtNumEmploye.IsEnabled = isNumEmployeActive;
            txtNumPermisEmploye.IsEnabled = isNumPermisActive;

            DataContext = CMPP = new ControlModelProfessionnelProfil(employe);
        }
        
    }
}
