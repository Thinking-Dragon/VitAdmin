using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.MVVM;

namespace VitAdmin.ControlModel
{
    public class ControlModelBandeauNavigationGeneral
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        public ICommand CmdProfil
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {

                    }
                );
            }
        }

        public ICommand CmdNotifications
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        
                    }
                );
            }
        }

        public ControlModelBandeauNavigationGeneral(GestionnaireEcrans gestionnaireEcrans, GestionnaireEcrans gestionnaireSousEcrans)
        {
            GestionnaireEcrans = gestionnaireEcrans;
            GestionnaireSousEcrans = gestionnaireSousEcrans;
        }
    }
}
