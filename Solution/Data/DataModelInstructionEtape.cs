using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitAdmin.Data
{
    public static class DataModelInstructionEtape
    {
        public static List<string> GetInstructions(int idEtape)
        {
            List<string> instructions = new List<string>();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT description nom " +
                        "FROM SousEtapes " +
                        "WHERE idEtape = {0}",
                        idEtape
                    ), lecteur => instructions.Add(lecteur.GetString("nom"))
                );
            }

            return instructions;
        }
    }
}
