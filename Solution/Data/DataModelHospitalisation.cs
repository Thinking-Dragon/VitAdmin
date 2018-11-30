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
        public static List<Hospitalisation> GetHospitalisations(Citoyen citoyen)
        {
            // On crée une liste d'hospitalisation venant de la BD
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
                            //DateFin = SqlDR.IsDBNull(SqlDR.GetOrdinal("dFin")) ? new DateTime() : (DateTime)SqlDR.GetMySqlDateTime("dFin")
                            DateFin = (DateTime)SqlDR.GetMySqlDateTime("dFin")
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

        public static Hospitalisation GetHospitalisation(Citoyen citoyen)
        {
            Hospitalisation hospitalisation = new Hospitalisation();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                ConnexionBD.Instance().ExecuterRequete(
                    "SELECT h.dateDebut dDebut, h.dateFin dFin, t.Nom NomTrait " +
                    "FROM hospitalisations h " +
                    "INNER JOIN citoyens c ON c.idCitoyen = h.idCitoyen " +
                    "INNER JOIN hospitalisationstraitements ht ON ht.idHospitalisation = h.idHospitalisation " +
                    "INNER JOIN traitements t ON t.idTraitement = ht.idTraitement " +
                    "INNER JOIN departements d ON d.idDepartement = t.idDepartement " +
                    "WHERE c.numAssuranceMaladie ='" + citoyen.AssMaladie + "' "
                    , lecteur =>
                    {
                        hospitalisation.DateDebut = (DateTime)lecteur.GetMySqlDateTime("dDebut");
                        hospitalisation.DateFin = lecteur.IsDBNull(lecteur.GetOrdinal("dFin")) ? new DateTime() : (DateTime)lecteur.GetMySqlDateTime("dFin");
                    }
                    );
            }

            return hospitalisation;
        }

        public static void PostHospitalisation(Citoyen citoyen, Hospitalisation hospitalisation, Traitement traitement, Chambre chambre, Lit lit)
        {

            if (ConnexionBD.Instance().EstConnecte())
            {
                PutHospitalisationTerminees(citoyen);

                // On crée la nouvelle hospitalisation liée au patient
                ConnexionBD.Instance().ExecuterRequete(
                    
                        "INSERT INTO hospitalisations (idCitoyen, dateDebut, dateFin, contexte) " +
                        "VALUES ((SELECT idCitoyen FROM citoyens c WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "'), " +
                        "'" + hospitalisation.DateDebut.ToString() + "', " +
                        "'" + hospitalisation.DateFin.ToString() + "', " +
                        "'" + hospitalisation.Contexte + "') "
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

                        "INSERT INTO hospitalisationstraitements (idHospitalisation, idTraitement, estEnCours) " +
                        "VALUES ((SELECT idHospitalisation FROM hospitalisations h INNER JOIN citoyens c WHERE (c.numAssuranceMaladie = '" + citoyen.AssMaladie + "') AND (h.dateDebut = '" + hospitalisation.DateDebut.ToString() + "')), " +
                        "(SELECT idTraitement FROM traitements t WHERE t.nom = '" + traitement.Nom + "'), " +
                        "true) "
                );

                PutHospitalisationTraitement(hospitalisation, traitement);

                // Ensuite, il faut mettre à jour le lit dans lequel le citoyen est hospitalisé s'il a été assigné dans un lit disponible
                // Si le citoyen était assigné à un autre lit, il faut le sortir de ce lit pour le libérer. 
                // TODO: Il va falloir mettre l'ancien lit en entretien par défaut lorsque l'infirmière-chef aura le menu pour modifier l'état des lits
                if(citoyen.Lit.Numero != null)
                    DataModelLit.PutAncienLitCitoyen(citoyen);
                
                if(lit != null)
                    DataModelLit.PutNouveauLitCitoyen(lit, citoyen);
                
                //On change le lit du patient dans la mémoire aussi!
                citoyen.Lit = lit;
            }
        }
        /// <summary>
        /// sert à rendre à false le estEnCours des traitements qui ne sont plus en cours dans une hospitalisation
        /// </summary>
        /// <param name="hospitalisation"></param>
        /// <param name="traitementEnCours"></param> 
        public static void PutHospitalisationTraitement(Hospitalisation hospitalisation, Traitement traitementEnCours)
        {

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                // Si oui, on execute la requête que l'on veut effectuer
                ConnexionBD.Instance().ExecuterRequete(
                    "UPDATE hospitalisationstraitements ht " +
                    "SET ht.estEnCours = 0 " +
                    "WHERE ht.idHospitalisation = (SELECT idHospitalisation FROM hospitalisations h WHERE h.dateDebut = '" + hospitalisation.DateDebut + "') " +
                    "AND ht.idTraitement != (SELECT idTraitement FROM traitements t WHERE t.nom = '" + traitementEnCours.Nom + "') "
                    );
            }
        }

        /// <summary>
        /// Sert à s'assurer que les dernières hospitalisations ont été terminées lorsqu'un nouvelle hospitalisation a été créée pour un patient
        /// </summary>
        /// <param name="citoyen"></param>
        public static void PutHospitalisationTerminees(Citoyen citoyen)
        {
            Hospitalisation hospitalisation = new Hospitalisation();

            // On vérifie si la BD est connecté
            if (ConnexionBD.Instance().EstConnecte())
            {
                //hospitalisation = GetHospitalisation(citoyen);

                // On met fin au traitement de l'hospitalisation à terminer
                ConnexionBD.Instance().ExecuterRequete(
                    "UPDATE hospitalisationstraitements ht " +
                    "SET ht.estEnCours = 'false' " +
                    "WHERE ht.idHospitalisation = (SELECT idHospitalisation FROM hospitalisations h WHERE (h.dateFin = '0001-01-01 00:00:00' OR h.dateFin IS NULL) " +
                    "AND h.idCitoyen = (SELECT idCitoyen FROM citoyens c WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "')) "
                    );

                // Si oui, on execute la requête que l'on veut effectuer
                ConnexionBD.Instance().ExecuterRequete(
                    "UPDATE hospitalisations h " +
                    "SET h.dateFin = '" + DateTime.Now + "' " +
                    "WHERE (h.dateFin = '0000-00-00 00:00:00' OR h.dateFin IS NULL) " +
                    "AND h.idCitoyen = (SELECT idCitoyen FROM citoyens c WHERE c.numAssuranceMaladie = '" + citoyen.AssMaladie + "') "
                    );
            }
        }


    }
}
