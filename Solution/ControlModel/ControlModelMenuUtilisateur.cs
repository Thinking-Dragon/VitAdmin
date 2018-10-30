using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using VitAdmin.MVVM;
using VitAdmin.Parameter;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
    class ControlModelMenuUtilisateur : ObjetObservable
    {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
        GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        // Flag du bouton pour horaire
        private bool IsBtnHoraireEnabledPrivate { get; set; }
        public bool IsBtnHoraireEnabled
        {
            get
            {
                return IsBtnHoraireEnabledPrivate;
            }
            set
            {
                IsBtnHoraireEnabledPrivate = value;
                RaisePropertyChangedEvent("IsBtnHoraireEnabled");
            }
        }

        // Flag du bouton pour profil
        private bool IsBtnProfilEnabledPrivate { get; set; }
        public bool IsBtnProfilEnabled
        {
            get
            {
                return IsBtnProfilEnabledPrivate;
            }
            set
            {
                IsBtnProfilEnabledPrivate = value;
                RaisePropertyChangedEvent("IsBtnProfilEnabled");
            }
        }

        // Couleur border horaire
        private Color PrivateFillBtnHoraire { get; set; }
        public Color FillBtnHoraire
        {
            get
            {
                return PrivateFillBtnHoraire;
            }
            set
            {
                PrivateFillBtnHoraire = value;
                RaisePropertyChangedEvent("FillBtnHoraire");
            }
        }

        // Couleur border profil
        private Color PrivateFillBtnProfil { get; set; }
        public Color FillBtnProfil
        {
            get
            {
                return PrivateFillBtnProfil;
            }
            set
            {
                PrivateFillBtnProfil = value;
                RaisePropertyChangedEvent("FillBtnProfil");
            }
        }

        // Modification apparence btn sélectionné
        public ControlModelMenuUtilisateur(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;

            // Bouton horaire
            if (!(GestionnaireEcrans.GetEcranPresent() is ViewProfessionnelHoraire))
            {
                IsBtnHoraireEnabled = true;
                FillBtnHoraire = Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                FillBtnHoraire = Color.FromArgb(50, 0, 0, 0);
                IsBtnHoraireEnabled = false;
            }

            // Bouton Profil
            if (GestionnaireEcrans.GetEcranPresent() is ViewProfessionnelProfil)
            {
                IsBtnProfilEnabled = false;
                FillBtnProfil = Color.FromArgb(50, 0, 0, 0);
            }
            else
            {
                IsBtnProfilEnabled = true;
                FillBtnProfil = Color.FromArgb(0, 255, 255, 255);
            }


        }

        // Icommand pour changer view vers horaire
        public ICommand CmdAfficheHoraire
        {
            get
            {
                return new CommandeDeleguee(
                   param =>
                   {
                       GestionnaireSousEcrans.Changer(new ViewProfessionnelHoraire(GestionnaireSousEcrans));
                       DialogHost.CloseDialogCommand.Execute(null, null);
                   }
          );
            }
        }

        // ICommand pour changer view vers profil
        public ICommand CmdModifierProfil
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        GestionnaireSousEcrans.Changer(new ViewProfessionnelProfil(GestionnaireSousEcrans, UsagerConnecte.Usager));
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                );
            }
        }

        public ICommand CmdLogOut
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        UsagerConnecte.Deconnecter();
                        Notifications.GestionnaireNotifications.DetruireInstance();
                        GestionnaireEcrans.Changer(new ViewConnexion(GestionnaireEcrans));
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                );
            }
        }
    }
}
