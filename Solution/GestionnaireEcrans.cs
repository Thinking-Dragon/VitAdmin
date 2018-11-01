using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VitAdmin.MVVM;

namespace VitAdmin
{
    /// <summary>
    /// Gestionnaire des écrans de l'application: affiche des pages dans une frame de façon dynamique (on peut changer la page)
    public class GestionnaireEcrans
    {
        /// <summary>
        /// Conteneur de la page présente
        /// </summary>
        private FrameSansNavigation Frame { get; set; }
        /// <summary>
        /// Callback qui est appelé lorsque la fenêtre change s'il est défini
        /// </summary>
        private Action<Page> ActionLorsqueFenetreChange { get; set; } = null;
        /// <summary>
        /// L'écran qui a été utilisé en dernier
        /// </summary>
        private Page AncienEcran { get; set; } = null;

        public GestionnaireEcrans(Panel parent)
        {
            Frame = new FrameSansNavigation();
            parent.Children.Add(Frame);
        }

        public GestionnaireEcrans(Panel parent, Action<Page> actionLorsqueFenetreChange) : this(parent)
            => ActionLorsqueFenetreChange = actionLorsqueFenetreChange;

        public GestionnaireEcrans(Panel parent, Page premierEcran) : this(parent)
            => Changer(premierEcran);

        public GestionnaireEcrans(Panel parent, Action<Page> actionLorsqueFenetreChange, Page premierEcran) : this(parent, actionLorsqueFenetreChange)
            => Changer(premierEcran);

        public void Changer(Page ecran)
        {
            AncienEcran = Frame.Content as Page;
            Frame.Content = ecran;
            ActionLorsqueFenetreChange?.Invoke(ecran);
        }

        public void RetourAncienEcran()
            => Changer(AncienEcran);

        public Page GetEcranPresent() => (Frame.Content != null ? Frame.Content as Page : null);

        public void AfficherMessage(string message, string texteBouton = "Okay")
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.Children.Add(new Label { Content = message, Margin = new Thickness(32) });
            Button buttonClose = new Button { Content = texteBouton };
            buttonClose.Command = DialogHost.CloseDialogCommand;
            Grid.SetRow(buttonClose, 1);
            grid.Children.Add(buttonClose);

            DialogHost.Show(grid, "dialogGeneral");
        }
    }
}
