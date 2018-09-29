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

            cboDepartements.SelectedItem = departements[departements.IndexOf(deptRecherche)];

            // Ensuite, il faut afficher dans le cboProfessionnel les professionnels assignés au département
            

            
            


            
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
    }
}
