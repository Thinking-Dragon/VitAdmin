using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    public static class DataModelCitoyen
    {
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List <Citoyen> getCitoyens()
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if(ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "FROM citoyens" +
                    ""
                    ,  SqlDR => { lstCitoyen.Add(new Citoyen
                    {
                        Nom = SqlDR.GetString("nom"),
                        Prenom = SqlDR.GetString("prenom"),
                        AssMaladie = SqlDR.GetString("numAssuranceMaladie"),
                        NumTelephone = SqlDR.GetString("telephone"),
                        Adresse = SqlDR.GetString("adresse")

                    });}
                    );
            }

            return lstCitoyen;
        }

        /*public static List<Citoyen> getCitoyensLstPatient()
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "FROM citoyens c" +
                    "INNER JOIN lits l ON c.idCitoyen = l.idCitoyen" +
                    ""
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nom"),
                            Prenom = SqlDR.GetString("prenom"),
                            AssMaladie = SqlDR.GetString("numAssuranceMaladie"),
                            NumTelephone = SqlDR.GetString("telephone"),
                            Adresse = SqlDR.GetString("adresse"),
                            Lit = new Lit { Numero = "numero",
                                            }
                        });
                    }
                    );
            }

            return lstCitoyen;*/
        }


    }
}
