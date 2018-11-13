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
using VitAdmin.Parameter;
using VitAdmin.Data;

namespace VitAdmin.Control
{
    /// <summary>
    /// Logique d'interaction pour ControlListePatient.xaml
    /// </summary>
    public partial class ControlListePatient : UserControl
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        ComboBox cboProfessionnel = new ComboBox();
        ComboBox cboDepartements = new ComboBox();
        List<Citoyen> LstCitoyenRecherche;

        public ControlListePatient(GestionnaireEcrans gestionnaireEcrans, ObservableCollection<Departement> departements, ObservableCollection<Employe> employes, Departement departement, Employe employe)
        {
            InitializeComponent();
            GestionnaireEcrans = gestionnaireEcrans;
            ControlModelListePatient controlModelListePatient = new ControlModelListePatient(gestionnaireEcrans, departement.EstNull() ? new ObservableCollection<Citoyen>(DataModelCitoyen.GetCitoyens()) : new ObservableCollection<Citoyen>(DataModelCitoyen.GetCitoyensLstPatient(employe)), departements, employes);

            // On met dans le datacontexte les infos qui seront liées dans le UserControl
            DataContext = controlModelListePatient;

            // Permet de sélectionner par défaut le département du professionnel dans la combobox
            // Je dois créer mes combobox avant de les mettre dans mon stackpanel puisque l'event selectedchange 
            // s'enclenchait au démarrage et fait planter l'application à cause de mon système par défaut.
            initialiserCboDepartement(departements, departement.EstNull() ? new Departement { Nom = "Tous"} : departement);

            // Ensuite, il faut afficher dans le cboProfessionnel le professionnel par défaut
            initialiserCboProfessionnel(employes, employe, departement);

            // Pour la barre de recherche
            LstCitoyenRecherche = controlModelListePatient.Citoyens.ToList<Citoyen>();


        }

        private void initialiserCboDepartement(ObservableCollection<Departement> departements, Departement departement)
        {           
            Departement deptRecherche = new Departement();
            departements.Add(new Departement { Nom = "Tous" });
            foreach (Departement dep in departements)
            {
                if (dep.Nom == departement.Nom)
                    deptRecherche = dep;
            }

            cboDepartements.ItemsSource = departements;
            cboDepartements.DisplayMemberPath = "Nom";
            // Au cas qu'un admin se connecte, aucun département lui est associé, donc il faut enlever la fonction par défaut des filtres.
            //cboDepartements.SelectedItem = UsagerConnecte.Usager.NomUtilisateur == "admin" ? departements[0] : departements[departements.IndexOf(deptRecherche)];
            cboDepartements.SelectedItem = departements[departements.IndexOf(deptRecherche)];

            cboDepartements.SelectionChanged += CboDepartements_SelectionChanged;

            Label lblDepartement = new Label { Content = "Par département" };

            stpnlFiltres.Children.Add(lblDepartement);
            stpnlFiltres.Children.Add(cboDepartements);

        }

        private void initialiserCboProfessionnel(ObservableCollection<Employe> employes, Employe employe, Departement departement)
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
            // Au cas qu'un utilisateur se connecte et qu'il est associé à aucun département, il faut enlever la fonction par défaut des filtres.
            cboProfessionnel.SelectedItem = departement.EstNull() ? employes[0] : employes[employes.IndexOf(empRecherche)];
            cboProfessionnel.SelectionChanged += CboProfessionnel_SelectionChanged;
            cboProfessionnel.Width = stpnlFiltres.Width - 10;

            Label lblEmploye = new Label { Content = "Par professionnel" };

            stpnlFiltres.Children.Add(lblEmploye);
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

        /// <summary>
        /// C'est cette fonction qui effectue une requête à la bd pour prendre les citoyens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboProfessionnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControlModelListePatient controlModelListePatient = (ControlModelListePatient)DataContext;

            // la fonction clear dans la fonction CboDepartements_SelectionChanged déclenche un selectionChanged 
            // dans le cboProfessionnel lorsque son contenu est vide. Donc, il fait planté la requête ici.
            if (controlModelListePatient.Employes.Count != 0)
            {
                List<Citoyen> lstPatients = new List<Citoyen>();
                Employe employeSelectionne = (Employe)cboProfessionnel.SelectedItem;
                Departement departementSelectionne = (Departement)cboDepartements.SelectedItem;

                if (employeSelectionne.Nom == "Tous" && departementSelectionne.Nom == "Tous")
                    lstPatients = Data.DataModelCitoyen.GetCitoyens();
                else if (employeSelectionne.Nom == "Tous")
                    lstPatients = Data.DataModelCitoyen.GetTousCitoyensDepartement((Departement)cboDepartements.SelectedItem);
                else
                    lstPatients = Data.DataModelCitoyen.GetCitoyensLstPatient(employeSelectionne);


                controlModelListePatient.Citoyens.Clear();
                lstPatients.ForEach(patient => controlModelListePatient.Citoyens.Add(patient));
                // Pour la barre de recherche
                LstCitoyenRecherche = lstPatients;
            }
            
        }

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
            string texteRecherche = cboRecherche.Text;

            if (e.Key == Key.Enter)
            {
                // On ajoute dans une liste temporaire les patients trouvés dans la liste des patients.
                List<Citoyen> LstCitoyenTrouve = LstCitoyenRecherche.FindAll((patient) => patient.Nom.IndexOf(cboRecherche.Text) != -1);

                // On vide la liste bindée avec la combobox
                controlModelListePatient.Citoyens.Clear();

                // On doit remettre le texte dans le combobox, car le clear précédent détruit le texte inscrit par l'utilisateur
                cboRecherche.Text = texteRecherche;

                // Finalement, on copie tous les patients trouvés dans la liste installée dans le DataContext.
                LstCitoyenTrouve.ForEach((patient) => controlModelListePatient.Citoyens.Add(patient));
            }
            else if (cboRecherche.Text == "") // Si la barre de recherche est vide, on remet tous les patients. Sinon, on vide la liste pour préparer la recherche.
            {
                // On vide la liste bindée avec la combobox
                controlModelListePatient.Citoyens.Clear();
                LstCitoyenRecherche.ForEach((patient) => controlModelListePatient.Citoyens.Add(patient));
            }
                


        }

        private void DG_Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            GestionnaireEcrans.Changer(new ViewDemandesTransfert((Citoyen)dtgPatient.SelectedItem));
            
        }
    }

}
