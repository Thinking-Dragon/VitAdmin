using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelResultatsLabo
    {
        public static List<ResultatLabo> GetResultatsLaboCitoyens(String NumAssMaladie, DateTime dateDebut)
        {
            // On crée une liste de citoyen venant de la BD
            List<ResultatLabo> lstResultatLabo = new List<ResultatLabo>();


            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                string test = dateDebut.ToString();
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT resultatslabo.nomAnalyse nA, resultatslabo.lienImage lIm, e.dateHeure dthr, cit.nom nm, cit.prenom pm, em.numEmploye numEm " +
                          "FROM resultatslabo " +
                              "INNER JOIN evenements e on e.idEvenement = resultatslabo.idEvenement " +
                              "INNER JOIN hospitalisations h on h.idHospitalisation = e.idHospitalisation " +
                              "INNER JOIN citoyens c on c.idCitoyen = h.idCitoyen " +
                              "INNER JOIN employes em  on em.idEmploye = e.idEmploye " +
                              "INNER JOIN citoyens cit on cit.idCitoyen = em.idCitoyen " +
                                  "WHERE c.numAssuranceMaladie = '" + NumAssMaladie + "' AND h.dateDebut = '" + dateDebut.ToString() + "'"

                    , SqlDR =>
                    {
                        lstResultatLabo.Add(new ResultatLabo
                        {
                            NomAnalyse = SqlDR.GetString("nA"),
                            LienImage = SqlDR.GetString("lIm"),
                            DateEvenement = SqlDR.GetDateTime("dthr"),
                            EmployeImplique = new Employe
                            {
                                Nom = SqlDR.GetString("nm"),
                                Prenom = SqlDR.GetString("pm"),
                                NumEmploye = SqlDR.GetString("numEm")
                            }
                        });
                    });

                foreach (ResultatLabo result in lstResultatLabo)
                {
                    result.Resultats = new BitmapImage(new Uri(result.LienImage));
                }


            }


            return lstResultatLabo;
        }

        public static void AddResultatLabo(Hospitalisation hospit, ResultatLabo resultLabo, int numEmp)
        {
            resultLabo.DateEvenement = DateTime.Now;

            if (ConnexionBD.Instance().EstConnecte())
            {

                string requete = string.Format("INSERT INTO evenements " +
                                           "(idHospitalisation, idEmploye, dateHeure, estNotifier) " +
                                           "VALUES (" +
                                           "(SELECT idHospitalisation FROM hospitalisations WHERE dateDebut = '{0}')," +
                                           "{1}," +
                                           "'{2}'," +
                                           "{3})", hospit.DateDebut.ToString(), numEmp, resultLabo.DateEvenement.ToString(), resultLabo.EstNotifier);


                ConnexionBD.Instance().ExecuterRequete(requete);

                requete = string.Format("INSERT INTO resultatsLabo " +
                                           "(idEvenement, lienImage, nomAnalyse) " +
                                           "VALUES (" +
                                           "(SELECT idEvenement FROM evenements WHERE dateHeure = '{0}')," +
                                           "'{1}'," +
                                           "'{2}')" 
                                           , resultLabo.DateEvenement.ToString(), resultLabo.LienImage, resultLabo.NomAnalyse);

                ConnexionBD.Instance().ExecuterRequete(requete);

            }
        }
    }
}
