using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelConnexion
    {
        public static EtatAvecMessage ValiderIdentite(string usager, string motDePasse)
        {
            EtatAvecMessage retour = new EtatAvecMessage() { Etat = false };

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT nom, hash FROM Usagers WHERE nom = *USAGER*", (MySqlDataReader lecteur) =>
                    {
                        string nom = lecteur.GetString(0);
                        string hash = lecteur.GetString(1);
                        if (true) // Valider hash
                            retour.Etat = true;
                    }
                );
            }
            else retour.Message = "Impossible de se connecter au service de données du système";

            return retour;
        }

        public static Usager GetUsager(string nom)
        {
            Usager usager = null;

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * FROM Usagers WHERE nom = *USAGER*", (MySqlDataReader lecteur) =>
                    {
/*                      usager = new Usager(
                            lecteur.GetString(0),
                            lecteur.GetString(1),
                            lecteur.GetString(2),
                            lecteur.GetString(3)
                        );
*/                  }
                );
            }

            return usager;
        }
    }
}
