using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ViewModelGestionUsagersCreation : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ViewModelGestionUsagersCreation(GestionnaireEcrans gestionnaireEcrans)
            => GestionnaireEcrans = gestionnaireEcrans;

        public ICommand CmdConfirmer => new CommandeDeleguee(param =>
        {
            
        });

        private string _btnConfirmationTexte { get; set; } = "Créer";
        public string BtnConfirmationTexte
        {
            get => _btnConfirmationTexte;
            set { _btnConfirmationTexte = value; RaisePropertyChangedEvent("BtnConfirmationTexte"); }
        }
    }
}
