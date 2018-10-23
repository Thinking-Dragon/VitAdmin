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
using VitAdmin.Control;
using VitAdmin.View.Tool;

namespace VitAdmin.View
{
    /// <summary>
    /// Interaction logic for ViewProfessionnelHoraire.xaml
    /// </summary>
    public partial class ViewProfessionnelHoraire : Page, IEcranRetour
    {
        GestionnaireEcrans GestEcrans { get; set; }
        public ViewProfessionnelHoraire(GestionnaireEcrans gestEcrans)
        {
            InitializeComponent();
            GestEcrans = gestEcrans;
            Content = new ControlTableauHoraire();
        }

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestEcrans.RetourAncienEcran(); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Retour"; }
        }

    }
}
