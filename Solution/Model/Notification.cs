using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VitAdmin.Data;
using VitAdmin.View;

namespace VitAdmin.Model
{
    public class Notification
    {
        public string Message { get; set; }
        public DateTime TempsReception { get; set; }
        public bool EstLu { get; set; }
        public LienNotificationEcran LienNotificationEcran { get; set; }

        public void Voir(GestionnaireEcrans gestionnaireEcrans)
        {
            bool erreur = true;

            Grid conteneurPage = new Grid { Width = 1000, Height = 600 };

            conteneurPage.ColumnDefinitions.Add(new ColumnDefinition());
            conteneurPage.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(64) });
            conteneurPage.RowDefinitions.Add(new RowDefinition { Height = new GridLength(32) });
            conteneurPage.RowDefinitions.Add(new RowDefinition());

            Button btnFermer = new Button
            {
                Content = 'X',
                Foreground = Brushes.Black,
                Background = Brushes.White,
                Command = DialogHost.CloseDialogCommand
            };
            Grid.SetColumn(btnFermer, 1);

            conteneurPage.Children.Add(btnFermer);

            Grid contextePage = new Grid { Margin = new Thickness(16) };
            Grid.SetColumnSpan(contextePage, 2);
            Grid.SetRow(contextePage, 1);

            conteneurPage.Children.Add(contextePage);

            GestionnaireEcrans gestionnaireEcransDialog = new GestionnaireEcrans(contextePage);

            if (LienNotificationEcran.TypeEcran == typeof(ViewProfessionnelDossierPatient))
            {
                erreur = false;
                gestionnaireEcransDialog.Changer(new ViewProfessionnelDossierPatient(
                    gestionnaireEcransDialog,
                    DataModelCitoyen.GetUnCitoyen(
                        new Citoyen { AssMaladie = LienNotificationEcran.Parametres["AssuranceMaladiePatient"] as string }
                    )
                ));
            }
            else if(LienNotificationEcran.TypeEcran == typeof(ViewMessageNotification))
            {
                erreur = false;
                gestionnaireEcransDialog.Changer(
                    new ViewMessageNotification(
                        LienNotificationEcran.Parametres["Titre"] as string,
                        LienNotificationEcran.Parametres["Message"] as string
                    )
                );
            }

            if(!erreur)
                DialogHost.Show(conteneurPage, "dialogGeneral");
            else
                gestionnaireEcrans.AfficherMessage("Une erreur est survenue lors de l'ouverture de la notification");

        }
    }
}
