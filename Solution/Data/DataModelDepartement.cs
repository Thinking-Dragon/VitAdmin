using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelDepartement
    {
        /*public static Departement GetDepartement(string numEmploye)
        {
            Departement departement = new Departement();

            if(ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "SELECT "
                    )
                );
            }
        }*/

        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Departement> GetDepartements()
        {
            List<Departement> lstDepartement = new List<Departement>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "FROM departements"
                    , SqlDR => {
                        lstDepartement.Add(new Departement
                        {
                            Nom = SqlDR.GetString("nom"),
                            Abreviation = SqlDR.GetString("abreviation")

                        });
                    }
                    );
            }

            return lstDepartement;
        }

        public static Departement GetDepartementEmploye(Employe employe)
        {
            Departement departement = new Departement();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT d.nom depNom, d.abreviation depAbrev " +
                    "FROM departements d " +
                    "JOIN quarts q ON q.idDepartement = d.idDepartement " +
                    "JOIN quartsemployes qe ON qe.idQuart = q.idQuart " +
                    "JOIN employes e ON e.idEmploye = qe.idEmploye " +
                    "WHERE e.numEmploye = '" + employe.NumEmploye + "' "
                    , SqlDR => {
                        departement.Nom = SqlDR.GetString("depNom");
                        departement.Abreviation = SqlDR.GetString("depAbrev");
                    }
                    );
            }

            return departement;
        }
    }
}
