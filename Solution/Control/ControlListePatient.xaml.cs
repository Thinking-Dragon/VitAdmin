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
using VitAdmin.View;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListePatient.xaml
    /// </summary>
    public partial class ControlListePatient : UserControl
    {
        ComboBox cboProfessionnel = new ComboBox();
        ComboBox cboDepartements = new ComboBox();

        public ControlListePatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Citoyen> citoyens, ObservableCollection<Departement> departements, ObservableCollection<Employe> employes, Departement departement, Employe employe)
        {
            InitializeComponent();

            // On met dans le datacontexte les infos qui seront liées dans le UserControl
            DataContext = new ControlModelListePatient(gestionnaireEcrans, citoyens, departements, employes);

            // Permet de sélectionner par défaut le département du professionnel dans la combobox
            // Je dois créer mes combobox avant de les mettre dans mon stackpanel puisque l'event selectedchange 
            // s'enclenchait au démarrage et fait planter l'application à cause de mon système par défaut.
            initialiserCboDepartement(departements, departement);

            // Ensuite, il faut afficher dans le cboProfessionnel le professionnel par défaut
            initialiserCboProfessionnel(employes, employe);



            
        }

        private void barreRecherche()
        {
            var viewDtgPatients = CollectionViewSource.GetDefaultView(dtgPatient);

            viewDtgPatients.Filter = delegate (object o)
            {
                if (o.ToString().Contains(txtRecherche.Text))
                {
                    return true;
                }
                return false;
            };

            dtgPatient.ItemsSource = viewDtgPatients;
        }

        private void initialiserCboDepartement(ObservableCollection<Departement> departements, Departement departement)
        {           
            Departement deptRecherche = new Departement();
            foreach (Departement dep in departements)
            {
                if (dep.Nom == departement.Nom)
                    deptRecherche = dep;
            }

            cboDepartements.ItemsSource = departements;
            cboDepartements.DisplayMemberPath = "Nom";
            cboDepartements.SelectedItem = departements[departements.IndexOf(deptRecherche)];
            cboDepartements.SelectionChanged += CboDepartements_SelectionChanged;

            stpnlFiltres.Children.Add(cboDepartements);

        }

        private void initialiserCboProfessionnel(ObservableCollection<Employe> employes, Employe employe)
        {
            Employe empRecherche = new Employe();
            foreach (Employe emp in employes)
            {
                if (emp.Nom == employe.Nom)
                    empRecherche = emp;
            }

            employes.Add(new Employe { Nom = "Tous" });

            cboProfessionnel.ItemsSource = employes;
            cboProfessionnel.DisplayMemberPath = "idPrenomNom";
            cboProfessionnel.SelectedItem = employes[employes.IndexOf(empRecherche)];
            cboProfessionnel.SelectionChanged += CboProfessionnel_SelectionChanged;

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

        private void CboProfessionnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Plante avec un null à employe et fait des doublons...
            // Normal, il n'y a pas de lien entre un patient et un employé! Voir note 30/09/2018
            /*ObservableCollection<Citoyen> citoyens = new ObservableCollection<Citoyen>(Data.DataModelCitoyen.getCitoyensLstPatient((Employe)cboProfessionnel.SelectedItem));
            cboProfessionnel.ItemsSource = citoyens;*/
        }

        private void CboDepartements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<Employe> employes = new ObservableCollection<Employe>(Data.DataModelEmploye.GetEmployesLstPatient((Departement)cboDepartements.SelectedItem));
            cboProfessionnel.ItemsSource = employes;
        }

        private void txtRecherche_KeyDown(object sender, KeyEventArgs e)
        {
            /*var viewDtgPatients = CollectionViewSource.GetDefaultView(dtgPatient);

            viewDtgPatients.Filter = delegate (object o)
            {
                if (o.ToString().Contains(txtRecherche.Text))
                {
                    return true;
                }
                return false;
            };

            dtgPatient.ItemsSource = viewDtgPatients;*/
        }
     
    }

}
