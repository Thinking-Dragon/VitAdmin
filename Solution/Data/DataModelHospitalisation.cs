using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitAdmin.Model;

namespace VitAdmin.Data
{
    class DataModelHospitalisation
    {
        public static List<Hospitalisation> getHospitalisation(Citoyen citoyen)
        {
            // On crée une liste de citoyen venant de la BD
            List<Hospitalisation> lstHospitalisation = new List<Hospitalisation>();
            List<string> lstNomTraitement = new List<string>();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT h.dateDebut dDebut, h.dateFin dFin, t.Nom NomTrait " +
                    "FROM hospitalisations h " +
                    "INNER JOIN citoyens c ON c.idCitoyen = h.idCitoyen " +
                    "INNER JOIN hospitalisationstraitements ht ON ht.idHospitalisation = h.idHospitalisation " +
                    "INNER JOIN traitements t ON t.idTraitement = ht.idTraitement " +
                    "INNER JOIN departements d ON d.idDepartement = t.idDepartement " +
                    "WHERE c.numAssuranceMaladie ='" + citoyen.AssMaladie + "' "
                    , SqlDR => {
                        lstHospitalisation.Add(new Hospitalisation
                        {
                            DateDebut = (DateTime)SqlDR.GetMySqlDateTime("dDebut"),
                            DateFin = SqlDR.IsDBNull(SqlDR.GetOrdinal("dFin")) ? new DateTime() : (DateTime)SqlDR.GetMySqlDateTime("dFin") , 
                            
                        });

                        lstNomTraitement.Add(SqlDR.GetString("NomTrait"));
                    }
                    );
            }


            // Il faut aller chercher dans une autre requête les traitements de l'hospitalisation
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste des citoyens de la BD
                foreach(Hospitalisation hospitalisation in lstHospitalisation)
                {
                    hospitalisation.LstTraitements = new List<Traitement>();
                    foreach(string NomTraitement in lstNomTraitement)
                    {

                        ConnexionBD.Instance().ExecuterRequete(
                            "SELECT d.Nom depNom " +
                            "FROM traitements t " +
                            "INNER JOIN departements d ON d.idDepartement = t.idDepartement " +
                            "INNER JOIN hospitalisationstraitements ht ON ht.idTraitement = t.idTraitement " +
                            "INNER JOIN hospitalisations h ON h.idHospitalisation = ht.idHospitalisation " +
                            "INNER JOIN citoyens c ON c.idCitoyen = h.idCitoyen " +
                            "WHERE t.Nom ='" + NomTraitement + "' "
                            , SqlDR => { hospitalisation.LstTraitements.Add(new Traitement {

                                DepartementAssocie = new Departement { Nom = SqlDR.GetString("depNom") }

                                });
                                
                            }
                            );
                    }
                }
            }

            return lstHospitalisation;
        }
    }
}
