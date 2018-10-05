﻿using System;
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
using VitAdmin.Data;
using VitAdmin.View;

namespace VitAdmin
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ConnexionBD.Instance().NomBD = "vitadmin_bd_main"; // Initialiser la connexion à la base de donnée.
            GestionnaireEcrans = new GestionnaireEcrans(grdMain); // Initialiser le gestionnaire d'écrans.
<<<<<<< HEAD
            //GestionnaireEcrans.Changer(new ViewConnexion(GestionnaireEcrans));
            GestionnaireEcrans.Changer(new ViewSuperEcran(GestionnaireEcrans, new ViewProfessionnelHub(GestionnaireEcrans, new Model.Departement { Nom = "Chirurgie" }, new Model.Employe { Nom = "Therien", Prenom = "Jacques", NumEmploye = "123456THJ"})));
=======
            GestionnaireEcrans.Changer(new ViewConnexion(GestionnaireEcrans));
            //GestionnaireEcrans.Changer(new ViewSuperEcran(GestionnaireEcrans, new ViewProfessionnelHub(GestionnaireEcrans, new Model.Departement { Nom = "Chirurgie" }, new Model.Employe { Nom = "Therien", Prenom = "Jacques", NumEmploye = "123456THJ"})));
>>>>>>> 8b244c47607c5d4e74d88c22f76c4bde093d9c56
            //GestionnaireEcrans.Changer(new ViewPatientHospitalisation(GestionnaireEcrans, new Model.Citoyen("tous059615"), new Model.Hospitalisation() ));
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
            => ConnexionBD.Instance().Fermer();
    }
}
