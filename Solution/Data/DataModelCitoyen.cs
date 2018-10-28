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
        #region GET
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List <Citoyen> GetCitoyens()
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
                    "FROM citoyens "
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

        // ****** FÉLIX, tu peux utiliser cette requête pour l'infirmière-chef!! *******
        public static List<Citoyen> GetCitoyensLstPatient(Employe employe)
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, g.Nom GenreCit, c.dateNaissance DNaiss, c.telephone Tel, c.adresse AdresseCit, d.nom nomDep, ch.nom nomCh, l.numero numeroLit, el.nom EtLitNom " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN etatslits el ON el.idEtatLit = l.idEtatLit " +
                    "INNER JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "INNER JOIN departements d ON d.idDepartement = ch.idDepartement " +
                    "INNER JOIN quartsemployescitoyens qec ON qec.idCitoyen = c.idCitoyen " +
                    "INNER JOIN quartsemployes qe ON qe.idQuartEmploye = qec.idQuartEmploye " +
                    "INNER JOIN employes e ON e.idEmploye = qe.idEmploye " +
                    "WHERE e.numEmploye = '" + employe.NumEmploye + "' "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            UnGenre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("GenreCit")),
                            DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("DNaiss"),
                            NumTelephone = SqlDR.GetString("Tel"),
                            Adresse = SqlDR.GetString("AdresseCit"),
                            Lit = new Lit
                            {
                                Numero = SqlDR.GetString("numeroLit"),
                                EtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), SqlDR.GetString("EtLitNom")),
                                Chambre = new Chambre
                                {
                                    Numero = SqlDR.GetString("nomCh"),
                                    UnDepartement = new Departement
                                    {
                                        Nom = SqlDR.GetString("nomDep"),
                                      
                                    }
                                }
                            }
                            
                        });
                    }
                    );


            }

            return lstCitoyen;
        }

        // ****** FÉLIX, tu peux utiliser cette requête pour l'infirmière-chef!! *******
        // On rend static la fonction pour être en mesure de l'utiliser partout
        public static List<Citoyen> GetTousCitoyensDepartement(Departement departementSelectionne)
        {
            // On crée une liste de citoyen venant de la BD
            List<Citoyen> lstCitoyen = new List<Citoyen>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, c.dateNaissance DNaissance, c.telephone Tel, c.Adresse AdresseCit, " +
                    "g.nom NomGenre, " +
                    "d.nom nomDep, " +
                    "ch.nom nomCh, " +
                    "l.numero numeroLit, " +
                    "el.nom EtLitNom " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN etatslits el ON el.idEtatLit = l.idEtatLit " +
                    "INNER JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "INNER JOIN departements d ON d.idDepartement = ch.idDepartement " +
                    "WHERE d.nom = '" + departementSelectionne.Nom + "' "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            UnGenre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("NomGenre")),
                            DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("DNaissance"),
                            NumTelephone = SqlDR.GetString("Tel"),
                            Adresse = SqlDR.GetString("AdresseCit"),

                            Lit = new Lit
                            {
                                Numero = SqlDR.GetString("numeroLit"),
                                EtatLit = (EtatLit)Enum.Parse(typeof(EtatLit), SqlDR.GetString("EtLitNom")),
                                Chambre = new Chambre
                                {
                                    Numero = SqlDR.GetString("nomCh"),
                                    UnDepartement = new Departement
                                    {
                                        Nom = SqlDR.GetString("nomDep"),

                                    }
                                }
                            }

                        });
                    }
                    );
            }

            return lstCitoyen;
        }

        public static Citoyen GetUnCitoyen(Citoyen citoyen)
        {
            // On crée un citoyen venant de la BD
            Citoyen InfosCitoyen = new Citoyen();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, g.nom nomGenre, c.dateNaissance dtNaiss, c.adresse uneAdresse, c.telephone numTel " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "' "
                     , SqlDR => {

                        InfosCitoyen.Nom = SqlDR.GetString("nomCit");
                        InfosCitoyen.Prenom = SqlDR.GetString("prenomCit");
                        InfosCitoyen.AssMaladie = SqlDR.GetString("AssMal");
                        InfosCitoyen.UnGenre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("nomGenre"));
                        InfosCitoyen.DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("dtNaiss");
                        InfosCitoyen.Adresse = SqlDR.GetString("uneAdresse");
                        InfosCitoyen.NumTelephone = SqlDR.GetString("numTel");
                         
                     }
                    );
            }

            return InfosCitoyen;
        }

        #endregion GET
        #region PUT
        public static void PutCitoyen(Citoyen citoyen)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "UPDATE citoyens " +
                        "SET prenom = '" + citoyen.Prenom + "', " +
                        "nom = '" + citoyen.Nom + "', " +
                        //"numAssuranceMaladie = '" + citoyen.AssMaladie + "', " +
                        "dateNaissance = '" + citoyen.DateNaissance + "', " +
                        "telephone = '" + citoyen.NumTelephone + "', " +
                        "adresse = '" + citoyen.Adresse + "', " +
                        "idGenre = ( SELECT idGenre FROM genres WHERE nom = '" + citoyen.UnGenre + "') " +
                        "WHERE numAssuranceMaladie = '" + citoyen.AssMaladie + "' "
                        ,
                        citoyen
                    )
                );
            }
        }
        #endregion
        #region POST
        public static void PostCitoyen(Citoyen citoyen)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "INSERT INTO citoyens (prenom, nom, numAssuranceMaladie, dateNaissance, telephone, adresse, idGenre) " +
                        "VALUES ('" + citoyen.Prenom + "', " +
                        "'" + citoyen.Nom + "', " +
                        "'" + citoyen.AssMaladie + "', " +
                        "'" + citoyen.DateNaissance + "', " +
                        "'" + citoyen.NumTelephone + "', " +
                        "'" + citoyen.Adresse + "', " +
                        "(SELECT idGenre FROM genres g WHERE g.nom = '" + citoyen.UnGenre.ToString() + "') ) "
                        ,
                        citoyen
                    )
                );
            }
        }
        #endregion

    }
}
