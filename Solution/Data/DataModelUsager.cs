using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public class DataModelUsager
    {
        public static void Post(Usager usager, string motDePasseEncrypte)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                DataModelEmploye.Post(usager as Employe);
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "INSERT INTO Usagers (nomUtilisateur, motDePasse, idEmploye, idRole) " +
                        "VALUES ( " +
                        "   '{0}', " +
                        "   '{1}', " +
                        "   (SELECT idEmploye FROM Employes WHERE NumEmploye = '{2}'), " +
                        "   (SELECT idRole FROM Roles WHERE role = '{3}') " +
                        ")",
                        usager.NomUtilisateur, motDePasseEncrypte,
                        usager.NumEmploye, usager.RoleUsager
                    )
                );
            }
        }

        public static List<Usager> GetUsagers()
        {
            List<Usager> usagers = new List<Usager>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT u.nomUtilisateur, r.role, " +
                    "e.numEmploye, e.numPermis, e.NAS, " +
                    "c.prenom, c.nom, c.numAssuranceMaladie, c.dateNaissance, c.telephone, c.adresse, " +
                    "g.nom genre, p.nom poste " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "JOIN Employes e ON u.idEmploye = e.idEmploye " +
                    "JOIN Citoyens c ON e.idCitoyen = c.idCitoyen " +
                    "JOIN Genres g ON g.idGenre = c.idGenre " +
                    "JOIN Postes p ON e.idPoste = p.idPoste ",
                    lecteur => usagers.Add(new Usager()
                    {
                        NomUtilisateur = lecteur.GetString("nomUtilisateur"),
                        RoleUsager = (Role)System.Enum.Parse(typeof(Role), lecteur.GetString("role")),
                        
                        NumEmploye = lecteur.GetString("numEmploye"),
                        Poste = lecteur.GetString("poste"),
                        NumPermis = lecteur.GetString("numPermis"),
                        NAS = lecteur.GetString("NAS"),

                        Nom = lecteur.GetString("nom"),
                        Prenom = lecteur.GetString("prenom"),
                        AssMaladie = lecteur.GetString("numAssuranceMaladie"),
                        DateNaissance = (DateTime)lecteur.GetMySqlDateTime("dateNaissance"),
                        Adresse = lecteur.GetString("adresse"),
                        NumTelephone = lecteur.GetString("telephone"),
                        Genre = (Genre)System.Enum.Parse(typeof(Genre), lecteur.GetString("genre"))
                    })
                );
            }

            return usagers;
        }

        public static Usager GetUsager(int idUsager)
        {
            Usager usager = null;

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format(
                    "SELECT u.nomUtilisateur, r.role, " +
                    "e.numEmploye, e.numPermis, e.NAS, " +
                    "c.prenom, c.nom, c.numAssuranceMaladie, c.dateNaissance, c.telephone, c.adresse, " +
                    "g.nom genre, p.nom poste " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "JOIN Employes e ON u.idEmploye = e.idEmploye " +
                    "JOIN Citoyens c ON e.idCitoyen = c.idCitoyen " +
                    "JOIN Genres g ON g.idGenre = c.idGenre " +
                    "JOIN Postes p ON e.idPoste = p.idPoste " +
                    "WHERE u.idUsager = {0} ",
                    idUsager
                );

                ConnexionBD.Instance().ExecuterRequete(
                    requete, (MySqlDataReader lecteur) =>
                    {
                        usager = new Usager()
                        {
                            NomUtilisateur = lecteur.GetString("nomUtilisateur"),
                            RoleUsager = (Role)System.Enum.Parse(typeof(Role), lecteur.GetString("role")),

                            NumEmploye = lecteur.GetString("numEmploye"),
                            Poste = lecteur.GetString("poste"),
                            NumPermis = lecteur.GetString("numPermis"),
                            NAS = lecteur.GetString("NAS"),

                            Nom = lecteur.GetString("nom"),
                            Prenom = lecteur.GetString("prenom"),
                            AssMaladie = lecteur.GetString("numAssuranceMaladie"),
                            DateNaissance = (DateTime)lecteur.GetMySqlDateTime("dateNaissance"),
                            Adresse = lecteur.GetString("adresse"),
                            NumTelephone = lecteur.GetString("telephone"),
                            Genre = (Genre)System.Enum.Parse(typeof(Genre), lecteur.GetString("genre"))
                        };
                    }
                );


            }

            return usager;
        }

        public static List<Usager> GetInfirmieresChef()
        {
            List<Usager> usagers = new List<Usager>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                List<int> idUsagers = new List<int>();
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT u.idUsager " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "WHERE r.role = 'InfChef'",
                    lecteur => idUsagers.Add(int.Parse(lecteur.GetString("idUsager")))
                );
                for (int i = 0; i < idUsagers.Count; i++)
                    usagers.Add(GetUsager(idUsagers[i]));
            }

            return usagers;
        }

        public static void Put(Usager ancienUsager, Usager nouvelUsager)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Usagers " +
                        "SET nomUtilisateur = '{0}', " +
                        "    idRole = (SELECT idRole FROM Roles WHERE role = '{1}') " +
                        "WHERE nomUtilisateur = '{2}' ",
                        nouvelUsager.NomUtilisateur, nouvelUsager.RoleUsager, ancienUsager.NomUtilisateur
                    )
                );

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Employes " +
                        "SET numEmploye = '{0}', numPermis = '{1}', NAS = '{2}', " +
                        "    idPoste = (SELECT idPoste FROM Postes WHERE nom = '{3}') " +
                        "WHERE numEmploye = '{4}'",
                        nouvelUsager.NumEmploye, nouvelUsager.NumPermis,
                        nouvelUsager.NAS, nouvelUsager.Poste,
                        ancienUsager.NumEmploye
                    )
                );

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE Citoyens " +
                        "SET prenom = '{0}', nom = '{1}', numAssuranceMaladie = '{2}', dateNaissance = '{3}', telephone = '{4}', adresse = '{5}', " +
                        "    idGenre = (SELECT idGenre FROM Genres WHERE nom = '{6}') " +
                        "WHERE numAssuranceMaladie = '{7}'",
                        nouvelUsager.Prenom, nouvelUsager.Nom,
                        nouvelUsager.AssMaladie, nouvelUsager.DateNaissance,
                        nouvelUsager.NumTelephone, nouvelUsager.Adresse,
                        nouvelUsager.Genre, ancienUsager.AssMaladie
                    )
                );
            }
        }

        public static void Delete(Usager usager)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format("DELETE FROM Usagers WHERE nomUtilisateur = '{0}'", usager.NomUtilisateur));
                DataModelEmploye.Delete(usager as Employe);
            }
        }
    }
}
