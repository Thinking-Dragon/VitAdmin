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
using VitAdmin.ControlModel;
using VitAdmin.Model;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListePatient.xaml
    /// </summary>
    public partial class ControlListePatient : UserControl
    {
        public ControlListePatient(ObservableCollection<Citoyen> citoyens, ObservableCollection<Departement> departements, ObservableCollection<Employe> employes, Departement departement, Employe employe)
        {
            InitializeComponent();

            // On met dans le datacontexte les infos qui seront liées dans le UserControl
            DataContext = new ControlModelListePatient(citoyens, departements, employes);

            // Permet de sélectionner par défaut le département du professionnel dans la combobox
            Departement deptRecherche = new Departement();
            foreach (Departement dep in departements)
            {
                if (dep.Nom == departement.Nom)
                    deptRecherche = dep;
            }

            ComboBox cboDepartements = new ComboBox
            {   ItemsSource = departements,
                DisplayMemberPath = "Nom",
                SelectedItem = departements[departements.IndexOf(deptRecherche)],                                 
            };

            cboDepartements.SelectionChanged += cboProfessionnel_SelectionChanged;
           
            stpnlFiltres.Children.Add(cboDepartements);
            

            // Ensuite, il faut afficher dans le cboProfessionnel le professionnel par défaut
            Employe empRecherche = new Employe();
            foreach (Employe emp in employes)
            {
                if (emp.Nom == employe.Nom)
                    empRecherche = emp;
            }

            ComboBox cboProfessionnel = new ComboBox
            {
                ItemsSource = employes,
                DisplayMemberPath = "idPrenomNom",
                SelectedItem = employes[employes.IndexOf(empRecherche)]
            };

            cboProfessionnel.SelectionChanged += cboProfessionnel_SelectionChanged;

            stpnlFiltres.Children.Add(cboProfessionnel);
        }

        // Enlève le placeholder lorsqu'il y a focus sur le txtbox
        private void txtRecherche_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRecherche.Text = "";
        }

        // Rajoute le placeholder lorsqu'il n'y a plus de texte et défocuse.
        private void txtRecherche_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRecherche.Text))
                txtRecherche.Text = "Recherche";
        }

        private void cboProfessionnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cboDepartements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*ObservableCollection<Employe> employes = new ObservableCollection<Employe>(Data.DataModelEmploye.GetEmployesLstPatient((Departement)cboProfessionnel.SelectedItem));
            cboProfessionnel.ItemsSource = employes;*/
        }
    }
}
