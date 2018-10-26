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
        private Color FillBtnHoraire { get; set; }
        public Color fillBtnHoraire
        {
            get
            {
                return FillBtnHoraire;
            }
            set
            {
                FillBtnHoraire = value;
                RaisePropertyChangedEvent("FillBtnHoraire");
            }
        }

        // Couleur border profil
        private Color FillBtnProfil { get; set; }
        public Color fillBtnProfil
        {
            get
            {
                return FillBtnHoraire;
            }
            set
            {
                FillBtnProfil = value;
                RaisePropertyChangedEvent("FillBtnProfil");
            }
        }

        // Modification apparence btn sélectionné
        public ControlModelMenuUtilisateur(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;

            // Bouton horaire
            if (!(GestionnaireEcrans.GetEcranPresent() is ViewProfessionnelHoraire))
            {
                IsBtnHoraireEnabled = true;
                fillBtnHoraire = Color.FromArgb(0, 255, 255, 255);
            }
            else
            {
                fillBtnHoraire = Color.FromArgb(50, 0, 0, 0);
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
                       GestionnaireEcrans.Changer(new ViewProfessionnelHoraire(GestionnaireEcrans));
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
                        GestionnaireEcrans.Changer(new ViewProfessionnelProfil(GestionnaireEcrans, UsagerConnecte.Usager));
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                );
            }
        }
   }
}
