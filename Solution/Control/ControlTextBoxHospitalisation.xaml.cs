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
using VitAdmin.View;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlContexte.xaml
    /// </summary>
    public partial class ControlTextBoxHospitalisation : UserControl
    {
        public placeHolder placeHolder { get; set; }

        public ControlTextBoxHospitalisation(string nomLabel, Hospitalisation hospitalisation)
        {
            InitializeComponent();

            
            DataContext = new ControlModelTextBoxHospitalisation(nomLabel, hospitalisation);
            placeHolder = new placeHolder("Ajouter le contexte ici");
            (DataContext as ControlModelTextBoxHospitalisation).Hospitalisation.Contexte = placeHolder.Texte;
            Loaded += Focus_OnLoaded;

            txtContext.GotFocus += placeHolder.EnleverTexte;
            txtContext.LostFocus += placeHolder.AjouterTexte;
            
        }

        private void Focus_OnLoaded(object sender, RoutedEventArgs e)
        {
            txtContext.Focus();
        }
    }
}
