using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;
using VitAdmin.Parameter;

namespace VitAdmin.Data
{
    public static class DataModelEmploye
    {
        public static void SetIdEmployeUsagerConnecte()
        {
            Employe employe = new Employe();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = "SELECT e.idEmploye emp, p.nom pos FROM Employes e " +
                                 "INNER JOIN Postes p ON p.idPoste = e.idPoste " +
                                 "INNER JOIN Usagers us ON e.idEmploye = us.idEmploye " +
                                 "WHERE us.nomUtilisateur = '" + UsagerConnecte.Usager.NomUtilisateur + "' ";
                ConnexionBD.Instance().ExecuterRequete(requete, SqlDR =>
                {
                    UsagerConnecte.Usager.idEmploye = SqlDR.GetInt32("emp");
                    UsagerConnecte.Usager.Poste = SqlDR.GetString("pos");
                });
            }
        }

        public static List<Employe> GetEmployes()
        {
            List<Employe> lstEmployes = new List<Employe>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = "SELECT * FROM Employes e " +
                                 "JOIN Postes p ON p.idPoste = e.idPoste";
                ConnexionBD.Instance().ExecuterRequete(requete, SqlDR =>
                {
                    lstEmployes.Add(new Employe
                    {
                        NumEmploye = SqlDR.GetString("e.numEmploye"),
                        NumPermis = SqlDR.GetString("e.numPermis"),
                        NAS = SqlDR.GetString("e.NAS"),
                        Poste = SqlDR.GetString("p.nom")
                    });
                });
            }

            return lstEmployes;
        }

        public static Employe GetEmploye(int idEmp)
        {
            Employe employe = new Employe();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = "SELECT nom, prenom, numEmploye FROM Employes e " +
                                 "JOIN Citoyens c ON c.idCitoyen = e.idCitoyen " +
                                 "WHERE e.idEmploye = " + idEmp + ";";

                ConnexionBD.Instance().ExecuterRequete(requete, SqlDR =>
                {
                    employe = new Employe
                    {
                        NumEmploye = SqlDR.GetString("numEmploye"),
                        Nom = SqlDR.GetString("nom"),
                        Prenom = SqlDR.GetString("prenom")
                    };
                });
            }

            return employe;
        }

        public static List<Employe> GetLstEmployesDepartement(Departement depSelectionne)
        {
            List<Employe> lstEmployes = new List<Employe>();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = "SELECT c.nom nomCit, c.prenom prenomCit, e.numEmploye nEmploye " +
                                 "FROM citoyens c " +
                                 "INNER JOIN employes e ON e.idCitoyen = c.idCitoyen " +
                                 "INNER JOIN quartsEmployes qe ON qe.idEmploye = e.idEmploye " +
                                 "INNER JOIN quarts q ON q.idQuart = qe.idQuart " +
                                 "INNER JOIN departements d ON d.idDepartement = q.idDepartement " +
                                 "WHERE d.nom = '" + depSelectionne.Nom + "' " +
                                 "GROUP BY nEmploye " +
                                 "ORDER BY nomCit ";
                ConnexionBD.Instance().ExecuterRequete(requete, SqlDR =>
                {
                    lstEmployes.Add(new Employe
                    {
                        Nom = SqlDR.GetString("nomCit"),
                        Prenom = SqlDR.GetString("prenomCit"),
                        NumEmploye = SqlDR.GetString("nEmploye"),
                    });
                });
            }

            return lstEmployes;
        }

        public static void AddEmploye (Employe employe)
        {
            EtatAvecMessage retour = new EtatAvecMessage();

            if (ConnexionBD.Instance().EstConnecte())
            {
                string requete = string.Format("INSERT INTO Employes " +
                                           "(idCitoyen, idPoste, numEmploye, numPermis, NAS) " +
                                           "VALUES (" +
                                           "(SELECT idCitoyen FROM Citoyens WHERE numAssuranceMaladie = '{0}'," +
                                           "(SELECT idPoste FROM Postes WHERE nom = '{1}'," +
                                           "'{2}'," +
                                           "'{3}'," +
                                           "'{4}'"
                                           , employe.AssMaladie, employe.Poste, employe.NumEmploye, employe.NumPermis, employe.NAS);

                //DataModelCitoyen.AddCitoyen(employe);
                ConnexionBD.Instance().ExecuterRequete(requete);
                // TODO : Recevoir code erreur BD dans cas d'erreur (duplicata)
            }
        }
    }
}
