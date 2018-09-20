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
                ConnexionBD.Instance().ExecuterRequete( // TODO: prevent obvious sql injection exploit
                    "SELECT nom, hash FROM Usagers WHERE nom = '" + usager + "'", (MySqlDataReader lecteur) =>
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
                    "SELECT u.nom usager, r.nom role " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "WHERE u.nom = '" + nom + "'", (MySqlDataReader lecteur) =>
                    {
                        usager = new Usager()
                        {
                            NomUtilisateur = lecteur.GetString(0),
                            // Role usager
                        };
                  }
                );
            }

            return usager;
        }
    }
}
