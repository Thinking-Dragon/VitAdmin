using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Data
{
    public class EtatAvecMessage
    {
        public string Message { get; set; }
        public bool Etat { get; set; }


        /// <summary>
        /// Par défaut, flag à "false", string vide.
        /// Déclarer directement en ajustant le flag et/ou en ajoutant un message.
        /// </summary>
        public EtatAvecMessage() : this(false, string.Empty)
        { }
        public EtatAvecMessage(bool etat) : this(etat, string.Empty)
        { }
        public EtatAvecMessage(string message) : this(false, message)
        { }
        public EtatAvecMessage(bool etat, string message)
        {
            Etat = etat;
            Message = message;
        }
    }
}
