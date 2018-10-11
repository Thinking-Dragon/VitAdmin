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

        public static void PostInstructions(List<string> instructions, int idEtape)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                foreach (string instruction in instructions)
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        String.Format(
                            "INSERT INTO SousEtapes (description, idEtape) " +
                            "VALUES ('{0}', {1})",
                            instruction, idEtape
                        )
                    );
                }
            }
        }

        public static void DeleteInstructions(int idEtape)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                   String.Format(
                       "DELETE FROM SousEtapes " +
                       "WHERE idEtape = {0}",
                       idEtape
                   )
               );
            }
        }
    }
}
