using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.MVVM;

namespace VitAdmin.Control
{
    public class placeHolder: ObjetObservable
    {
        private string TextePlaceHolder { get; set; }
        private  string _texte;
        public string Texte
        {
            get => _texte;
            set
            {
                _texte = value;
                RaisePropertyChangedEvent("Texte");
            }
        }

        public placeHolder(string texte, string textePlaceholder)
        {
            Texte = texte;
            TextePlaceHolder = textePlaceholder;
        }

        public void EnleverTexte(object sender, EventArgs e)
        {
            Texte = "";
        }

        public void AjouterTexte(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Texte))
               Texte = TextePlaceHolder;
        }
    }
}
