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
using VitAdmin.ControlModel.DemandesTransfert;


namespace VitAdmin.Control.DemandesTransfert
{
    /// <summary>
    /// Logique d'interaction pour test.xaml
    /// </summary>
    public partial class ControlListeLits : UserControl
    {
        ControlModelInfos ControlModelInfos { get; set; }
        public ControlListeLits(Citoyen citoyen)
        {
            InitializeComponent();
            DataContext = ControlModelInfos = new ControlModelInfos(citoyen);

            /*InitialiserTxtNomPrenomPatient();
            InitialiserCboEtatLit();*/

        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {

        }

        /*private void InitialiserCboEtatLit()
        {
            Label lblEtatLit = new Label { Content = "État du lit" };
            ComboBox cboEtatLit = new ComboBox
            {
                ItemsSource = Enum.GetValues(typeof(EtatLit)).Cast<EtatLit>(),
                SelectedItem = ControlModelInfos.Citoyen.Lit.EtatLit
            };




        }

        private void InitialiserTxtNomPrenomPatient()
        {
            Label lblNomPrenomPatient = new Label { Content = "Nom et prénom" };
            TextBox txtNomPrenom = new TextBox { Text = ControlModelInfos.Citoyen.NomComplet };


        }*/
    }
}
