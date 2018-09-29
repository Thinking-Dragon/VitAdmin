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

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewProfessionnelHub.xaml
    /// </summary>
    public partial class ViewProfessionnelHub : Page
    {
        // TODO: Modifier le paramètres pour qu'il recoit les infos du professionnel qui se connecte
        // Ainsi, le filtre département sera par défaut le département de l'employé ainsi la liste des
        // professionnels sera mis à jour.
        public ViewProfessionnelHub(Departement departement, Employe employe) //Remplacer tous les paramètres par Employe, voir avec Clément
        {
            InitializeComponent();

            Control.ControlListePatient ctrlLstPatient = new Control.ControlListePatient(new ObservableCollection<Citoyen>(DataModelCitoyen.getCitoyensLstPatient()), new ObservableCollection<Departement>(DataModelDepartement.getDepartement()), new ObservableCollection<Employe>(DataModelEmploye.GetEmployesLstPatient(departement)),
                                                                                        departement, employe);

            Grid.SetColumnSpan(ctrlLstPatient, 2);

            grdLstPatient.Children.Add(ctrlLstPatient);
        }
    }
}
