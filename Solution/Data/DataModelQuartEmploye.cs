using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelQuartEmploye
    {
        public static List<QuartEmploye> GetHoraire(Employe employe)
        {
            List<QuartEmploye> horaire = new List<QuartEmploye>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT date dt, cit.nom nm, cit.prenom pm, dep.nom nmDep, shift.periode typeQ " +
                          "FROM quartsemployes qe " +
                              "INNER JOIN quarts q on qe.idQuart = q.idQuart " +
                              "INNER JOIN employes em  on em.idEmploye = qe.idEmploye " +
                              "INNER JOIN citoyens cit on cit.idCitoyen = em.idCitoyen " +
                              "INNER JOIN periodesjournee shift on shift.idPeriodeJournee = q.idPeriodeJournee " +
                              "INNER JOIN departements dep on dep.idDepartement = q.idDepartement " +
                                  "WHERE em.idEmploye = " + employe.idEmploye + ";"

                    , SqlDR => {
                        horaire.Add(new QuartEmploye
                        {
                            TypeDeQuart = (TypeQuart)System.Enum.Parse(typeof(TypeQuart), SqlDR.GetString("typeQ")),
                            Date = SqlDR.GetDateTime("dt"),
                            DepartementAssocie = new Departement
                            {
                                Nom = SqlDR.GetString("nmDep")
                            },
                            Employe = new Employe
                            {
                                Nom = SqlDR.GetString("nm"),
                                Prenom = SqlDR.GetString("pm")
                            }
                        });
                    });

            }

            /*SELECT  date, cit.nom, prenom, dep.nom, periode
                          FROM quartsEmployes qe
                              INNER JOIN quarts q on qe.idQuart = q.idQuart
                              INNER JOIN employes em  on em.idEmploye = qe.idEmploye
                              INNER JOIN citoyens cit on cit.idCitoyen = em.idCitoyen
                              INNER JOIN periodesjournee shift on shift.idPeriodeJournee = q.idPeriodeJournee
                              INNER JOIN departements dep on dep.idDepartement = q.idDepartement
                                  WHERE qe.idEmploye = 4;*/

            return horaire;
        }

        public static void POSTHoraire(List<QuartEmploye> horaire)
        {

            if (ConnexionBD.Instance().EstConnecte())
            {
                foreach (QuartEmploye item in horaire)
                {
                    if (!existQuart(item))
                    {
                        string req = string.Format("INSERT INTO quarts " +
                           "(idDepartement, idPeriodeJournee, date) " +
                           "VALUES (" +
                           "(SELECT idDepartement FROM departements WHERE nom = '{0}')," +
                           "(SELECT idPeriodeJournee FROM periodesjournee WHERE periode = '{1}'), " +
                           "'{2}');",
                           item.DepartementAssocie.Nom, item.TypeDeQuart, item.Date.ToString());

                        ConnexionBD.Instance().ExecuterRequete(req);
                    }

                    if (!existQuartEmploye(item))
                    {
                        string requete = string.Format("INSERT INTO quartsEmployes " +
                                               "(idQuart, idEmploye) " +
                                               "VALUES (" +

                                               "(SELECT idQuart FROM quarts q " +
                                               "INNER JOIN periodesJournee pj ON pj.idPeriodeJournee = q.idPeriodeJournee " +
                                               "INNER JOIN departements dep ON dep.idDepartement = q.idDepartement " +
                                               "WHERE date = '{0}' && pj.periode = '{1}' && dep.nom = '{2}' ), " +

                                               "(SELECT idEmploye FROM employes WHERE idEmploye = {3}));",
                                               item.Date.ToShortDateString(), item.TypeDeQuart, item.DepartementAssocie.Nom, item.Employe.idEmploye);

                        ConnexionBD.Instance().ExecuterRequete(requete);
                    }
                    
                }
            }
        }


        public static bool existQuart(QuartEmploye quart)
        {
            QuartEmploye qe = null;

            if (ConnexionBD.Instance().EstConnecte())
            {
                
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT date dt, dep.nom nmDep, shift.periode typeQ " +
                          "FROM quarts qe " +
                              "INNER JOIN periodesjournee shift on shift.idPeriodeJournee = qe.idPeriodeJournee " +
                              "INNER JOIN departements dep on dep.idDepartement = qe.idDepartement " +
                                  "WHERE qe.date = '" + quart.Date.ToShortDateString() + "' && shift.periode = '" + quart.TypeDeQuart + "' && dep.nom = '" + quart.DepartementAssocie.Nom + "';"

                    , SqlDR =>
                    {
                        qe = new QuartEmploye
                        {
                            Date = SqlDR.GetDateTime("dt"),
                            DepartementAssocie = new Departement
                            {
                                Nom = SqlDR.GetString("nmDep")
                            },
                            TypeDeQuart = (TypeQuart)System.Enum.Parse(typeof(TypeQuart), SqlDR.GetString("typeQ"))
                        };
                    });
            }

            return qe != null;
        }

        public static bool existQuartEmploye(QuartEmploye quart)
        {
            QuartEmploye qe = null;

            if (ConnexionBD.Instance().EstConnecte())
            {

                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT date dt, dep.nom nmDep, shift.periode typeQ " +
                          "FROM quartsemployes qe " +
                              "INNER JOIN quarts q on qe.idQuart = q.idQuart " +
                              "INNER JOIN employes em  on em.idEmploye = qe.idEmploye " +
                              "INNER JOIN citoyens cit on cit.idCitoyen = em.idCitoyen " +
                              "INNER JOIN periodesjournee shift on shift.idPeriodeJournee = q.idPeriodeJournee " +
                              "INNER JOIN departements dep on dep.idDepartement = q.idDepartement " +
                                  "WHERE em.idEmploye = " + quart.Employe.idEmploye + 
                                  " && q.date = '" + quart.Date.ToShortDateString() +
                                  "' && shift.periode = '" + quart.TypeDeQuart +
                                  "' && dep.nom = '" + quart.DepartementAssocie.Nom + "';"
                    , SqlDR => {
                        qe = new QuartEmploye
                        {
                            TypeDeQuart = (TypeQuart)System.Enum.Parse(typeof(TypeQuart), SqlDR.GetString("typeQ")),
                            Date = SqlDR.GetDateTime("dt"),
                            DepartementAssocie = new Departement
                            {
                                Nom = SqlDR.GetString("nmDep")
                            }
                        };
                    });
            }

            return qe != null;
        }

        public static void DELETE_quartEmploye(QuartEmploye qe)
        {
            string requete = string.Format("DELETE FROM quartsEmployes " +
                                               "WHERE idQuart = (SELECT idQuart FROM quarts WHERE date = '{0}' " +
                                               "&& idDepartement = (SELECT idDepartement FROM departements WHERE nom = '{1}') " +
                                               "&& idPeriodeJournee = (SELECT idPeriodeJournee FROM periodesjournee WHERE periode = '{2}')) " +
                                               "&& idEmploye = {3};",
                                                qe.Date.ToShortDateString(), qe.DepartementAssocie.Nom, qe.TypeDeQuart, qe.Employe.idEmploye);

            ConnexionBD.Instance().ExecuterRequete(requete);
        }
    }
}

