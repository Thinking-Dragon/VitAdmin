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
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Departement> getDepartement()
        {
            // On crée une liste de citoyen venant de la BD
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
    }
}
