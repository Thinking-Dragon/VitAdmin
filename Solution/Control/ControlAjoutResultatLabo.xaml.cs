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
    /// Interaction logic for ControlAjoutResultatLabo.xaml
    /// </summary>
    public partial class ControlAjoutResultatLabo : UserControl
    {
        private ControlModelAjoutResultatLabo ControlModelResultatLabo { get; set; }

        private bool EstDeuxiemeClick { get; set; }

        public ControlAjoutResultatLabo(Hospitalisation hospit)
        {
            InitializeComponent();
            DataContext = ControlModelResultatLabo = new ControlModelAjoutResultatLabo(hospit);

        }

        private void Confirmer_Click(object sender, RoutedEventArgs e)
        {
            if (lienImage.Text != "" && nomAnalyse.Text != "")
            {
                if (!EstNomImageValide())
                {
                    (DataContext as ControlModelAjoutResultatLabo).MessageErreur = "L'image n'existe pas";
                }
                else if (EstDeuxiemeClick == true)
                {
                    ControlModelResultatLabo.CmdBtnClicConfirmerResultatLabo.Execute(new ResultatLabo(lienImage.Text, nomAnalyse.Text, (bool)Notifier.IsChecked));
                    DialogHost.CloseDialogCommand.Execute(null, null);
                }
                else
                {
                    (DataContext as ControlModelAjoutResultatLabo).MessageErreur = "Voulez-vous vraiment confirmer?";
                    EstDeuxiemeClick = true;
                }
            }
            else
            {
                (DataContext as ControlModelAjoutResultatLabo).MessageErreur = "Vous devez remplir tous les champs";
            }
            
        }

        private bool EstValide()
        {
            return nomAnalyse.Text.Length > 255;
        }

        private bool EstNomImageValide()
        {
            BitmapImage img = null;
            //420.cstj.qc.ca/2016/1524043/VitAdmin/radio1.jpg
            try
            {
                img = new BitmapImage(new Uri(lienImage.Text));
                return img != null;
            }
            catch (Exception e)
            {
                return false;
            }
            

            
        }
    }
}
