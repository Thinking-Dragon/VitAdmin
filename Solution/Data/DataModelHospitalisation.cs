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
                            //DateFin = SqlDR.IsDBNull(SqlDR.GetOrdinal("dFin")) ? new DateTime() : (DateTime)SqlDR.GetMySqlDateTime("dFin") , 
                            
                        });

                    }
                    );
            }


            // Il faut aller chercher dans une autre requête les traitements de l'hospitalisation
            if (ConnexionBD.Instance().EstConnecte() && lstHospitalisation.Count > 0)
            {
                // Si oui, on execute la requête que l'on veut effectuer
                // SqlDR (MySqlDataReader) emmagasine une liste de la BD


                foreach(Hospitalisation hospitalisation in lstHospitalisation)
                {
                    hospitalisation.LstTraitements = new List<Traitement>();
                    

                    ConnexionBD.Instance().ExecuterRequete(
                        "SELECT d.Nom depNom, t.Nom TraitNom " +
                        "FROM traitements t " +
                        "INNER JOIN departements d ON d.idDepartement = t.idDepartement " +
                        "INNER JOIN hospitalisationstraitements ht ON ht.idTraitement = t.idTraitement " +
                        "INNER JOIN hospitalisations h ON h.idHospitalisation = ht.idHospitalisation " +
                        "WHERE h.dateDebut = '" + hospitalisation.DateDebut + "' "
                        , SqlDR => { hospitalisation.LstTraitements.Add(new Traitement {

                            Nom = SqlDR.GetString("TraitNom"),
                            DepartementAssocie = new Departement { Nom = SqlDR.GetString("depNom") }

                            });
                                
                        }
                        );
                    
                }
                

            }

            return lstHospitalisation;
        }

        public static void PostHospitalisation(Citoyen citoyen, Hospitalisation hospitalisation, Traitement traitement, Chambre chambre, Lit lit)
        {

            if (ConnexionBD.Instance().EstConnecte())
            {

                // On crée la nouvelle hospitalisation liée au patient
                ConnexionBD.Instance().ExecuterRequete(
                    
                        "INSERT INTO hospitalisations (idCitoyen, dateDebut, dateFin, contexte) " +
                        "VALUES ((SELECT idCitoyen FROM citoyens c WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "'), " +
                        "'" + hospitalisation.DateDebut.ToString() + "', " +
                        "'" + hospitalisation.DateFin.ToString() + "', " +
                        "'" + hospitalisation.Contexte + "') " ,
                        new Tuple<string, string>("@AssMaladie", citoyen.AssMaladie),
                        new Tuple<string, string>("@DateDebut", hospitalisation.DateDebut.ToString()),
                        new Tuple<string, string>("@DateFin", hospitalisation.DateFin.ToString()),
                        new Tuple<string, string>("@Contexte", hospitalisation.Contexte)
                );

                // On crée la nouvelle liste de symptôme lié à l'hospitalisation
                hospitalisation.LstSymptomes.ForEach(symptome => {
                    ConnexionBD.Instance().ExecuterRequete(

                            "INSERT INTO symptomes (idHospitalisation, description, estActif) " +
                            "VALUES ((SELECT idHospitalisation FROM hospitalisation h " +
                            "INNER JOIN citoyen c ON c.idCitoyen = h.idCitoyen " +
                            "WHERE h.dateDebut = '" + hospitalisation.DateDebut.ToString() + "' AND " +
                            "c.numAssuranceMaladie = '" + citoyen.AssMaladie + "' ), " + // Fin de la valeur idHospitalisation
                            "'" + symptome.Description + "', " +
                            "'" + symptome.EstActif + "') ");
                });


                // Ensuite, il faut créer le lien en bd entre l'hospitalisation et le traitement assigné
                ConnexionBD.Instance().ExecuterRequete(

                        "INSERT INTO hospitalisationstraitements (idHospitalisation, idTraitement) " +
                        "VALUES ((SELECT idHospitalisation FROM hospitalisations h INNER JOIN citoyens c WHERE (c.numAssuranceMaladie = '" + citoyen.AssMaladie + "') AND (h.dateDebut = '" + hospitalisation.DateDebut.ToString() + "')), " +
                        "(SELECT idTraitement FROM traitements t WHERE t.nom = '" + traitement.Nom + "')) " ,
                        new Tuple<string, string>("@AssMaladie", citoyen.AssMaladie),
                        new Tuple<string, string>("@TraitementNom", traitement.Nom)
                );

                // Ensuite, il faut mettre à jour le lit dans lequel le citoyen est hospitalisé
                ConnexionBD.Instance().ExecuterRequete(

                        "UPDATE lits l " +
                        "JOIN chambres ch ON ch.idChambre = l.idChambre " +
                        "SET idCitoyen = (SELECT idCitoyen FROM citoyens c WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "') " +
                        "WHERE (ch.nom = '" + chambre.Numero + "') AND " +
                        "(l.numero = '" + lit.Numero + "') ",
                        new Tuple<string, string>("@AssMaladie", citoyen.AssMaladie),
                        new Tuple<string, string>("@NomChambre", chambre.Numero),
                        new Tuple<string, string>("@NumLit", lit.Numero)

                );
            }
        }

        public void PostHospitalisation2(Hospitalisation hospitalisation)
        {

        }
    }
}
