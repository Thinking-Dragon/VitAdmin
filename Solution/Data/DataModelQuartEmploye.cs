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

    }
}

