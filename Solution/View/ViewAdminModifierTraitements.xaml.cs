using MaterialDesignThemes.Wpf;
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
using VitAdmin.Data;
using VitAdmin.Model;
using VitAdmin.View.Tool;
using VitAdmin.ViewModel;

namespace VitAdmin.View
{
    /// <summary>
    /// Logique d'interaction pour ViewAdminModifierTraitements.xaml
    /// </summary>
    public partial class ViewAdminModifierTraitements : Page, IEcranRetour
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewAdminModifierTraitements(GestionnaireEcrans gestionnaireEcrans)
        {
            InitializeComponent();
            DataContext = new ViewModelAdminModifierTraitements(gestionnaireEcrans, DataModelTraitement.GetTraitements(true));
            GestionnaireEcrans = gestionnaireEcrans;
            cpBarreRecherche.Content = new ControlBarreRechercheTraitement((DataContext as ViewModelAdminModifierTraitements).Traitements);
            cpListeTraitementsAvecEtapes.Content = new ControlListeTraitementsAvecEtapes(gestionnaireEcrans, (DataContext as ViewModelAdminModifierTraitements).Traitements);
        }
        

        /*
         * Pour implémenter la logique du bouton de retour dans un écran, il faut que l'écran hérite de IEcranRetour,
         * ce qui vous fera implémenter ces deux propriétés :
         */

        // CmdRetourEcranPrecedent, qui retourne une fonction qui s'exécutera lorsque l'utilisateur cliquera sur le bouton de retour.
        public Action CmdRetourEcranPrecedent
        {
            get { return () => { GestionnaireEcrans.Changer(new ViewHubAdmin(GestionnaireEcrans)); }; }
        }

        // TexteBoutonRetourEcran, qui retourne une chaine de caractères, qui s'affichera sur le bouton.
        public string TexteBoutonRetourEcran
        {
            get { return "< Accueil"; }
        }
    }
}
