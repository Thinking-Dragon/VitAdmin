using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

                ConnexionBD.Instance().ExecuterRequete( // TODO: prevent obvious sql injection exploit -- @Clément réglé? Nah !
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

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format(
                    "SELECT u.nomUtilisateur, r.role, " +
                    "e.numEmploye, e.numPermis, e.NAS, " +
                    "c.prenom, c.nom, c.numAssuranceMaladie, c.dateNaissance, c.telephone, c.adresse " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "JOIN Employes e ON u.idEmploye = e.idEmploye " +
                    "JOIN Citoyens c ON e.idCitoyen = c.idCitoyen " +
                    "WHERE nomUtilisateur = '{0}'",
                    nom
                );

                ConnexionBD.Instance().ExecuterRequete(
                    requete, (MySqlDataReader lecteur) =>
                    {
                        usager = new Usager()
                        {
                            NomUtilisateur = lecteur.GetString("nomUtilisateur"),

                            // https://stackoverflow.com/questions/2290262/search-for-a-string-in-enum-and-return-the-enum
                            // Fonctionne, mais case sensitive
                            RoleUsager = (Role)System.Enum.Parse(typeof(Role), lecteur.GetString("role")),

                            NumEmploye = lecteur.GetString("numEmploye"),
                            NumPermis = lecteur.GetString("numPermis"),
                            NAS = lecteur.GetString("NAS"),
                            Prenom = lecteur.GetString("prenom"),
                            Nom = lecteur.GetString("nom"),
                            AssMaladie = lecteur.GetString("numAssuranceMaladie"),
                            DateNaissance = (DateTime)lecteur.GetMySqlDateTime("dateNaissance"),
                            NumTelephone = lecteur.GetString("telephone"),
                            Adresse = lecteur.GetString("adresse")
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
                                           "'{3}'"
                                           , usager.NumEmploye, usager.RoleUsager.ToString(), usager.NomUtilisateur, motDePasseHash);

                DataModelEmploye.AddEmploye(usager);
                ConnexionBD.Instance().ExecuterRequete(requete);
                // TODO : Recevoir code erreur BD dans cas d'erreur (duplicata)
            }
        }
    }
}
