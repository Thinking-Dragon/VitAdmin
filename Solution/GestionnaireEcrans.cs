using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VitAdmin.MVVM;
using VitAdmin.View.Tool;

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
        /// Le type de l'écran présentement affiché
        /// </summary>
        private Type TypeEcranPresent { get; set; }
        /// <summary>
        /// Le type de l'écran affiché précédemment
        /// </summary>
        private Type TypeAncienEcran { get; set; }
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
            if (!(ecran.GetType() == TypeEcranPresent))
            {
                if(!(GetEcranPresent() is IRetourEcranListeExclusion ecranPresent &&
                     ecranPresent.ListeExclusionEcransRetour.Exists(type => type == ecran.GetType())))
                {
                    AncienEcran = Frame.Content as Page;
                    TypeAncienEcran = TypeEcranPresent;
                }
                Frame.Content = ecran;
                ActionLorsqueFenetreChange?.Invoke(ecran);
                TypeEcranPresent = ecran.GetType();
            }
        }

        public void RetourAncienEcran()
            => Changer(AncienEcran);

        public Page GetEcranPresent() => Frame.Content as Page;

        public void AfficherMessage(string message, string texteBouton = "Okay", string nomDialogue = "dialogGeneral", Action callback = null)
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.Children.Add(new Label { Content = message, Margin = new Thickness(32) });
            Button buttonClose = new Button { Content = texteBouton };
            buttonClose.Command = new CommandeDeleguee(param =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                callback?.Invoke();
            });
            Grid.SetRow(buttonClose, 1);
            grid.Children.Add(buttonClose);

            DialogHost.Show(grid, nomDialogue);
        }
        
        public void DemanderOuiNon(string message, Action<bool> callback, string nomDialogue = "dialogGeneral")
        {
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            Label lblMessage = new Label { Content = message, Margin = new Thickness(32) };

            Button buttonOui = new Button { Content = "Oui" };
            Button buttonNon = new Button { Content = "Non" };

            buttonOui.Command = new CommandeDeleguee(param =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                callback(true);
            });
            buttonNon.Command = new CommandeDeleguee(param =>
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
                callback(false);
            });

            Grid.SetColumnSpan(lblMessage, 2);
            grid.Children.Add(lblMessage);

            Grid.SetRow(buttonOui, 1);
            grid.Children.Add(buttonOui);

            Grid.SetRow(buttonNon, 1);
            Grid.SetColumn(buttonNon, 1);
            grid.Children.Add(buttonNon);

            DialogHost.Show(grid, nomDialogue);
        }
    }
}
