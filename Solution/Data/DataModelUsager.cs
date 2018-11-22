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
        public static List<Usager> GetUsagers()
        {
            List<Usager> usagers = new List<Usager>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT u.nomUtilisateur, r.role, " +
                    "e.numEmploye, e.numPermis, e.NAS, " +
                    "c.prenom, c.nom, c.numAssuranceMaladie, c.dateNaissance, c.telephone, c.adresse, " +
                    "g.nom genre " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "JOIN Employes e ON u.idEmploye = e.idEmploye " +
                    "JOIN Citoyens c ON e.idCitoyen = c.idCitoyen " +
                    "JOIN Genres g ON g.idGenre = c.idGenre ",
                    lecteur => usagers.Add(new Usager()
                    {
                        NomUtilisateur = lecteur.GetString("nomUtilisateur"),

                        RoleUsager = (Role)System.Enum.Parse(typeof(Role), lecteur.GetString("role")),
                        Genre = (Genre)System.Enum.Parse(typeof(Genre), lecteur.GetString("genre")),

                        NumEmploye = lecteur.GetString("numEmploye"),
                        NumPermis = lecteur.GetString("numPermis"),
                        NAS = lecteur.GetString("NAS"),
                        Prenom = lecteur.GetString("prenom"),
                        Nom = lecteur.GetString("nom"),
                        AssMaladie = lecteur.GetString("numAssuranceMaladie"),
                        DateNaissance = (DateTime)lecteur.GetMySqlDateTime("dateNaissance"),
                        NumTelephone = lecteur.GetString("telephone"),
                        Adresse = lecteur.GetString("adresse")
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
                    "g.nom genre " +
                    "FROM Usagers u " +
                    "JOIN Roles r ON u.idRole = r.idRole " +
                    "JOIN Employes e ON u.idEmploye = e.idEmploye " +
                    "JOIN Citoyens c ON e.idCitoyen = c.idCitoyen " +
                    "JOIN Genres g ON g.idGenre = c.idGenre " +
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
                            Genre = (Genre)System.Enum.Parse(typeof(Genre), lecteur.GetString("genre")),

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

        public static void Put(Usager usager, string nomUtilisateur)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE TABLE Usagers " +
                        "SET nomUtilisateur = '{0}', " +
                        "    idRole = (SELECT idRole FROM Roles WHERE role = '{1}') " +
                        "WHERE idUsager = (" +
                        "   SELECT idUsager " +
                        "   FROM Usagers " +
                        "   WHERE nomUtilisateur = '{2}' " +
                        ")",
                        usager.NomUtilisateur, usager.RoleUsager, nomUtilisateur
                    )
                );

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE TABLE Employe " +
                        "SET numEmploye = '{0}', numPermis = '{1}', NAS = '{2}' " +
                        "WHERE idEmploye = ( " +
                        "   SELECT e.idEmploye " +
                        "   FROM Employes e " +
                        "   JOIN Usagers u ON u.idEmploye = e.idEmploye " +
                        ")"
                    )
                );

                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "UPDATE TABLE Citoyen " +
                        "SET prenom = '{0}', nom = '{1}', numAssuranceMaladie = '{2}', dateNaissance = '{3}', telephone = '{4}', adresse = '{5}', " +
                        "    idGenre = (SELECT idGenre FROM Genres WHERE nom = '{6}') " +
                        "WHERE idCitoyen = ( " +
                        "   SELECT idCitoyen " +
                        "   FROM Citoyens c " +
                        "   JOIN Employes e ON c.idCitoyen = e.idCitoyen " +
                        "   JOIN Usagers u ON e.idEmploye = u.idEmploye " +
                        ")"
                    )
                );
            }
        }
    }
}
