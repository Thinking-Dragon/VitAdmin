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
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelProfil.xaml
    /// </summary>
    public partial class ViewProfessionnelProfil : Page, IEcranRetour
    {
        GestionnaireEcrans GestEcran { get; set; }
        //GestionnaireEcrans previousView { get; set; }
        Employe Employe_ { get; set; }
        public ViewProfessionnelProfil(GestionnaireEcrans gestionnaireEcrans, Employe employe = null)
        {
            InitializeComponent();
            DataContext = new ViewModelProfessionnelProfil();
            Employe_ = employe;
            GestEcran = gestionnaireEcrans;

            // Configure le control affichant les infos de la partie employé de l'employé
            Control.ControlDossierPatientInfos CDPI = new Control.ControlDossierPatientInfos(employe);
            Grid.SetColumn(CDPI, 0);
            Grid.SetRow(CDPI, 0);
            Grid.SetRowSpan(CDPI,7);

            // Configure le control affichant les infos de la partie citoyen de l'employé
            Control.ControlProfessionnelProfil CPP = new Control.ControlProfessionnelProfil(employe);
            Grid.SetColumn(CPP, 1);
            Grid.SetRow(CPP, 4);
            Grid.SetRowSpan(CPP, 3);

            grdViewCit.Children.Add(CDPI);
            grdViewCit.Children.Add(CPP);
        }

        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcran.RetourAncienEcran(); }; }
        }
        public string TexteBoutonRetourEcran => "< Retour";
    }
}
