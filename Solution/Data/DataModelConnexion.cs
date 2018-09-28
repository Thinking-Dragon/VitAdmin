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
            EtatAvecMessage retour = new EtatAvecMessage();

            if (ConnexionBD.Instance().EstConnecte())
            {
                int nbUsagers = 0;
                string requete = string.Format("SELECT nomUtilisateur, motDePasse FROM Usagers WHERE nomUtilisateur = '{0}'", usager);

                ConnexionBD.Instance().ExecuterRequete( // TODO: prevent obvious sql injection exploit -- @Clément réglé?
                    requete, (MySqlDataReader lecteur) =>
                    {
                        string nom = lecteur.GetString("nomUtilisateur");
                        string hash = lecteur.GetString("motDePasse");
                        if (motDePasse == hash) // TODO : Valider hash
                            retour.Etat = true;
                        ++nbUsagers;
                    }
                );
                if (nbUsagers == 0)
                    retour.Message = "Nom d'utilisateur ou mot de passe invalide";
            }
            else retour.Message = "Impossible de se connecter au service de données du système";
            
            return retour;
        }

        public static Usager GetUsager(string nom)
        {
            Usager usager = null;

            if(ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format("SELECT u.nomUtilisateur usager, r.role role " +
                                               "FROM Usagers u " +
                                               "JOIN Roles r ON u.idRole = r.idRole " +
                                               "WHERE u.nomUtilisateur = '{0}'", nom);

                ConnexionBD.Instance().ExecuterRequete( // TODO: ajouter les informations des superclasses d'Usager.
                    requete, (MySqlDataReader lecteur) =>
                    {
                        usager = new Usager()
                        {
                            NomUtilisateur = lecteur.GetString("usager"),
                            // Role usager (TODO: implémenter un convertisseur de string à Role)
                            // https://stackoverflow.com/questions/2290262/search-for-a-string-in-enum-and-return-the-enum
                            //RoleUsager = (Role)System.Enum.Parse(typeof(Role), lecteur.GetString("role"))
                        };
                    }
                );
            }

            return usager;
        }

        public static void AddUsager(Usager usager, string motDePasseHash)
        {
            EtatAvecMessage retour = new EtatAvecMessage();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format("INSERT INTO Usagers " +
                                           "(idRole, idEmploye, nomUtilisateur, motDePasse) " +
                                           "VALUES (" +
                                           "(SELECT idEmploye FROM Employes WHERE numEmploye = '{0}'," +
                                           "(SELECT idRole FROM Roles WHERE role = '{1}'," +
                                           "'{2}'," +
                                           "'{3}',"
                                           , usager.NumEmploye, usager.RoleUsager, usager.NomUtilisateur, motDePasseHash);

                //ConnexionBD.Instance().ExecuterRequete(requete);
            }
        }
    }
}
