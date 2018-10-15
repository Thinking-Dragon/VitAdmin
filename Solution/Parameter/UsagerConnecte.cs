using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Data;
using VitAdmin.Model;

namespace VitAdmin.Parameter
{
    public class UsagerConnecte
    {
        public static Usager Usager { get; private set; }
        public static bool EstConnecte { get; private set; } = false;

        public static EtatAvecMessage TenterConnexion(string usager, string motDePasse)
        {
            EtatAvecMessage validation = DataModelConnexion.ValiderIdentite(usager, motDePasse);
            

            if (validation.Etat)
            {
                Usager = DataModelConnexion.GetUsager(usager);
                EstConnecte = true;
                DataModelEmploye.SetIdEmployeUsagerConnecte();
            }

            return validation;
        }
    }
}
