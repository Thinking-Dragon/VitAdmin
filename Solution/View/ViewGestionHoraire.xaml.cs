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
using VitAdmin.Control;
using VitAdmin.View.Tool;
using VitAdmin.ControlModel;
using MaterialDesignThemes.Wpf;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewGestionHoraire.xaml
    /// </summary>
    public partial class ViewGestionHoraire : Page, IEcranRetour, IEcranAAideContextuelle
    {
        private GestionnaireEcrans GestEcrans { get; set; }
        private Employe Employe { get; set; }
        public ViewGestionHoraire(Employe employe, GestionnaireEcrans gest)
        {
            InitializeComponent();
            Employe = employe;
            Content = new ControlGestionHoraire(Employe);
            GestEcrans = gest;
        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get
            {
                ControlEnregistrerHoraire test = new ControlEnregistrerHoraire((Content as ControlGestionHoraire).GrdHoraire , Employe, () => { ControlGestionHoraire.aujourdhui = DateTime.Now;  });
                DialogHost.Show(test, "dialogGeneral:modal=false");

                return () => { GestEcrans.Changer(new ViewListeEmployes(GestEcrans)); };
            }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Liste d'employés"; }
        }

        public string AncreSectionAideContextuelle => "MPhoraireVetM";
    }
}
