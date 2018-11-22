using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VitAdmin.Model;
using VitAdmin.ViewModel;
using VitAdmin.View.Tool;
using VitAdmin.Parameter;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelHub.xaml
    /// </summary>
    public partial class ViewProfessionnelHub : Page
    {
        private ViewModelProfessionnelHub ViewModelProfessionnelHub { get; set; }
        protected GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewProfessionnelHub(GestionnaireEcrans gestionnaireEcrans, Employe employe) 
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;

            Departement departementEmploye = DataModelDepartement.GetDepartementEmploye(employe);

            ViewModelProfessionnelHub = new ViewModelProfessionnelHub(gestionnaireEcrans);
            DataContext = ViewModelProfessionnelHub;

            Control.ControlListePatient ctrlLstPatient =
                new Control.ControlListePatient(
                    gestionnaireEcrans,
                    new ObservableCollection<Departement>(DataModelDepartement.GetDepartements()),
                    new ObservableCollection<Employe>(DataModelEmploye.GetLstEmployesDepartement(departementEmploye)),
                    departementEmploye,
                    employe);

            Grid.SetColumnSpan(ctrlLstPatient, 2);

            grdLstPatient.Children.Add(ctrlLstPatient);
            if(UsagerConnecte.Usager.RoleUsager == Role.InfChef)
                CreerBoutonGestionLit();
        }

       private void CreerBoutonGestionLit()
        {
            Button btnGestionLit = new Button
            {
                Content = "Gestion des lits",
                Command = ViewModelProfessionnelHub.CmdBtnGestionLits,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            Grid.SetColumn(btnGestionLit, 1);
            Grid.SetRow(btnGestionLit, 1);
            grdLstPatient.Children.Add(btnGestionLit);
        }
    }
}
