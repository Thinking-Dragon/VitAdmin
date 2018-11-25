using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VitAdmin.Data;
using VitAdmin.Helper;
using VitAdmin.Model;
using VitAdmin.MVVM;
using VitAdmin.View;
using VitAdmin.View.Tool;

namespace VitAdmin.ViewModel
{
    public class ViewModelGestionUsagersCreation : ObjetObservable
    {
        private GestionnaireEcrans GestionnaireEcrans { get; set; }

        public ObservableCollection<string> PostesPossibles { get; set; }

        private string _posteSelectionne = string.Empty;
        public string PosteSelectionne
        {
            get => _posteSelectionne;
            set { _posteSelectionne = value; RaisePropertyChangedEvent(nameof(PosteSelectionne)); }
        }

        private Usager AncienUsager { get; set; } = null;

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

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set { _password = value; RaisePropertyChangedEvent(nameof(Password)); }
        }
        
        public ICommand CmdConfirmer => new CommandeDeleguee(param => {
            Usager.Poste = PosteSelectionne;
            if(Validateur.Tester())
            {
                if (AncienUsager == null)
                    DataModelUsager.Post(Usager, Crypto.Encrypter(Password));
                else
                    DataModelUsager.Put(AncienUsager, Usager);
                GestionnaireEcrans.Changer(new ViewGestionUsagers(GestionnaireEcrans));
            }
        });

        private string _btnConfirmationTexte { get; set; } = "Créer";
        public string BtnConfirmationTexte
        {
            get => _btnConfirmationTexte;
            set { _btnConfirmationTexte = value; RaisePropertyChangedEvent("BtnConfirmationTexte"); }
        }

        private string _messageErreurNom = string.Empty;
        public string MessageErreurNom
        {
            get => _messageErreurNom;
            set { _messageErreurNom = value; RaisePropertyChangedEvent(nameof(MessageErreurNom)); }
        }

        private Validateur Validateur { get; set; }

        public ViewModelGestionUsagersCreation(GestionnaireEcrans gestionnaireEcrans, Usager usager)
        {
            GestionnaireEcrans = gestionnaireEcrans;

            PostesPossibles = new ObservableCollection<string>(DataModelPoste.GetPostes());
            Usager = usager;

            if (usager != null)
            {
                BtnConfirmationTexte = "Appliquer les modifications";
                PasswordVisibility = Visibility.Collapsed;

                AncienUsager = new Usager
                {
                    NomUtilisateur = usager.NomUtilisateur,
                    NumEmploye = usager.NumEmploye,
                    AssMaladie = usager.AssMaladie
                };

                for (int i = 0; i < PostesPossibles.Count; i++)
                    if (PostesPossibles[i] == usager.Poste)
                        PosteSelectionne = PostesPossibles[i];
            }
            else
            {
                Usager = new Usager
                {
                    Nom = string.Empty,
                    Prenom = string.Empty,
                    AssMaladie = string.Empty,
                    Adresse = string.Empty,
                    NumTelephone = string.Empty,
                    NumEmploye = string.Empty,
                    Poste = string.Empty,
                    NumPermis = string.Empty,
                    NAS = string.Empty,
                    NomUtilisateur = string.Empty
                };
                Random random = new Random();
                Password = new string(
                    Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 16)
                    .Select(s => s[random.Next(s.Length)]).ToArray()
                );
            }

            Validateur = new Validateur(
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length < 1 || Usager.Nom.Length > 20
                ),
                new Validateur.Regle(
                    "Le nom doit avoir au moins 1 caractères",
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length >= 1
                ),
                new Validateur.Regle(
                    "Le nom doit avoir au plus 20 caractères",
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length <= 20
                )
            );
        }
    }
}
