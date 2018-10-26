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
            if (produit.Text != "" && posologie.Text != "" && dateDebut.SelectedDate != null && nbJour.Text != null)
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
                        (DataContext as ControlModelAjoutPrescription).MessageErreur = "Vous n'êtes pas autorisé à ajouter une prescription";
                    }
                }
                else
                {
                    (DataContext as ControlModelAjoutPrescription).MessageErreur = "Voulez-vous vraiment confirmer?";
                    EstDeuxiemeClick = true;
                }
            }
            else if (EstValide())
            {
                (DataContext as ControlModelAjoutPrescription).MessageErreur = "La limite de caractère est atteinte";
            }
            else if(EstDateDebutValide())
            {
                (DataContext as ControlModelAjoutPrescription).MessageErreur = "Une prescription ne doit pas débuter dans le passé";
            }
            else
            {
                (DataContext as ControlModelAjoutPrescription).MessageErreur = "Vous devez remplir tous les champs";

            }
        }

        private bool EstValide()
        {
            return produit.Text.Length < 50 && posologie.Text.Length < 255;
        }

        private bool EstDateDebutValide()
        {
            return (DateTime)dateDebut.SelectedDate >= DateTime.Now;
        }
    }
}
