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
using VitAdmin.ControlModel;
using VitAdmin.Data;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlDossierPatientInfos.xaml
    /// </summary>
    /// 
    public partial class ControlDossierPatientInfos : UserControl
    {
        ComboBox CboGenre { get; set; }
        ControlModelDossierPatientInfos controlModelDossierPatientInfos;

        public ControlDossierPatientInfos(Citoyen citoyen)
        {
            InitializeComponent();
            /*if (dtpkrNaissance.SelectedDate == null)
                dtpkrNaissance.SelectedDate = DtReference;*/
          

            controlModelDossierPatientInfos = new ControlModelDossierPatientInfos(citoyen);

            DataContext = controlModelDossierPatientInfos;

            InitialiserCboGenre();
        }

        private void InitialiserCboGenre()
        {

            CboGenre = new ComboBox
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 100,
                ItemsSource = Enum.GetValues(typeof(Genre)).Cast<Genre>(),
                SelectedItem = controlModelDossierPatientInfos.Citoyen == null ? Genre.autre : controlModelDossierPatientInfos.Citoyen.Genre
            };

            CboGenre.SelectionChanged += CboGenre_SelectionChanged;

            Grid.SetColumn(CboGenre, 1);
            Grid.SetRow(CboGenre, 2);

            grdInfosPatient.Children.Add(CboGenre);
        }

        public void CboGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            controlModelDossierPatientInfos.Citoyen.Genre = (Genre)CboGenre.SelectedItem;
        }
    }
}
