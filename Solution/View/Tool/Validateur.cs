using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.View.Tool
{
    public class Validateur
    {
        public class Regle
        {
            private string Message { get; set; }
            private Action<string> Echec{ get; set; }
            private Func<bool> Condition { get; set; }

            public bool Tester()
            {
                bool succes = !Condition();
                if (!succes) Echec(Message);
                return succes;
            }

            public Regle(string message, Action<string> echec, Func<bool> condition)
            {
                Message = message;
                Echec = echec;
                Condition = condition;
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

        public Validateur(params Regle[] regles)
            => Regles = new List<Regle>(regles);
    }
}
