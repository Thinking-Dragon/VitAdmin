using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.View.Tool
{
    /// <summary>
    /// Description: Permet de définir des règles de validation pour un ensemble de propriétés éditables par l'usager.
    /// Auteur: Clément Gaßmann-Prince.
    /// </summary>
    public class Validateur
    {
        /// <summary>
        /// Règle de validation.
        /// On lui donne un message à afficher et une condition d'échec de la validation.
        /// L'action «échec» sera appelée si la validation échoue.
        /// </summary>
        public class Regle
        {
            private string Message { get; set; }
            private Action<string> Echec{ get; set; }
            private Func<bool> Condition { get; set; }
            private bool Ignorer { get; set; }

            public bool Tester()
            {
                bool echec = Condition();
                if (echec) Echec(Message);
                return Ignorer ? true : !echec;
            }

            /// <summary>
            /// Construit une règle de validation.
            /// </summary>
            /// <param name="message">Message à afficher en cas d'échec</param>
            /// <param name="echec">Fonction à appeler en cas d'échec (elle reçoit le message à afficher)</param>
            /// <param name="condition">Condition d'échec</param>
            /// <param name="ignorer">Appelle la fonction d'échec si la condition est remplie, mais n'affecte pas le résultat du test</param>
            public Regle(string message, Action<string> echec, Func<bool> condition, bool ignorer = false)
            {
                Message = message;
                Echec = echec;
                Condition = condition;
                Ignorer = ignorer;
            }
        }

        private List<Regle> Regles { get; set; }

        public bool Tester()
        {
            bool succes = true;

            foreach (var regle in Regles)
                if (!regle.Tester()) succes = false;
            
            return succes;
        }

        /// <summary>
        /// Construit un validateur de règles pour un ensemble de propriétés éditables par l'usager.
        /// </summary>
        /// <param name="regles">Règles de validation</param>
        public Validateur(params Regle[] regles)
            => Regles = new List<Regle>(regles);
    }
}
