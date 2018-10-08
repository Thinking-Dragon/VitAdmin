using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        List<Citoyen> LstCitoyenRecherche;

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
            cboRecherche.Text = "";
        }

        // Rajoute le placeholder lorsqu'il n'y a plus de texte et défocuse.
        private void txtRecherche_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboRecherche.Text))
                cboRecherche.Text = "Recherche";
        }

        private void CboProfessionnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControlModelListePatient controlModelListePatient = (ControlModelListePatient)DataContext;

            // la fonction clear dans la fonction CboDepartements_SelectionChanged déclenche un selectionChanged 
            // dans le cboProfessionnel lorsque son contenu est vide. Donc, il fait planté la requête ici.
            if (controlModelListePatient.Employes.Count != 0)
            {
                List<Citoyen> lstPatients = new List<Citoyen>();
                Employe employeSelectionne = (Employe)cboProfessionnel.SelectedItem;

                if (employeSelectionne.Nom == "Tous")
                    lstPatients = Data.DataModelCitoyen.GetTousCitoyensDepartement((Departement)cboDepartements.SelectedItem);                   
                else
                    lstPatients = Data.DataModelCitoyen.GetCitoyensLstPatient(employeSelectionne);

                controlModelListePatient.Citoyens.Clear();
                lstPatients.ForEach(patient => controlModelListePatient.Citoyens.Add(patient));
                // Pour la barre de recherche
                LstCitoyenRecherche = lstPatients;
            }
            
        }

        // À modifier pour le rendre en ICommand dans ControlModel?
        private void CboDepartements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On va chercher le contenu du DataContext pour modifier directement les valeurs
            ControlModelListePatient controlModelListePatient = (ControlModelListePatient)DataContext;

            // On met temporairement la nouvelle liste d'employés du département sélectionné dans une liste
            List<Employe> lstEmploye = Data.DataModelEmploye.GetLstEmployesDepartement((Departement)cboDepartements.SelectedItem);
            // On vide la liste dans le datacontext
            controlModelListePatient.Employes.Clear();
            // On ajoute les nouveaux employés dans le datacontext
            lstEmploye.Add(new Employe { Nom = "Tous" });
            // On met le critère Tous lorsque le département est changé.
            cboProfessionnel.SelectedItem = lstEmploye.Find((employe) => employe.Nom == "Tous");
            lstEmploye.ForEach(employe => controlModelListePatient.Employes.Add(employe));
        }

        private void CboRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            ControlModelListePatient controlModelListePatient = (ControlModelListePatient)DataContext;
            // Si la barre de recherche est vide, on remet tous les patients. Sinon, on vide la liste pour préparer la recherche.
            if (cboRecherche.Text == "")
                LstCitoyenRecherche.ForEach((patient) => controlModelListePatient.Citoyens.Add(patient));


            // On ajoute dans une liste temporaire les patients trouvés dans la liste des patients.
            List<Citoyen> LstCitoyenTrouve = LstCitoyenRecherche.FindAll((patient) => patient.Nom.IndexOf(cboRecherche.Text) != -1);

            controlModelListePatient.Citoyens.Clear();

            // Finalement, on copie tous les patients trouvés dans la liste installée dans le DataContext.
            LstCitoyenTrouve.ForEach((patient) => controlModelListePatient.Citoyens.Add(patient));
        }

    }

}
