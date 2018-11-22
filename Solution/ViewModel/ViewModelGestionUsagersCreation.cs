using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.ViewModel
{
    public class ViewModelGestionUsagersCreation : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        private Usager _usager;
        public Usager Usager
        {
            get => _usager;
            set { _usager = value; RaisePropertyChangedEvent("Usager"); }
        }

        private Visibility _passwordVisibility = Visibility.Visible;
        public Visibility PasswordVisibility
        {
            get => _passwordVisibility;
            set { _passwordVisibility = value; RaisePropertyChangedEvent("PasswordVisibility"); }
        }

        public ViewModelGestionUsagersCreation(GestionnaireEcrans gestionnaireEcrans, Usager usager)
        {
            GestionnaireEcrans = gestionnaireEcrans;

            Usager = usager;

            if (usager != null)
            {
                BtnConfirmationTexte = "Appliquer les modifications";
                PasswordVisibility = Visibility.Collapsed;
            }
        }
        
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
