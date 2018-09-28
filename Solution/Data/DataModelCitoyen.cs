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
                string requete = "SELECT * FROM Citoyens c " +
                                 "JOIN Genres g ON g.idGenre = c.idGenre";

                ConnexionBD.Instance().ExecuterRequete(requete,  SqlDR => 
                    {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("c.nom"),
                            Prenom = SqlDR.GetString("c.prenom"),
                            AssMaladie = SqlDR.GetString("c.numAssuranceMaladie"),
                            NumTelephone = SqlDR.GetString("c.telephone"),
                            Adresse = SqlDR.GetString("c.adresse"),

                            Genre = (Genre)System.Enum.Parse(typeof(Genre), SqlDR.GetString("g.nom"))
                        });
                    });
            }

            return lstCitoyen;
        }

        public static void AddCitoyen(Citoyen citoyen)
        {
            EtatAvecMessage retour = new EtatAvecMessage();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format("INSERT INTO Citoyens " +
                                           "(idGenre, prenom, nom, numAssuranceMaladie, telephone, adresse) " +
                                           "VALUES (" +
                                           "(SELECT idGenre FROM Genre WHERE nom = '{0}'," +
                                           "'{1}'," +
                                           "'{2}'," +
                                           "'{3}'," +
                                           "'{4}'," +
                                           "'{5}'"
                                           , citoyen.Genre, citoyen.Prenom, citoyen.Nom, citoyen.AssMaladie, citoyen.NumTelephone, citoyen.Adresse);

                ConnexionBD.Instance().ExecuterRequete(requete);
                // TODO : Recevoir code erreur BD dans cas d'erreur (duplicata)
                //https://dev.mysql.com/doc/refman/5.6/en/error-messages-server.html
            }
        }
    }
}
