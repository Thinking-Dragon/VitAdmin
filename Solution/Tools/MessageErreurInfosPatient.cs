using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.MVVM;

namespace VitAdmin.Tools
{
    public class MessageErreurInfosPatient : ObjetObservable
    {
        private string prenom;
        public string Prenom
        {
            get => prenom;

            set
            {
                prenom = value;
                RaisePropertyChangedEvent("Prenom");
            }
        }
        private string nom;
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                RaisePropertyChangedEvent("Nom");
            }
        }
        private string adresse;
        public string Adresse
        {
            get => adresse;
            set
            {
                adresse = value;
                RaisePropertyChangedEvent("Adresse");
            }
        }
        private string telephone;
        public string Telephone
        {
            get => telephone;
            set
            {
                telephone = value;
                RaisePropertyChangedEvent("Telephone");
            }
        }
        private string assMaladie;
        public string AssMaladie
        {
            get => assMaladie;
            set
            {
                assMaladie = value;
                RaisePropertyChangedEvent("AssMaladie");
            }
        }


        public void ActiverMessageErreur(Citoyen citoyen, List<Citoyen> lstCitoyen)
        {
            if(!citoyen.ValiderPrenom())
                Prenom = new StringBuilder("Minimum " + Citoyen.iMIN_CARAC_NOMPRENOM.ToString() + " caractères et maximum " + Citoyen.iMAX_CARAC_NOMPRENOM.ToString() + " caractères").ToString();
            if(!citoyen.ValiderNom())
                Nom = new StringBuilder("Minimum " + Citoyen.iMIN_CARAC_NOMPRENOM.ToString() + " caractères et maximum " + Citoyen.iMAX_CARAC_NOMPRENOM.ToString() + " caractères").ToString();
            if(!citoyen.ValiderAdresse())
                Adresse = new StringBuilder("Maximum " + Citoyen.iMaxAdresse + " caractères").ToString();
            if(!citoyen.ValiderTelephone())
                Telephone = new StringBuilder("Doit contenir " + Citoyen.iCaracteresTelephone + " caractères").ToString();
            if(!citoyen.ValiderFormatAssMaladie())
                AssMaladie = new StringBuilder("Doit respecter le format AAAA00000000").ToString();
            if(!citoyen.ValiderDuplicataAssMaladie(lstCitoyen))
                AssMaladie = new StringBuilder("Numéro déjà utilisé").ToString();

        }

        public void ViderMessages()
        {
            Prenom = "";
            Nom = "";
            Telephone = "";
            Adresse = "";
            AssMaladie = "";
        }
    }
}
