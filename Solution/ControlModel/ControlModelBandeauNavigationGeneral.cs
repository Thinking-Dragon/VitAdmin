using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VitAdmin.Control;
using VitAdmin.MVVM;
using VitAdmin.View.Tool;

namespace VitAdmin.ControlModel
{
    public class ControlModelBandeauNavigationGeneral : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }
        private GestionnaireEcrans GestionnaireSousEcrans { get; set; }

        private string _textBoutonRetourEcran = string.Empty;
        public string TexteBoutonRetourEcran
        {
            get { return _textBoutonRetourEcran; }
            set
            {
                _textBoutonRetourEcran = value;
                RaisePropertyChangedEvent("TexteBoutonRetourEcran");
            }
        }

        public ICommand CmdRetourEcran
        {
            get
            {
                return new CommandeDeleguee(
                    param =>
                    {
                        if (GestionnaireSousEcrans.GetEcranPresent() is IEcranRetour)
                            (GestionnaireSousEcrans.GetEcranPresent() as IEcranRetour).CmdRetourEcranPrecedent();
                    }
                );
            }
        }

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
