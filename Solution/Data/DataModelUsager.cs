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
        public static Usager GetUsager(int idUsager)
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
    }
}
