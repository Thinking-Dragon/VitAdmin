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
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Ça plante, puisque des éléments sont null comme le lit, la chambre et le département...

                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, g.Nom GenreCit, c.dateNaissance DNaiss, c.telephone Tel, c.adresse AdresseCit, d.nom nomDep, ch.nom nomCh, l.numero numeroLit, el.nom EtLitNom " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "LEFT JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "LEFT JOIN etatslits el ON el.idEtatLit = l.idEtatLit " +
                    "LEFT JOIN chambres ch ON ch.idChambre = l.idChambre " +
                    "LEFT JOIN departements d ON d.idDepartement = ch.idDepartement "
                    , SqlDR => {
                        lstCitoyen.Add(new Citoyen
                        {
                            Nom = SqlDR.GetString("nomCit"),
                            Prenom = SqlDR.GetString("prenomCit"),
                            AssMaladie = SqlDR.GetString("AssMal"),
                            Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("GenreCit")),
                            DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("DNaiss"),
                            NumTelephone = SqlDR.GetString("Tel"),
                            Adresse = SqlDR.GetString("AdresseCit"),
                            Lit = SqlDR.IsDBNull(SqlDR.GetOrdinal("numeroLit")) ? new Lit() : new Lit
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
                            Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("GenreCit")),
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
                            Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("NomGenre")),
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
                        InfosCitoyen.Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("nomGenre"));
                        InfosCitoyen.DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("dtNaiss");
                        InfosCitoyen.Adresse = SqlDR.GetString("uneAdresse");
                        InfosCitoyen.NumTelephone = SqlDR.GetString("numTel");
                         
                     }
                    );
            }

            return InfosCitoyen;
        }

        public static void GetUnCitoyenParLit(Lit lit)
        {
            Citoyen citoyen = new Citoyen();
            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, g.nom nomGenre, c.dateNaissance dtNaiss, c.adresse uneAdresse, c.telephone numTel " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "WHERE l.idLit = '" + lit._identifiant + "' "
                     , SqlDR => {

                         citoyen.Nom = SqlDR.GetString("nomCit");
                         citoyen.Prenom = SqlDR.GetString("prenomCit");
                         citoyen.AssMaladie = SqlDR.GetString("AssMal");
                         citoyen.Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("nomGenre"));
                         citoyen.DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("dtNaiss");
                         citoyen.Adresse = SqlDR.GetString("uneAdresse");
                         citoyen.NumTelephone = SqlDR.GetString("numTel");

                     }
                    );
            }

            lit.Citoyen = citoyen;
        }

        public static List<Citoyen> GetCitoyenDemandeTraitement(Departement departement)
        {
            List<Citoyen> lstCitoyen = new List<Citoyen>();
            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT c.nom nomCit, c.prenom prenomCit, c.numAssuranceMaladie AssMal, g.nom nomGenre, c.dateNaissance dtNaiss, c.adresse uneAdresse, c.telephone numTel " +
                    "FROM citoyens c " +
                    "INNER JOIN genres g ON g.idGenre = c.idGenre " +
                    "INNER JOIN lits l ON l.idCitoyen = c.idCitoyen " +
                    "INNER JOIN hospitalisations h ON h.idCitoyen = c.idCitoyen " +
                    "INNER JOIN hospitalisationstraitements ht ON ht.idHospitalisation = h.idHospitalisation " +
                    "INNER JOIN traitements t ON t.idTraitement = ht.idTraitement " +
                    "INNER JOIN departements d ON d.idDepartement = t.idDepartement " +
                    "WHERE d.nom = '" + departement.Nom + "' " +
                    "AND ht.estEnCours = true " 
                     , SqlDR => {
                         lstCitoyen.Add(new Citoyen
                         {
                             Nom = SqlDR.GetString("nomCit"),
                             Prenom = SqlDR.GetString("prenomCit"),
                             AssMaladie = SqlDR.GetString("AssMal"),
                             Genre = (Genre)Enum.Parse(typeof(Genre), SqlDR.GetString("nomGenre")),
                             DateNaissance = (DateTime)SqlDR.GetMySqlDateTime("dtNaiss"),
                             Adresse = SqlDR.GetString("uneAdresse"),
                             NumTelephone = SqlDR.GetString("numTel")
                         });
                     }
                     
                    );
            }

            return lstCitoyen;
        }

        #endregion GET
        #region PUT
        public static void PutCitoyen(Citoyen citoyen, string AssMaladieAncien)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    String.Format(
                        "UPDATE citoyens " +
                        "SET prenom = '" + citoyen.Prenom + "', " +
                        "nom = '" + citoyen.Nom + "', " +
                        "numAssuranceMaladie = '" + citoyen.AssMaladie + "', " +
                        "dateNaissance = '" + citoyen.DateNaissance + "', " +
                        "telephone = '" + citoyen.NumTelephone + "', " +
                        "adresse = '" + citoyen.Adresse + "', " +
                        "idGenre = ( SELECT idGenre FROM genres WHERE nom = '" + citoyen.Genre + "') " +
                        "WHERE numAssuranceMaladie = '" + AssMaladieAncien + "' "
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
                        "(SELECT idGenre FROM genres g WHERE g.nom = '" + citoyen.Genre.ToString() + "') ) "
                        ,
                        citoyen
                    )
                );
            }
        }
        #endregion

    }
}
