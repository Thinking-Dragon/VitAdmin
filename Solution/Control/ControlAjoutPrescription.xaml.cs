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
using VitAdmin.ControlModel;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlAjoutPrescription.xaml
    /// </summary>
    public partial class ControlAjoutPrescription : UserControl
    {
        private ControlModelAjoutPrescription ControlModelPrescription { get; set; }

        private bool EstDeuxiemeClick { get; set; }

        public ControlAjoutPrescription(Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = ControlModelPrescription = new ControlModelAjoutPrescription(hospit);
        }

        private void Confirmer_Click(object sender, RoutedEventArgs e)
        {
            if (EstDeuxiemeClick == true)
            {
                if (Parameter.UsagerConnecte.Usager.Poste == "médecin" || Parameter.UsagerConnecte.Usager.Poste == "infirmière" || Parameter.UsagerConnecte.Usager.Poste == "admin")
                {
                    ControlModelPrescription.CmdBtnClicConfirmerPrescription.Execute(new Prescription(produit.Text, posologie.Text, new DateTime(dateDebut.SelectedDate.Value.Year, dateDebut.SelectedDate.Value.Month, dateDebut.SelectedDate.Value.Day), (int)nbJour.Value, (bool)Notifier.IsChecked));
                    DialogHost.CloseDialogCommand.Execute(null, null);

                }
                else
                {
                    //TODO
                }
            }
            else
            {
                (DataContext as ControlModelAjoutPrescription).MessageErreur = "Voulez-vous vraiment confirmer?";
                EstDeuxiemeClick = true;
            }

        }
    }
}
