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

        #region Messages validation
        private string _messageErreurNom = string.Empty;
        public string MessageErreurNom
        {
            get => _messageErreurNom;
            set { _messageErreurNom = value; RaisePropertyChangedEvent(nameof(MessageErreurNom)); }
        }

        private string _messageErreurPrenom = string.Empty;
        public string MessageErreurPrenom
        {
            get => _messageErreurPrenom;
            set { _messageErreurPrenom = value; RaisePropertyChangedEvent(nameof(MessageErreurPrenom)); }
        }

        private string _messageErreurAssuranceMaladie = string.Empty;
        public string MessageErreurAssuranceMaladie
        {
            get => _messageErreurAssuranceMaladie;
            set { _messageErreurAssuranceMaladie = value; RaisePropertyChangedEvent(nameof(MessageErreurAssuranceMaladie)); }
        }

        private string _messageErreurDateNaissance = string.Empty;
        public string MessageErreurDateNaissance
        {
            get => _messageErreurDateNaissance;
            set { _messageErreurDateNaissance = value; RaisePropertyChangedEvent(nameof(MessageErreurDateNaissance)); }
        }

        private string _messageErreurTelephone = string.Empty;
        public string MessageErreurTelephone
        {
            get => _messageErreurTelephone;
            set { _messageErreurTelephone = value; RaisePropertyChangedEvent(nameof(MessageErreurTelephone)); }
        }

        private string _messageErreurAdresse = string.Empty;
        public string MessageErreurAdresse
        {
            get => _messageErreurAdresse;
            set { _messageErreurAdresse = value; RaisePropertyChangedEvent(nameof(MessageErreurAdresse)); }
        }

        private string _messageErreurNoEmploye = string.Empty;
        public string MessageErreurNoEmploye
        {
            get => _messageErreurNoEmploye;
            set { _messageErreurNoEmploye = value; RaisePropertyChangedEvent(nameof(MessageErreurNoEmploye)); }
        }

        private string _messageErreurPoste = string.Empty;
        public string MessageErreurPoste
        {
            get => _messageErreurPoste;
            set { _messageErreurPoste = value; RaisePropertyChangedEvent(nameof(MessageErreurPoste)); }
        }

        private string _messageErreurNoPermis = string.Empty;
        public string MessageErreurNoPermis
        {
            get => _messageErreurNoPermis;
            set { _messageErreurNoPermis = value; RaisePropertyChangedEvent(nameof(MessageErreurNoPermis)); }
        }

        private string _messageErreurNAS = string.Empty;
        public string MessageErreurNAS
        {
            get => _messageErreurNAS;
            set { _messageErreurNAS = value; RaisePropertyChangedEvent(nameof(MessageErreurNAS)); }
        }

        private string _messageErreurNomUsager = string.Empty;
        public string MessageErreurNomUsager
        {
            get => _messageErreurNomUsager;
            set { _messageErreurNomUsager = value; RaisePropertyChangedEvent(nameof(MessageErreurNomUsager)); }
        }

        private string _messageErreurMotDePasse = string.Empty;
        public string MessageErreurMotDePasse
        {
            get => _messageErreurMotDePasse;
            set { _messageErreurMotDePasse = value; RaisePropertyChangedEvent(nameof(MessageErreurMotDePasse)); }
        }
        #endregion

        private Validateur Validateur { get; set; }

        public ViewModelGestionUsagersCreation(GestionnaireEcrans gestionnaireEcrans, Usager usager)
        {
            #region Initialisation
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
                    Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 16)
                    .Select(s => s[random.Next(s.Length)]).ToArray()
                );
            }
            #endregion

            #region Validation
            Validateur = new Validateur(
            #region Nom
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length >= 1 && Usager.Nom.Length <= 20, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length < 1
                ),      // length > 15
                new Validateur.Regle(
                    "Doit avoir au plus 15 caractères",
                    message => MessageErreurNom = message,
                    () => Usager.Nom.Length > 15
                ),
            #endregion
            #region Prénom
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurPrenom = message,
                    () => Usager.Prenom.Length >= 1 && Usager.Prenom.Length <= 20, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurPrenom = message,
                    () => Usager.Prenom.Length < 1
                ),      // length > 15
                new Validateur.Regle(
                    "Doit avoir au plus 15 caractères",
                    message => MessageErreurPrenom = message,
                    () => Usager.Prenom.Length > 15
                ),
            #endregion
            #region Assurance maladie
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurAssuranceMaladie = message,
                    () => Usager.ValiderFormatAssMaladie()
                       && AncienUsager != null || Usager.ValiderDuplicataAssMaladie(DataModelCitoyen.GetCitoyens()), true
                ),      // Format du numéro d'assurance maladie
                new Validateur.Regle(
                    "Format invalide",
                    message => MessageErreurAssuranceMaladie = message,
                    () => !Usager.ValiderFormatAssMaladie()
                ),      // Duplicata
                new Validateur.Regle(
                    "Existe déjà dans notre système",
                    message => MessageErreurAssuranceMaladie = message,
                    () => AncienUsager == null && !Usager.ValiderDuplicataAssMaladie(DataModelCitoyen.GetCitoyens())
                ),
            #endregion
            #region Date de naissance
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurDateNaissance = message,
                    () => Usager.DateNaissance.CompareTo(DateTime.Now) <= 0, true
                ),      // Date de naissance > maintenant
                new Validateur.Regle(
                    "Ne peut pas être dans le futur!",
                    message => MessageErreurDateNaissance = message,
                    () => Usager.DateNaissance.CompareTo(DateTime.Now) > 0
                ),
            #endregion
            #region Numéro de téléphone
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurTelephone = message,
                    () => Usager.NumTelephone.Length == 10, true
                ),      // length != 10
                new Validateur.Regle(
                    "Format invalid",
                    message => MessageErreurTelephone = message,
                    () => Usager.NumTelephone.Length != 10
                ),
            #endregion
            #region Adresse
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurAdresse = message,
                    () => Usager.Adresse.Length >= 1 && Usager.Adresse.Length <= 100, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurAdresse = message,
                    () => Usager.Adresse.Length < 1
                ),      // length > 100
                new Validateur.Regle(
                    "Doit avoir au plus 100 caractères",
                    message => MessageErreurAdresse = message,
                    () => Usager.Adresse.Length > 100
                ),
            #endregion
            #region Numéro d'employé
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNoEmploye = message,
                    () => Usager.NumEmploye.Length >= 1 && Usager.NumEmploye.Length <= 20, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurNoEmploye = message,
                    () => Usager.NumEmploye.Length < 1
                ),      // length > 25
                new Validateur.Regle(
                    "Doit avoir au plus 25 caractères",
                    message => MessageErreurNoEmploye = message,
                    () => Usager.NumEmploye.Length > 25
                ),
            #endregion
            #region Poste
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurPoste = message,
                    () => Usager.Poste != string.Empty, true
                ),      // Empty
                new Validateur.Regle(
                    "Ne doit pas être vide",
                    message => MessageErreurPoste = message,
                    () => Usager.Poste == string.Empty
                ),
            #endregion
            #region Numéro permis
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNoPermis = message,
                    () => Usager.NumPermis.Length >= 1 && Usager.NumPermis.Length <= 20, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurNoPermis = message,
                    () => Usager.NumPermis.Length < 1
                ),      // length > 25
                new Validateur.Regle(
                    "Doit avoir au plus 25 caractères",
                    message => MessageErreurNoPermis = message,
                    () => Usager.NumPermis.Length > 25
                ),
            #endregion
            #region NAS
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNAS = message,
                    () => Usager.NAS.Length == 9, true
                ),      // length != 9
                new Validateur.Regle(
                    "Format invalid",
                    message => MessageErreurNAS = message,
                    () => Usager.NAS.Length != 9
                ),
            #endregion
            #region Nom d'usager
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurNomUsager = message,
                    () => Usager.NomUtilisateur.Length >= 1 && Usager.NomUtilisateur.Length <= 20, true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurNomUsager = message,
                    () => Usager.NomUtilisateur.Length < 1
                ),      // length > 25
                new Validateur.Regle(
                    "Doit avoir au plus 25 caractères",
                    message => MessageErreurNomUsager = message,
                    () => Usager.NomUtilisateur.Length > 25
                ),
            #endregion
            #region Mot de passe
                // Clear
                new Validateur.Regle(
                    string.Empty,
                    message => MessageErreurMotDePasse = message,
                    () => AncienUsager != null || (Password.Length >= 1 && Password.Length <= 20), true
                ),      // length < 1
                new Validateur.Regle(
                    "Doit avoir au moins 1 caractère",
                    message => MessageErreurMotDePasse = message,
                    () => AncienUsager == null && Password.Length < 1
                ),      // length > 30
                new Validateur.Regle(
                    "Doit avoir au plus 30 caractère",
                    message => MessageErreurMotDePasse = message,
                    () => AncienUsager == null && Password.Length > 30
                )
            #endregion
            );
            #endregion
        }
    }
}
