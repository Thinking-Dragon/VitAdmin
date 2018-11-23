using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelDepartement
    {
        public static List<Departement> GetDepartements()
        {
            List<Departement> lstDepartement = new List<Departement>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                List<int> idEmployesChefs = new List<int>();

                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT * " +
                    "FROM departements"
                    , SqlDR => {
                        lstDepartement.Add(new Departement
                        {
                            _identifiant = int.Parse(SqlDR.GetString("idDepartement")),
                            Nom = SqlDR.GetString("nom"),
                            Abreviation = SqlDR.GetString("abreviation")
                        });
                        idEmployesChefs.Add(SqlDR.IsDBNull(SqlDR.GetOrdinal("idEmploye")) ? -1 : int.Parse(SqlDR.GetString("idEmploye")));
                    }
                );

                for (int i = 0; i < lstDepartement.Count; i++)
                    lstDepartement[i].PersonnelMedicalEnChef = (idEmployesChefs[i] == -1 ? null : DataModelEmploye.GetEmploye(idEmployesChefs[i]));

                for (int i = 0; i < lstDepartement.Count; i++)
                    lstDepartement[i].Chambres = new ObservableCollection<Chambre>(DataModelChambre.GetChambres(lstDepartement[i]._identifiant.ToString(), "lits, equipements"));
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
                    "SELECT d.idDepartement _id, d.nom depNom, d.abreviation depAbrev " +
                    "FROM departements d " +
                    "JOIN quarts q ON q.idDepartement = d.idDepartement " +
                    "JOIN quartsemployes qe ON qe.idQuart = q.idQuart " +
                    "JOIN employes e ON e.idEmploye = qe.idEmploye " +
                    "WHERE e.numEmploye = '" + employe.NumEmploye + "' "
                    , SqlDR => {
                        departement._identifiant = int.Parse(SqlDR.GetString("_id"));
                        departement.Nom = SqlDR.GetString("depNom");
                        departement.Abreviation = SqlDR.GetString("depAbrev");
                    }
                    );
            }

            return departement;
        }

        public static Departement GetDepartementInfChef(Employe employe)
        {
            Departement departement = new Departement();

            int idEmployeRecherche = DataModelEmploye.GetidEmploye(employe);

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT d.idDepartement _id, d.nom depNom, d.abreviation depAbrev " +
                    "FROM departements d " +
                    "JOIN quarts q ON q.idDepartement = d.idDepartement " +
                    "JOIN quartsemployes qe ON qe.idQuart = q.idQuart " +
                    "JOIN employes e ON e.idEmploye = qe.idEmploye " +
                    "WHERE d.idEmploye = " + idEmployeRecherche + " "
                    , SqlDR => {
                        departement._identifiant = int.Parse(SqlDR.GetString("_id"));
                        departement.Nom = SqlDR.GetString("depNom");
                        departement.Abreviation = SqlDR.GetString("depAbrev");
                    }
                    );
            }

            return departement;
        }

        public static Departement GetDepartement(string abreviation, string expand = "")
        {
            Departement departement = null;
            if (ConnexionBD.Instance().EstConnecte())
            {
                int idEmployeChef = -1;
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "SELECT idDepartement _id, nom, abreviation, idEmploye " +
                        "FROM Departements " +
                        "WHERE abreviation = '{0}'",
                        abreviation
                    ), lecteur =>
                    {
                        departement = new Departement
                        {
                            _identifiant = int.Parse(lecteur.GetString("_id")),
                            Nom = lecteur.GetString("nom"),
                            Abreviation = lecteur.GetString("abreviation")
                        };
                        if(!lecteur.IsDBNull(lecteur.GetOrdinal("idEmploye")))
                            idEmployeChef = int.Parse(lecteur.GetString("idEmploye"));
                    }
                );
                
                departement.PersonnelMedicalEnChef = (idEmployeChef >= 0 ? null : DataModelEmploye.GetEmploye(idEmployeChef));
                if(expand.Contains("chambres"))
                {
                    StringBuilder expandPropagation = new StringBuilder();
                    if (expand.Contains("lits"))
                        expandPropagation.Append("lits ");
                    if (expand.Contains("equipements"))
                        expandPropagation.Append("equipements ");
                    departement.Chambres = new ObservableCollection<Chambre>(
                        DataModelChambre.GetChambres(
                            departement._identifiant.ToString(),
                            expandPropagation.ToString()
                        )
                    );
                }
            }
            return departement;
        }

        public static void PostDepartement(Departement departement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                if(departement.PersonnelMedicalEnChef == null)
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        string.Format(
                            "INSERT INTO Departements (idEmploye, nom, abreviation) " +
                            "VALUES ( " +
                            "   null, " +
                            "   '{0}', " +
                            "   '{1}' " +
                            ")",
                            departement.Nom, departement.Abreviation
                        )
                    );
                }
                else
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        string.Format(
                            "INSERT INTO Departements (idEmploye, nom, abreviation) " +
                            "VALUES ( " +
                            "   (SELECT idEmploye FROM Employes WHERE numEmploye = '{0}'), " +
                            "   '{1}', " +
                            "   '{2}' " +
                            ")",
                            departement.PersonnelMedicalEnChef.NumEmploye,
                            departement.Nom,
                            departement.Abreviation
                        )
                    );
                }

                DataModelChambre.PostChambres(
                    GetDepartement(departement.Abreviation, "chambres:{lits, equipements}")._identifiant,
                    new List<Chambre>(departement.Chambres));
            }
        }

        public static void PutDepartement(Departement departement)
        {
            if (ConnexionBD.Instance().EstConnecte())
            {
                if(departement.PersonnelMedicalEnChef == null)
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        string.Format(
                            "UPDATE Departements " +
                            "SET nom = '{0}', abreviation = '{1}', idEmploye = null " +
                            "WHERE idDepartement = {2} ",
                            departement.Nom,
                            departement.Abreviation,
                            departement._identifiant
                        )
                    );
                }
                else
                {
                    ConnexionBD.Instance().ExecuterRequete(
                        string.Format(
                            "UPDATE Departements " +
                            "SET nom = '{0}', abreviation = '{1}', idEmploye = (SELECT idEmploye FROM Employes WHERE numEmploye = '{2}') " +
                            "WHERE idDepartement = {3} ",
                            departement.Nom,
                            departement.Abreviation,
                            departement.PersonnelMedicalEnChef.NumEmploye,
                            departement._identifiant
                        )
                    );
                }
                DataModelChambre.PutChambres(departement._identifiant, new List<Chambre>(departement.Chambres));
            }
        }


        public static void DeleteDepartement(Departement departement)
        {
            if(ConnexionBD.Instance().EstConnecte())
            {
                if (departement.Chambres.Count > 0)
                    foreach (var chambre in departement.Chambres)
                        DataModelChambre.DeleteChambre(chambre);
                ConnexionBD.Instance().ExecuterRequete(
                    string.Format(
                        "DELETE FROM Departements " +
                        "WHERE idDepartement = {0}",
                        departement._identifiant
                    )
                );
            }
        }
    }
}