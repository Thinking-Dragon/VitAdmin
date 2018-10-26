using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
   class ControlModelMenuUtilisateur : ObjetObservable
   {
        GestionnaireEcrans GestionnaireEcrans { get; set; }
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

        private Color FillPrivate { get; set; }
        public Color FillBtnHoraire
        {
            get
            {
                return FillPrivate;
            }
            set
            {
                FillPrivate = value;
                RaisePropertyChangedEvent("FillBtnHoraire");
            }
        }

        public ControlModelMenuUtilisateur(GestionnaireEcrans gestionnaireEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            if (!(GestionnaireEcrans.GetEcranPresent() is ViewProfessionnelHoraire))
            {
                IsBtnHoraireEnabled = true;
                FillBtnHoraire = Color.FromArgb(0,255,255,255);
            }
            else
            {
                FillBtnHoraire = Color.FromArgb(50, 0, 0, 0);
                IsBtnHoraireEnabled = false;
            }

            
        }
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
   }
}
