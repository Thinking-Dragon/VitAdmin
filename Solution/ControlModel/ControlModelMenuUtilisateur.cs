using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;
using VitAdmin.View;

namespace VitAdmin.ControlModel
{
   class ControlModelMenuUtilisateur : ObjetObservable
   {
        GestionnaireEcrans GestionnaireEcrans { get; set; }

        public bool IsBtnHoraireEnabled { get; set; }

        private Brush FillPrivate { get; set; }
        public Brush FillBtnHoraire
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
            if(!(GestionnaireEcrans.GetEcranPresent() is ViewProfessionnelHoraire))
            {
                IsBtnHoraireEnabled = true;
                FillBtnHoraire = Brushes.LightGray;
            }

            FillBtnHoraire = Brushes.Blue;
            IsBtnHoraireEnabled = true;
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
